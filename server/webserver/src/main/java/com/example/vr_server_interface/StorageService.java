package com.example.vr_server_interface;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.Resource;
import org.springframework.core.io.UrlResource;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.nio.file.AtomicMoveNotSupportedException;
import java.nio.file.FileAlreadyExistsException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.StandardCopyOption;
import java.util.Locale;
import java.util.Set;

/**
 * Bridges the HTTP-facing webserver directories and the filesystem pipeline.
 *
 * <p>Uploads are first completed in {@code webserver/uploads} and then
 * published atomically into {@code pipeline/input/original_image}. Downloads
 * are staged in {@code webserver/downloads}; after the response payload has
 * been read, both the staged file and the corresponding pipeline output are
 * deleted. The directories are therefore transport directories, not an
 * accumulating archive.</p>
 */
@Service
public class StorageService {

    private static final Set<String> ALLOWED_EXTENSIONS = Set.of("png", "jpg", "jpeg", "bmp", "webp");
    private static final String ANIMATED_GLTF_NAME = "animate3d_model.gltf";
    private static final String ANIMATED_FBX_NAME = "animate3d_model.fbx";

    private final Path uploadDir;
    private final Path pipelineInputDir;
    private final Path downloadDir;
    private final Path glbDir;
    private final Path animatedDir;

    @Autowired
    public StorageService(
            @Value("${storage.path.upload}") String uploadPath,
            @Value("${storage.path.pipeline-input}") String pipelineInputPath,
            @Value("${storage.path.download}") String downloadPath,
            @Value("${storage.path.glb}") String glbPath,
            @Value("${storage.path.animated}") String animatedPath) throws IOException {
        this.uploadDir = root(uploadPath);
        this.pipelineInputDir = root(pipelineInputPath);
        this.downloadDir = root(downloadPath);
        this.glbDir = root(glbPath);
        this.animatedDir = root(animatedPath);

        Files.createDirectories(this.uploadDir);
        Files.createDirectories(this.pipelineInputDir);
        Files.createDirectories(this.downloadDir);
        Files.createDirectories(this.glbDir);
        Files.createDirectories(this.animatedDir);
        removeInterruptedUploadTemporaries();
    }

    /**
     * Compatibility constructor for callers of the original four-path
     * service. New applications should use the property-backed constructor
     * above so uploads/downloads remain separate transport directories.
     */
    public StorageService(String uploadPath, String downloadPath, String glbPath,
                          String ignoredArchivePath) throws IOException {
        this(uploadPath, uploadPath, downloadPath, glbPath, downloadPath);
    }

    private static Path root(String value) {
        return Path.of(value).toAbsolutePath().normalize();
    }

    // --- UPLOAD -------------------------------------------------------------

    /**
     * Stores a completed multipart upload and moves it into the pipeline.
     *
     * <p>The pipeline never sees the multipart stream or a partially written
     * file. A temporary file is completed in {@code uploads}, renamed to the
     * client's validated filename, copied to a temporary file in the pipeline
     * input folder, and only then atomically published there. The staging file
     * is removed on success and on failure.</p>
     */
    public synchronized Path storeUpload(MultipartFile file) throws IOException {
        if (file == null || file.isEmpty()) {
            throw new IOException("Empty upload");
        }

        String filename = safeOriginalFilename(file.getOriginalFilename());
        String extension = extensionOf(filename);
        if (!ALLOWED_EXTENSIONS.contains(extension)) {
            throw new IOException("Unsupported image type");
        }

        Path staged = uploadDir.resolve(filename).normalize();
        Path target = pipelineInputDir.resolve(filename).normalize();
        validate(staged, uploadDir);
        validate(target, pipelineInputDir);
        if (Files.exists(staged) || Files.exists(target)) {
            throw new FileAlreadyExistsException(filename,
                    null, "An upload with this filename is already being processed");
        }

        Path uploadTemporary = Files.createTempFile(uploadDir, ".uploading-", ".tmp");
        try {
            try (var input = file.getInputStream()) {
                Files.copy(input, uploadTemporary, StandardCopyOption.REPLACE_EXISTING);
            }
            moveWithinDirectory(uploadTemporary, staged);

            publishCopy(staged, target);
            Files.deleteIfExists(staged);
            return target;
        } catch (IOException | RuntimeException error) {
            Files.deleteIfExists(uploadTemporary);
            Files.deleteIfExists(staged);
            Files.deleteIfExists(target);
            throw error;
        } finally {
            Files.deleteIfExists(uploadTemporary);
        }
    }

