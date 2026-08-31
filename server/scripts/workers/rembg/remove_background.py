"""Remove image backgrounds with the rembg command-line interface."""

import argparse
import subprocess
from pathlib import Path


SERVER_DIR = Path(__file__).resolve().parents[3]
DEFAULT_INPUT = SERVER_DIR / "pipeline" / "stages" / "image_preparation" / "upscaled_image"
DEFAULT_OUTPUT = SERVER_DIR / "pipeline" / "stages" / "image_preparation" / "background_removed"


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--input", type=Path, default=DEFAULT_INPUT)
    parser.add_argument("--output", type=Path, default=DEFAULT_OUTPUT)
    args = parser.parse_args()
    args.input = args.input.resolve()
    args.output = args.output.resolve()
    args.input.mkdir(parents=True, exist_ok=True)
    args.output.mkdir(parents=True, exist_ok=True)

    # ``i`` is rembg's single-file command.  The pipeline stages are folders,
    # so use the folder command ``p`` to process every image in the batch.
    result = subprocess.run(
        ["rembg", "p", str(args.input), str(args.output)],
        cwd=SERVER_DIR,
        check=False,
    )
    if result.returncode == 0:
        for source in args.input.iterdir():
            if source.is_file():
                source.unlink()
    return result.returncode


if __name__ == "__main__":
    raise SystemExit(main())
