package com.example.vr_server_interface;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.io.TempDir;
import org.springframework.mock.web.MockMultipartFile;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;

import java.nio.file.Files;
import java.nio.file.Path;

import static org.junit.jupiter.api.Assertions.assertArrayEquals;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

class StorageServiceTest {

    @TempDir
    Path tempDir;

    @Test
    void uploadIsMovedFromWebserverStagingIntoPipelineInput() throws Exception {
        Path uploads = tempDir.resolve("uploads");
        Path input = tempDir.resolve("pipeline/input/original_image");
        StorageService storage = service(uploads, tempDir.resolve("downloads"), input);

        byte[] payload = {1, 2, 3, 4};
        Path target = storage.storeUpload(new MockMultipartFile(
                "file", "-10.png", "image/png", payload));

        assertTrue(target.startsWith(input));
        assertEquals("-10.png", target.getFileName().toString());
        assertArrayEquals(payload, Files.readAllBytes(target));
        try (var files = Files.list(uploads)) {
            assertTrue(files.noneMatch(Files::isRegularFile), "uploads must be empty after hand-off");
        }
    }

    @Test
    void uploadRejectsANameThatIsAlreadyInThePipeline() throws Exception {
        Path uploads = tempDir.resolve("uploads");
        Path input = tempDir.resolve("pipeline/input/original_image");
        StorageService storage = service(uploads, tempDir.resolve("downloads"), input);
        Files.write(input.resolve("-10.png"), new byte[]{1});

        assertThrows(java.nio.file.FileAlreadyExistsException.class,
                () -> storage.storeUpload(new MockMultipartFile(
                        "file", "-10.png", "image/png", new byte[]{2})));
        assertArrayEquals(new byte[]{1}, Files.readAllBytes(input.resolve("-10.png")));
    }

    @Test
    void staticDownloadRemovesPipelineAndDownloadCopies() throws Exception {
        Path downloads = tempDir.resolve("downloads");
        Path glbDir = tempDir.resolve("pipeline/output/static_models");
        StorageService storage = service(tempDir.resolve("uploads"), downloads,
                tempDir.resolve("pipeline/input/original_image"), glbDir,
                tempDir.resolve("pipeline/output/animated_models"));

        Path source = glbDir.resolve("card.glb");
        byte[] payload = {9, 8, 7};
        Files.write(source, payload);

        assertTrue(storage.existsForAsset("card.glb"));
        assertArrayEquals(payload, storage.loadAssetForDownloadAndDelete("card.glb"));
        assertFalse(Files.exists(source));
        assertFalse(Files.exists(downloads.resolve("card.glb")));
    }

    @Test
    void animatedGltfEndpointRemovesGltfFbxAndDownloadCopy() throws Exception {
        Path downloads = tempDir.resolve("downloads");
        Path animatedDir = tempDir.resolve("pipeline/output/animated_models");
        String model = "model-123";
        Path modelDir = animatedDir.resolve(model);
        Files.createDirectories(modelDir);
        Files.writeString(modelDir.resolve("animate3d_model.gltf"), "{\"asset\":{}}\n");
        Files.write(modelDir.resolve("animate3d_model.fbx"), new byte[]{1});
        StorageService storage = service(tempDir.resolve("uploads"), downloads,
                tempDir.resolve("pipeline/input/original_image"),
                tempDir.resolve("pipeline/output/static_models"), animatedDir);

        assertTrue(storage.existsForAnimatedGltf(model));
        assertTrue(storage.loadAnimatedGltfForDownloadAndDelete(model).length > 0);
        assertFalse(Files.exists(modelDir.resolve("animate3d_model.gltf")));
        assertFalse(Files.exists(modelDir.resolve("animate3d_model.fbx")));
        assertFalse(Files.exists(downloads.resolve(model + ".gltf")));
    }

    @Test
    void animatedHttpRouteUsesTheGltfContract() throws Exception {
        Path downloads = tempDir.resolve("downloads");
        Path animatedDir = tempDir.resolve("pipeline/output/animated_models");
        String model = "model-456";
        Path modelDir = animatedDir.resolve(model);
        Files.createDirectories(modelDir);
        byte[] payload = "{\"asset\":{\"version\":\"2.0\"}}".getBytes();
        Files.write(modelDir.resolve("animate3d_model.gltf"), payload);

        StorageService storage = service(tempDir.resolve("uploads"), downloads,
                tempDir.resolve("pipeline/input/original_image"),
                tempDir.resolve("pipeline/output/static_models"), animatedDir);
        MockMvc mvc = MockMvcBuilders.standaloneSetup(new FileController(storage)).build();

        mvc.perform(get("/files/animated/" + model + ".gltf"))
                .andExpect(status().isOk())
                .andExpect(content().contentType("model/gltf+json"))
                .andExpect(content().bytes(payload));
        assertFalse(Files.exists(modelDir.resolve("animate3d_model.gltf")));
    }

    private StorageService service(Path uploads, Path downloads, Path input) throws Exception {
        return service(uploads, downloads, input,
                tempDir.resolve("pipeline/output/static_models"),
                tempDir.resolve("pipeline/output/animated_models"));
    }

    private StorageService service(Path uploads, Path downloads, Path input,
                                   Path glbDir, Path animatedDir) throws Exception {
        return new StorageService(uploads.toString(), input.toString(), downloads.toString(),
                glbDir.toString(), animatedDir.toString());
    }
}
