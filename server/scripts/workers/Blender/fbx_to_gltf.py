"""Convert an Animate3D FBX into one self-contained animated GLTF file.

The Unity client uses glTFast and the webserver exposes the resulting file at
``/files/animated/<model>.gltf``. Blender 4.5 only offers ``GLTF_SEPARATE``
and ``GLB`` export, so this script exports separate assets temporarily and
embeds their buffers and images into the JSON document before returning. The
webserver can then transfer and remove one file without leaving sidecars in
its downloads directory.
"""

import base64
import json
import sys
from pathlib import Path
from urllib.parse import unquote

import bpy


def arguments() -> tuple[Path, Path]:
    try:
        separator = sys.argv.index("--")
    except ValueError:
        separator = 0
    values = sys.argv[separator + 1:]
    if len(values) != 2:
        raise SystemExit("Usage: blender --background --python fbx_to_gltf.py -- INPUT.fbx OUTPUT.gltf")
    return Path(values[0]).resolve(), Path(values[1]).resolve()


def embed_uri(document: dict, key: str, base_dir: Path, mime_by_suffix: dict[str, str]) -> None:
    """Replace external GLTF URIs with data URIs and remove their sidecars."""

    for item in document.get(key, []):
        uri = item.get("uri")
        if not uri or uri.startswith("data:"):
            continue

        sidecar = (base_dir / unquote(uri)).resolve()
        if not sidecar.is_file() or base_dir not in sidecar.parents:
            raise RuntimeError(f"GLTF sidecar not found below {base_dir}: {uri}")
        mime = mime_by_suffix.get(sidecar.suffix.lower(), "application/octet-stream")
        encoded = base64.b64encode(sidecar.read_bytes()).decode("ascii")
        item["uri"] = f"data:{mime};base64,{encoded}"
        sidecar.unlink()


def make_self_contained(output_path: Path) -> None:
    document = json.loads(output_path.read_text(encoding="utf-8"))
    embed_uri(document, "buffers", output_path.parent, {
        ".bin": "application/octet-stream",
    })
    embed_uri(document, "images", output_path.parent, {
        ".png": "image/png",
        ".jpg": "image/jpeg",
        ".jpeg": "image/jpeg",
        ".webp": "image/webp",
    })
    output_path.write_text(json.dumps(document, separators=(",", ":")), encoding="utf-8")


def main() -> int:
    input_path, output_path = arguments()
    if not input_path.is_file():
        raise SystemExit(f"FBX input not found: {input_path}")
    output_path.parent.mkdir(parents=True, exist_ok=True)

    bpy.ops.wm.read_factory_settings(use_empty=True)
    result = bpy.ops.import_scene.fbx(filepath=str(input_path))
    if result != {'FINISHED'}:
        raise RuntimeError(f"Blender could not import {input_path}: {result}")

    result = bpy.ops.export_scene.gltf(
        filepath=str(output_path),
        export_format="GLTF_SEPARATE",
        export_animations=True,
        export_skins=True,
        export_materials="EXPORT",
        export_image_format="AUTO",
    )
    if result != {'FINISHED'}:
        raise RuntimeError(f"Blender could not export {output_path}: {result}")
    make_self_contained(output_path)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
