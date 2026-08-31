# ARtRise Server

The ARtRise server receives card artwork from the Meta Quest client and turns it into progressively delivered 3D assets. A static GLB is published first; a self-contained animated GLTF follows when Animate3D has finished.

The backend combines a Spring Boot REST API with a filesystem-driven Python, Blender, and CUDA pipeline.

## Pipeline

```text
POST /files/upload (<cardId>.png)
  -> vertical image correction
  -> Real-ESRGAN upscaling
  -> rembg background removal
  -> TRELLIS image-to-3D
  -> static <cardId>.glb
  -> Blender GLB-to-OBJ conversion
  -> Animate3D FBX animation
  -> Blender self-contained <cardId>.gltf
```

The filename stem is the job and card identifier throughout the pipeline. Uploading `1.png` therefore produces `1.glb` and `1.gltf`.

## Requirements

- Linux or WSL2; the complete pipeline is Linux/CUDA oriented
- NVIDIA GPU, compatible driver, and CUDA toolkit
- A high-memory GPU for TRELLIS and Animate3D; approximately 24 GB VRAM may be required
- Conda or Miniforge
- Git, Bash, curl or wget, and unzip
- Java 17 for Spring Boot
- Blender installed separately and available through `BLENDER_EXECUTABLE`
- Sufficient disk space for model repositories, weights, Conda environments, and generated assets

The ML components require incompatible Python, PyTorch, and CUDA combinations. The installer therefore creates separate Conda environments:

```text
art-rise-base
art-rise-realesrgan
art-rise-rembg
art-rise-trellis
art-rise-animate3d
```

## Installation

Clone the repository with its submodules:

```bash
git clone --recurse-submodules <repository-url>
cd ARtRise
git submodule update --init --recursive
```

Run the server installer from Linux or WSL2:

```bash
cd server
chmod +x install.sh
./install.sh
```

The installer creates pipeline directories, installs the isolated environments, downloads required model weights, and builds the Spring Boot application. It is designed to reuse existing environments and downloads when run again.

Blender is intentionally not downloaded by the installer. Point the runtime to an existing installation:

```bash
export BLENDER_EXECUTABLE=/absolute/path/to/blender
```

Important optional installer settings include:

```bash
export CONDA_EXE=/absolute/path/to/conda
export ARTRISE_CUDA_HOME=/usr/local/cuda
export ARTRISE_REMBG_BACKEND=gpu       # cpu, gpu, or rocm
export ARTRISE_LOAD_CUDA_MODULE=0      # useful outside module-based clusters
```

See `install.sh` for additional version and environment overrides.

## Running the server

Start the processing supervisor from the `server` directory:

```bash
python scripts/run.py
```

This starts the central watcher and the persistent TRELLIS worker. The watcher launches image preparation, Real-ESRGAN, rembg, Blender, and Animate3D as their inputs become available.

Start the REST API in a second terminal:

```bash
cd server/webserver
SERVER_PORT=18082 ./mvnw spring-boot:run
```

Spring Boot binds to `0.0.0.0` and defaults to port `8080`. The Unity client defaults to port `18082`, so either use the command above or enter `8080` in the client connection dialog.

## REST API

All endpoints are below `/files`.

| Method | Endpoint | Behavior |
|---|---|---|
| `POST` | `/files/upload` | Accepts multipart field `file` and preserves its validated basename |
| `GET` | `/files/exists/{name}` | Checks a static `.glb` or the legacy bare-name FBX output |
| `GET` | `/files/download/{name}` | Downloads a static `.glb`; a bare name remains a legacy FBX route |
| `GET` | `/files/animated/{id}.gltf` | Returns the final self-contained animated GLTF, or `404` while unavailable |

Supported upload extensions are PNG, JPG/JPEG, BMP, and WEBP. Multipart uploads are limited to 200 MB.

Example:

```bash
curl --fail -F "file=@1.png" http://SERVER:18082/files/upload
curl --fail -o 1.glb http://SERVER:18082/files/download/1.glb
curl --fail -o 1.gltf http://SERVER:18082/files/animated/1.gltf
```

Downloads are one-shot: after a successful static or animated delivery, the corresponding public output is removed. A `404` can therefore mean that an asset is still processing, has failed, or has already been downloaded.

## Filesystem layout

```text
server/
|-- components/
|   |-- Animate3D/
|   |-- Real-ESRGAN/
|   |-- TRELLIS/
|   `-- rembg/
|-- pipeline/
|   |-- input/original_image/
|   |-- stages/image_preparation/
|   |-- stages/model_generation/generated_glb/
|   `-- output/
|       |-- static_models/
|       `-- animated_models/<cardId>/
|-- scripts/
|   |-- run.py
|   |-- watcher/watcher.py
|   `-- workers/
|-- webserver/
`-- install.sh
```

The watcher polls these directories once per second. They act as pipeline states; there is currently no database, durable message queue, retry store, or progress endpoint.

## Runtime configuration

The webserver accepts these path overrides:

```text
WEB_UPLOAD_DIR
WEB_DOWNLOAD_DIR
PIPELINE_INPUT_DIR
PIPELINE_GLB_DIR
PIPELINE_ANIMATED_DIR
PIPELINE_OUTPUT_DIR
```

The process supervisor and watcher also support environment overrides such as `CONDA_EXE`, `ARTRISE_TRELLIS_ENV`, `ARTRISE_REALESRGAN_ENV`, `ARTRISE_REMBG_ENV`, `ARTRISE_ANIMATE3D_ENV`, and `ANIMATE3D_SCRIPT`.

## Troubleshooting

### The Quest cannot connect

- Confirm that the API is listening on the same port entered in the client.
- Allow that TCP port through the host firewall.
- Use a LAN-reachable host address, not `localhost`.
- Keep the Quest and server on mutually reachable networks.

### Upload succeeds but no GLB appears

Check the supervisor output and inspect the pipeline stage directories to locate the stopped worker. Verify the Conda environment, CUDA compatibility, model weights, and free disk space.

### Static GLB works but animation never appears

Check that Blender is available through `BLENDER_EXECUTABLE`, the Animate3D checkpoint exists, and the `art-rise-animate3d` environment can load its CUDA extensions. The final expected file is:

```text
pipeline/output/animated_models/<cardId>/animate3d_model.gltf
```

## Security and licensing

The API currently has no authentication or TLS and is intended for a trusted development or laboratory network. Do not expose it directly to the public internet.

The root `LICENSE` covers ARtRise's own code. TRELLIS, Real-ESRGAN, Animate3D, rembg, Blender, model weights, and other dependencies retain their own licenses and usage conditions.
