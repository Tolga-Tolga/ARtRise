package com.example.vr_server_interface;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.nio.file.Path;


@RestController
@RequestMapping("/files")
public class FileController {

    private final StorageService storage;

    public FileController(StorageService storage) {
        this.storage = storage;
    }

    @PostMapping("/upload")
    public ResponseEntity<?> upload(@RequestParam("file") MultipartFile file) {
        try {
            Path saved = storage.storeUpload(file);
            return ResponseEntity.ok("Uploaded: " + saved.getFileName());
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }

    @GetMapping("/download/{name}")
    public ResponseEntity<byte[]> download(@PathVariable String name) {
        try {
            byte[] data = storage.loadAssetForDownloadAndDelete(name);
            String filename = name.toLowerCase().endsWith(".glb") ? name : "animate3d_model.fbx";
            String contentType = name.toLowerCase().endsWith(".glb")
                    ? "model/gltf-binary" : "application/octet-stream";

            return ResponseEntity.ok()
                    .header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=\"" + filename + "\"")
                    .header(HttpHeaders.CONTENT_TYPE, contentType)
                    .body(data);

        } catch (Exception e) {
            return ResponseEntity.notFound().build();
        }
    }

    /**
     * Progressive animation endpoint. The suffix is part of the public
     * contract so the Quest client can request the animated asset separately
     * from the early static GLB.
     */
    @GetMapping("/animated/{name:.+}")
    public ResponseEntity<byte[]> animated(@PathVariable String name) {
        String lowerName = name.toLowerCase(java.util.Locale.ROOT);
        if (!lowerName.endsWith(".gltf")) {
            return ResponseEntity.notFound().build();
        }

        String modelName = name.substring(0, name.length() - ".gltf".length());
        try {
            byte[] data = storage.loadAnimatedGltfForDownloadAndDelete(modelName);
            return ResponseEntity.ok()
                    .header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=\"" + name + "\"")
                    .contentType(MediaType.parseMediaType("model/gltf+json"))
                    .body(data);
        } catch (Exception e) {
            return ResponseEntity.notFound().build();
        }
    }
    
    @GetMapping("/exists/{name}")
    public ResponseEntity<Boolean> exists(@PathVariable String name) {
        try {
            return ResponseEntity.ok(storage.existsForAsset(name));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(false);
        }
    }
}