    private static String extensionOf(String filename) throws IOException {
        if (filename == null || filename.isBlank() || filename.indexOf('.') < 0) {
            throw new IOException("File extension missing");
        }
        String extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase(Locale.ROOT);
        if (extension.isBlank()) {
            throw new IOException("File extension missing");
        }
        return extension;
    }

    /**
     * Return the basename supplied by the multipart client after validating it
     * as a pipeline-safe filename. The pipeline uses the filename stem as the
     * model identifier, so changing it here would also change the download
     * identifier later on.
     */
    private static String safeOriginalFilename(String originalFilename) throws IOException {
        if (originalFilename == null || originalFilename.isBlank()
                || originalFilename.indexOf('\0') >= 0) {
            throw new IOException("File name missing");
        }

        // Browsers normally send a basename, but older multipart clients may
        // include a Windows or POSIX path. Keep only the basename in either
        // form; never allow a client path to influence filesystem resolution.
        String normalizedSeparators = originalFilename.replace('\\', '/');
        String filename = normalizedSeparators.substring(
                normalizedSeparators.lastIndexOf('/') + 1);
        if (filename.isBlank() || filename.equals(".") || filename.equals("..")
                || !filename.matches("[A-Za-z0-9._-]+")) {
            throw new IOException("Invalid file name");
        }
        return filename;
    }

    private void removeInterruptedUploadTemporaries() throws IOException {
        try (var files = Files.list(uploadDir)) {
            for (Path file : files.toList()) {
                if (Files.isRegularFile(file) && file.getFileName().toString().startsWith(".uploading-")) {
                    Files.deleteIfExists(file);
                }
            }
        }
    }

    // --- DOWNLOAD -----------------------------------------------------------

    /** Backwards-compatible view of a file already staged in downloads. */
    public Resource loadForDownload(String filename) throws IOException {
        requireSimpleName(filename);
        Path file = downloadDir.resolve(filename).normalize();
        validate(file, downloadDir);
        if (!Files.isRegularFile(file)) {
            throw new FileNotFoundException(filename);
        }
        return new UrlResource(file.toUri());
    }

    public boolean existsForDownload(String filename) {
        try {
            requireSimpleName(filename);
            Path file = downloadDir.resolve(filename).normalize();
            validate(file, downloadDir);
            return exists(file);
        } catch (RuntimeException error) {
            return false;
        }
    }

    /** Backwards-compatible FBX availability check. */
    public boolean existsForModel(String modelName) {
        return existsForAsset(modelName);
    }

    /** Backwards-compatible alias for the one-shot legacy FBX download. */
    public byte[] loadModelForDownloadAndDelete(String modelName) throws IOException {
        return loadAssetForDownloadAndDelete(modelName);
    }

    /** Backwards-compatible one-shot deletion for a staged download file. */
    public byte[] loadForDownloadAndDelete(String filename) throws IOException {
        requireSimpleName(filename);
        Path staged = downloadDir.resolve(filename).normalize();
        validate(staged, downloadDir);
        if (!Files.isRegularFile(staged)) {
            throw new FileNotFoundException(filename);
        }
        byte[] data = Files.readAllBytes(staged);
        Files.delete(staged);
        return data;
    }

    /**
     * Legacy endpoint support: serves a static GLB or the current FBX output
     * for a bare model name, then removes the delivered files.
     */
    public synchronized byte[] loadAssetForDownloadAndDelete(String name) throws IOException {
        requireSimpleName(name);
        if (name.toLowerCase(Locale.ROOT).endsWith(".glb")) {
            return readAndDelete(name, glbDir.resolve(name).normalize(), downloadDir.resolve(name).normalize());
        }

        Path source = animatedDir.resolve(name).resolve(ANIMATED_FBX_NAME).normalize();
        Path staged = downloadDir.resolve(name + ".fbx").normalize();
        return readAndDelete(name, source, staged);
    }

