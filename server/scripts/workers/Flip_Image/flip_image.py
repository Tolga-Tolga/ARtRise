from pathlib import Path
from PIL import Image

# -------- KONFIGURATION --------
# Resolve from this file, so starting the watcher from another directory still
# uses the correct pipeline folders.
SERVER_DIR = Path(__file__).resolve().parents[3]
SOURCE_DIR = SERVER_DIR / "pipeline" / "input" / "original_image"
TARGET_DIR = SERVER_DIR / "pipeline" / "stages" / "image_preparation" / "flipped_image"
IMAGE_EXTENSIONS = {".png", ".jpg", ".jpeg", ".bmp", ".webp"}
# --------------------------------

def process_image(image_path: Path):
    target_path = TARGET_DIR / image_path.name

    try:
        with Image.open(image_path) as img:
            # verify() catches files that are still being written by the
            # producer. Re-open afterwards because verify() closes the image.
            img.verify()

        with Image.open(image_path) as img:
            flipped = img.transpose(Image.FLIP_TOP_BOTTOM)
            flipped.save(target_path)

        image_path.unlink()  # Remove the original
        print(f"Processed: {image_path.name}")

    except Exception as e:
        print(f"Error at {image_path.name}: {e}")

def main():
    SOURCE_DIR.mkdir(parents=True, exist_ok=True)
    TARGET_DIR.mkdir(parents=True, exist_ok=True)

    for file in sorted(SOURCE_DIR.iterdir()):
        if file.is_file() and file.suffix.lower() in IMAGE_EXTENSIONS:
            process_image(file)

if __name__ == "__main__":
    main()
