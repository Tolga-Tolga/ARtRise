"""Watch pipeline folders and start the next processing worker when needed."""

import argparse
import os
import shutil
import subprocess
import sys
import time
from pathlib import Path


SERVER_DIR = Path(__file__).resolve().parents[2]
PIPELINE_DIR = SERVER_DIR / "pipeline"
BLENDER_CONVERTER = SERVER_DIR / "shell" / "Blender" / "convert_glb.sh"
ANIMATE3D_INPUT = SERVER_DIR / "components" / "Animate3D" / "data" / "animate3d" / "mesh" / "obj_file"
FLIP_SCRIPT = SERVER_DIR / "scripts" / "workers" / "Flip_Image" / "flip_image.py"
UPSCALE_SCRIPT = SERVER_DIR / "scripts" / "workers" / "Real-ESRGAN" / "upscale.py"
REMBG_SCRIPT = SERVER_DIR / "scripts" / "workers" / "rembg" / "remove_background.py"
CONDA_EXE = os.environ.get("CONDA_EXE", "conda")

WATCHED_DIRECTORIES = {
    "original_image": PIPELINE_DIR / "input" / "original_image",
    "flipped_image": PIPELINE_DIR / "stages" / "image_preparation" / "flipped_image",
    "upscaled_image": PIPELINE_DIR / "stages" / "image_preparation" / "upscaled_image",
    "background_removed": PIPELINE_DIR / "stages" / "image_preparation" / "background_removed",
    "generated_glb": PIPELINE_DIR / "stages" / "model_generation" / "generated_glb",
    "animated_model": PIPELINE_DIR / "output" / "animated_models",
    "animate3d_obj": ANIMATE3D_INPUT,
}
RECURSIVE_WATCHES = {"animate3d_obj"}


def snapshot() -> dict[Path, tuple[int, int]]:
    """Return files and their size/mtime for all watched directories."""
    files = {}
    for name, directory in WATCHED_DIRECTORIES.items():
        directory.mkdir(parents=True, exist_ok=True)
        paths = directory.rglob("*") if name in RECURSIVE_WATCHES else directory.iterdir()
        for path in paths:
            if path.is_file():
                stat = path.stat()
                files[path] = (stat.st_size, stat.st_mtime_ns)
    return files


def pending_animate3d_models() -> list[Path]:
    """Return model directories that already contain a complete OBJ hand-off."""

    return sorted(
        model_dir
        for model_dir in ANIMATE3D_INPUT.iterdir()
        if model_dir.is_dir() and (model_dir / "base.obj").is_file()
    )


def handle_change(path: Path, change: str) -> None:
    """Log one filesystem change for the pipeline operator."""
    print(f"[{change}] {path.relative_to(SERVER_DIR)}", flush=True)


def run_blender_converter() -> subprocess.Popen | None:
    if not BLENDER_CONVERTER.is_file():
        print(f"Blender script not found: {BLENDER_CONVERTER}", flush=True)
        return None
    try:
        if os.name == "nt":
            executable = os.environ.get("BLENDER_EXECUTABLE", "blender")
            if shutil.which(executable) is None and not Path(executable).is_file():
                print(f"Blender executable not found: {executable}", flush=True)
                return None
            command = [executable, "--background", "--python", str(SERVER_DIR / "scripts" / "workers" / "Blender" / "glb_to_obj.py")]
        else:
            command = ["bash", str(BLENDER_CONVERTER)]
        return subprocess.Popen(command, cwd=SERVER_DIR)
    except OSError as error:
        print(f"Could not start Blender converter: {error}", flush=True)
        return None


def conda_command(environment: str | None, command: list[str]) -> list[str]:
    if not environment:
        return command
    return [CONDA_EXE, "run", "--no-capture-output", "-n", environment, *command]


def run_animate3d(script: Path, model_name: str, environment: str | None) -> subprocess.Popen | None:
    if not script.is_file():
        print(f"Animate3D script not found: {script}", flush=True)
        return None
    try:
        return subprocess.Popen(
            conda_command(environment, ["bash", str(script), model_name]), cwd=SERVER_DIR
        )
    except OSError as error:
        print(f"Could not start Animate3D script: {error}", flush=True)
        return None