    /**
     * Serves the animation contract used by the Quest client:
     * {@code GET /files/animated/<id>.gltf}.
     */
    public synchronized byte[] loadAnimatedGltfForDownloadAndDelete(String modelName) throws IOException {
        requireSimpleName(modelName);
        Path source = animatedDir.resolve(modelName).resolve(ANIMATED_GLTF_NAME).normalize();
        Path staged = downloadDir.resolve(modelName + ".gltf").normalize();
        byte[] data = readAndDelete(modelName, source, staged);

        // Once the GLTF has been delivered, its FBX predecessor is no longer
        // part of the public output and must not accumulate on the server.
        Files.deleteIfExists(animatedDir.resolve(modelName).resolve(ANIMATED_FBX_NAME).normalize());
        deleteIfEmpty(animatedDir.resolve(modelName).normalize());
        return data;
    }

    public boolean existsForAsset(String name) {
        try {
            requireSimpleName(name);
            if (name.toLowerCase(Locale.ROOT).endsWith(".glb")) {
                return exists(glbDir.resolve(name).normalize()) || exists(downloadDir.resolve(name).normalize());
            }
            Path source = animatedDir.resolve(name).resolve(ANIMATED_FBX_NAME).normalize();
            Path staged = downloadDir.resolve(name + ".fbx").normalize();
            return exists(source) || exists(staged);
        } catch (RuntimeException error) {
            return false;
        }
    }

    public boolean existsForAnimatedGltf(String modelName) {
        try {
            requireSimpleName(modelName);
            Path source = animatedDir.resolve(modelName).resolve(ANIMATED_GLTF_NAME).normalize();
            Path staged = downloadDir.resolve(modelName + ".gltf").normalize();
            return exists(source) || exists(staged);
        } catch (RuntimeException error) {
            return false;
        }
    }

    private byte[] readAndDelete(String name, Path source, Path staged) throws IOException {
        Path sourceRoot = source.startsWith(animatedDir) ? animatedDir
                : source.startsWith(glbDir) ? glbDir : downloadDir;
        validate(source, sourceRoot);
        validate(staged, downloadDir);

        if (!source.equals(staged) && !Files.exists(staged)) {
            if (!Files.exists(source)) {
                throw new FileNotFoundException(name);
            }
            copyToDownload(source, staged);
        }

        if (!Files.exists(staged)) {
            throw new FileNotFoundException(name);
        }

        byte[] data = Files.readAllBytes(staged);
        Files.delete(staged);
        if (!source.equals(staged)) {
            Files.deleteIfExists(source);
        }
        return data;
    }

    private void copyToDownload(Path source, Path destination) throws IOException {
        Files.createDirectories(destination.getParent());
        Path temporary = Files.createTempFile(downloadDir, ".downloading-", ".tmp");
        try {
            Files.copy(source, temporary, StandardCopyOption.REPLACE_EXISTING);
            moveWithinDirectory(temporary, destination);
        } finally {
            Files.deleteIfExists(temporary);
        }
    }

    /** Copy to a temporary file in the destination directory, then publish it. */
    private void publishCopy(Path source, Path destination) throws IOException {
        Files.createDirectories(destination.getParent());
        Path temporary = Files.createTempFile(destination.getParent(), ".publishing-", ".tmp");
        try {
            Files.copy(source, temporary, StandardCopyOption.REPLACE_EXISTING);
            moveWithinDirectory(temporary, destination);
        } finally {
            Files.deleteIfExists(temporary);
        }
    }

    private static void moveWithinDirectory(Path source, Path destination) throws IOException {
        try {
            Files.move(source, destination, StandardCopyOption.ATOMIC_MOVE, StandardCopyOption.REPLACE_EXISTING);
        } catch (AtomicMoveNotSupportedException error) {
            Files.move(source, destination, StandardCopyOption.REPLACE_EXISTING);
        }
    }

    private static boolean exists(Path path) {
        return Files.isRegularFile(path);
    }

    private static void deleteIfEmpty(Path directory) throws IOException {
        if (Files.isDirectory(directory)) {
            try (var files = Files.list(directory)) {
                if (files.findAny().isEmpty()) {
                    Files.deleteIfExists(directory);
                }
            }
        }
    }

    private static void requireSimpleName(String name) {
        if (name == null || name.isBlank() || name.indexOf('\0') >= 0
                || name.contains("/") || name.contains("\\")
                || name.equals(".") || name.equals("..")
                || !name.matches("[A-Za-z0-9._-]+")) {
            throw new SecurityException("Invalid asset name");
        }
    }

    private static void validate(Path resolved, Path root) {
        if (!resolved.normalize().startsWith(root.normalize())) {
            throw new SecurityException("Invalid path access: " + resolved);
        }
    }
}
