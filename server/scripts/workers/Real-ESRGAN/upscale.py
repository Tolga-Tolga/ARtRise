"""Upscale pipeline images with the bundled Real-ESRGAN component."""

import argparse
import subprocess
import sys
from pathlib import Path


SERVER_DIR = Path(__file__).resolve().parents[3]
REAL_ESRGAN_DIR = SERVER_DIR / "components" / "Real-ESRGAN"
INFERENCE_SCRIPT = REAL_ESRGAN_DIR / "inference_realesrgan.py"
DEFAULT_INPUT = SERVER_DIR / "pipeline" / "stages" / "image_preparation" / "flipped_image"
DEFAULT_OUTPUT = SERVER_DIR / "pipeline" / "stages" / "image_preparation" / "upscaled_image"


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--input", type=Path, default=DEFAULT_INPUT)
    parser.add_argument("--output", type=Path, default=DEFAULT_OUTPUT)
    parser.add_argument("--model", default="RealESRGAN_x4plus")
    parser.add_argument("--scale", type=float, default=4)
    parser.add_argument("--tile", type=int, default=0)
    parser.add_argument("--fp32", action="store_true")
    args = parser.parse_args()

    if not INFERENCE_SCRIPT.is_file():
        parser.error(f"Real-ESRGAN inference script not found: {INFERENCE_SCRIPT}")

    args.input = args.input.resolve()
    args.output = args.output.resolve()
    args.input.mkdir(parents=True, exist_ok=True)
    args.output.mkdir(parents=True, exist_ok=True)

    command = [
        sys.executable,
        str(INFERENCE_SCRIPT),
        "--input", str(args.input),
        "--output", str(args.output),
        "--model_name", args.model,
        "--outscale", str(args.scale),
        "--tile", str(args.tile),
        "--suffix", "",
    ]
    if args.fp32:
        command.append("--fp32")

    return_code = subprocess.run(command, cwd=REAL_ESRGAN_DIR, check=False).returncode
    if return_code == 0:
        for image_path in args.input.iterdir():
            if image_path.is_file() and image_path.suffix.lower() in {".png", ".jpg", ".jpeg", ".bmp", ".webp"}:
                image_path.unlink()
    return return_code


if __name__ == "__main__":
    raise SystemExit(main())