def run_python_worker(script: Path, environment: str | None) -> subprocess.Popen | None:
    if not script.is_file():
        print(f"Worker script not found: {script}", flush=True)
        return None
    try:
        return subprocess.Popen(
            conda_command(environment, ["python", str(script)]), cwd=SERVER_DIR
        )
    except OSError as error:
        print(f"Could not start worker: {error}", flush=True)
        return None


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument(
        "--interval",
        type=float,
        default=1.0,
        help="Seconds between directory scans (default: 1.0)",
    )
    parser.add_argument("--flip-env", default=os.environ.get("ARTRISE_FLIP_ENV", "art-rise-base"))
    parser.add_argument("--realesrgan-env", default=os.environ.get("ARTRISE_REALESRGAN_ENV", "art-rise-realesrgan"))
    parser.add_argument("--rembg-env", default=os.environ.get("ARTRISE_REMBG_ENV", "art-rise-rembg"))
    parser.add_argument("--animate3d-env", default=os.environ.get("ARTRISE_ANIMATE3D_ENV", "art-rise-animate3d"))
    parser.add_argument(
        "--animate3d-script",
        type=Path,
        default=Path(os.environ.get(
            "ANIMATE3D_SCRIPT",
            SERVER_DIR / "scripts" / "workers" / "Animate_3D" / "idle.sh",
        )),
        help="Shell script used for Animate3D jobs.",
    )
    args = parser.parse_args()

    print("Pipeline watcher started.", flush=True)
    for name, directory in WATCHED_DIRECTORIES.items():
        directory.mkdir(parents=True, exist_ok=True)
        print(f"Watching {name}: {directory}", flush=True)

    previous = snapshot()
    blender_process = None
    animate3d_process = None
    flip_process = None
    upscale_process = None
    rembg_process = None
    image_extensions = {".png", ".jpg", ".jpeg", ".bmp", ".webp"}

    # Resume jobs that were handed off just before a watcher restart. In
    # particular, Animate3D stores each OBJ package below obj_file/<model>, so
    # this startup check complements the recursive snapshot above.
    pending_models = pending_animate3d_models()
    if pending_models:
        model_dir = pending_models[0]
        print(f"Found pending Animate3D model: {model_dir.name}", flush=True)
        animate3d_process = run_animate3d(
            args.animate3d_script.resolve(), model_dir.name, args.animate3d_env
        )

    # Likewise resume GLBs that already exist when the watcher starts. The
    # converter processes all files in the hand-off directory in one run.
    if any(path.is_file() and path.suffix.lower() == ".glb"
           for path in WATCHED_DIRECTORIES["generated_glb"].iterdir()):
        blender_process = run_blender_converter()

    try:
        while True:
            current = snapshot()

            for path in current.keys() - previous.keys():
                handle_change(path, "created")
                if (
                    path.parent == WATCHED_DIRECTORIES["original_image"]
                    and path.suffix.lower() in {".png", ".jpg", ".jpeg", ".bmp", ".webp"}
                    and (flip_process is None or flip_process.poll() is not None)
                ):
                    flip_process = run_python_worker(FLIP_SCRIPT, args.flip_env)
                if (
                    path.parent == WATCHED_DIRECTORIES["flipped_image"]
                    and path.suffix.lower() in {".png", ".jpg", ".jpeg", ".bmp", ".webp"}
                    and (upscale_process is None or upscale_process.poll() is not None)
                ):
                    upscale_process = run_python_worker(UPSCALE_SCRIPT, args.realesrgan_env)
                if (
                    path.parent == WATCHED_DIRECTORIES["upscaled_image"]
                    and path.suffix.lower() in image_extensions
                    and (rembg_process is None or rembg_process.poll() is not None)
                ):
                    rembg_process = run_python_worker(REMBG_SCRIPT, args.rembg_env)
                if (
                    path.parent == WATCHED_DIRECTORIES["generated_glb"]
                    and path.suffix.lower() == ".glb"
                    and (blender_process is None or blender_process.poll() is not None)
                ):
                    blender_process = run_blender_converter()
                if (
                    path.parent.parent == ANIMATE3D_INPUT
                    and path.name == "base.obj"
                    and (animate3d_process is None or animate3d_process.poll() is not None)
                ):
                    animate3d_process = run_animate3d(
                        args.animate3d_script.resolve(), path.parent.name, args.animate3d_env
                    )
            for path in previous.keys() - current.keys():
                handle_change(path, "removed")
            for path in current.keys() & previous.keys():
                if current[path] != previous[path]:
                    handle_change(path, "modified")

            if (
                (flip_process is None or flip_process.poll() is not None)
                and any(path.suffix.lower() in image_extensions for path in WATCHED_DIRECTORIES["original_image"].iterdir())
            ):
                flip_process = run_python_worker(FLIP_SCRIPT, args.flip_env)
            if (
                (upscale_process is None or upscale_process.poll() is not None)
                and any(path.suffix.lower() in image_extensions for path in WATCHED_DIRECTORIES["flipped_image"].iterdir())
            ):
                upscale_process = run_python_worker(UPSCALE_SCRIPT, args.realesrgan_env)
            if (
                (rembg_process is None or rembg_process.poll() is not None)
                and any(path.is_file() for path in WATCHED_DIRECTORIES["upscaled_image"].iterdir())
            ):
                rembg_process = run_python_worker(REMBG_SCRIPT, args.rembg_env)
            if (
                (blender_process is None or blender_process.poll() is not None)
                and any(path.is_file() and path.suffix.lower() == ".glb"
                        for path in WATCHED_DIRECTORIES["generated_glb"].iterdir())
            ):
                blender_process = run_blender_converter()
            pending_models = pending_animate3d_models()
            if (
                (animate3d_process is None or animate3d_process.poll() is not None)
                and pending_models
            ):
                model_dir = pending_models[0]
                animate3d_process = run_animate3d(
                    args.animate3d_script.resolve(), model_dir.name, args.animate3d_env
                )

            previous = current
            time.sleep(args.interval)
    except KeyboardInterrupt:
        print("Pipeline watcher stopped.", flush=True)
        if blender_process is not None and blender_process.poll() is None:
            blender_process.terminate()
        if animate3d_process is not None and animate3d_process.poll() is None:
            animate3d_process.terminate()
        if flip_process is not None and flip_process.poll() is None:
            flip_process.terminate()
        if upscale_process is not None and upscale_process.poll() is None:
            upscale_process.terminate()
        if rembg_process is not None and rembg_process.poll() is None:
            rembg_process.terminate()


if __name__ == "__main__":
    main()
