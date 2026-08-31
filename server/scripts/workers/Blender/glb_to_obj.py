import bpy
import os
import sys
import traceback
import datetime
import shutil
import numpy as np
from PIL import Image

# === Specify paths relative to the project directory ===
BASE_DIR = os.path.abspath(os.path.join(os.path.dirname(__file__), "../../.."))
INPUT_DIR = os.path.join(BASE_DIR, "pipeline", "stages", "model_generation", "generated_glb")
ANIMATE3D_DIR = os.path.join(BASE_DIR, "components", "Animate3D")
OUTPUT_DIR = os.path.join(ANIMATE3D_DIR, "data", "animate3d", "mesh", "obj_file")
GLB_OUTPUT_DIR = os.path.join(BASE_DIR, "pipeline", "output", "static_models")
WEB_DOWNLOAD_DIR = os.environ.get(
    "WEB_DOWNLOAD_DIR", os.path.join(BASE_DIR, "webserver", "downloads")
)
LOG_DIR = os.path.join(os.path.dirname(__file__), "logs")

# === Create folders if they are missing ===
os.makedirs(LOG_DIR, exist_ok=True)
os.makedirs(OUTPUT_DIR, exist_ok=True)
os.makedirs(GLB_OUTPUT_DIR, exist_ok=True)
os.makedirs(WEB_DOWNLOAD_DIR, exist_ok=True)

timestamp = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
summary_path = os.path.join(LOG_DIR, f"summary_{timestamp}.log")
converted, failed = [], []

def log(msg):
    print(msg)
    with open(summary_path, "a") as f:
        f.write(msg + "\n")

# === Find all GLB files in the input folder ===
glb_files = [f for f in os.listdir(INPUT_DIR) if f.lower().endswith(".glb")]
if not glb_files:
    log("Found no .glb files.")
    sys.exit(0)

log(f"Starting conversion of {len(glb_files)} files ...")

# === Processing loop ===
for glb_file in glb_files:
    name = os.path.splitext(glb_file)[0]
    glb_path = os.path.join(INPUT_DIR, glb_file)
    out_dir = os.path.join(OUTPUT_DIR, name)
    os.makedirs(out_dir, exist_ok=True)

    try:
        bpy.ops.wm.read_factory_settings(use_empty=True)
        bpy.ops.import_scene.gltf(filepath=glb_path)
        log(f"Import: {glb_file}")

        # === Extract embedded textures ===
        images = []
        for img in bpy.data.images:
            try:
                save_path = os.path.join(out_dir, img.name + ".png")
                img.filepath_raw = save_path
                img.save()
                images.append(save_path)
            except Exception as e:
                log(f"Error saving a texture: {e}")

        # === If there is only one texture → Split channels ===
        if len(images) == 1:
            atlas_path = images[0]
            atlas = Image.open(atlas_path).convert("RGBA")
            arr = np.array(atlas)

            Image.fromarray(arr[:,:,0]).save(os.path.join(out_dir, "texture_pbr.png"))
            Image.fromarray(arr[:,:,1]).save(os.path.join(out_dir, "texture_roughness.png"))
            Image.fromarray(arr[:,:,2]).save(os.path.join(out_dir, "texture_metallic.png"))
            Image.new("RGB", atlas.size, (255,255,255)).save(os.path.join(out_dir, "texture_diffuse.png"))
            Image.new("L", atlas.size, 128).save(os.path.join(out_dir, "texture_normal.png"))
            log("PBR atlas split.")
        else:
            # Multiple textures → Sort by name
            for file in images:
                l = os.path.basename(file).lower()
                dst = None
                if "base" in l or "diffuse" in l or "albedo" in l:
                    dst = "texture_diffuse.png"
                elif "metal" in l:
                    dst = "texture_metallic.png"
                elif "rough" in l:
                    dst = "texture_roughness.png"
                elif "normal" in l:
                    dst = "texture_normal.png"
                elif "pbr" in l:
                    dst = "texture_pbr.png"
                if dst:
                    os.rename(file, os.path.join(out_dir, dst))

            # Fill in missing textures
            for n in ["texture_diffuse.png","texture_metallic.png",
                      "texture_normal.png","texture_pbr.png","texture_roughness.png"]:
                p = os.path.join(out_dir, n)
                if not os.path.exists(p):
                    Image.new("RGB",(1024,1024),(255,255,255)).save(p)

        # === OBJ-Export ===
        obj_path = os.path.join(out_dir, "base.obj")
        bpy.ops.wm.obj_export(
            filepath=obj_path,
            export_materials=True,
            export_triangulated_mesh=True,
            export_normals=True,
            export_uv=True,
            export_colors=True,
            export_pbr_extensions=True,
            export_selected_objects=False
        )

        mtl_path = os.path.splitext(obj_path)[0] + ".mtl"
        if os.path.exists(mtl_path):
            shutil.move(mtl_path, os.path.join(out_dir, "base.mtl"))

        converted.append(glb_file)
        log(f"Converted {glb_file}.\n")

    except Exception as e:
        failed.append(glb_file)
        log(f"Conversion failed for {glb_file}: {e}")
        traceback.print_exc()

# === Publish and remove every input GLB, including failed conversions ===
for glb_file in glb_files:
    src = os.path.join(INPUT_DIR, glb_file)
    dst = os.path.join(GLB_OUTPUT_DIR, glb_file)
    try:
        shutil.copy2(src, dst)
        log(f"Published {glb_file} in {GLB_OUTPUT_DIR}")
        web_dst = os.path.join(WEB_DOWNLOAD_DIR, glb_file)
        shutil.copy2(src, web_dst)
        log(f"Published {glb_file} in {WEB_DOWNLOAD_DIR}")
    except Exception as e:
        log(f"Could not publish {glb_file}: {e}")
    finally:
        try:
            if os.path.exists(src):
                os.remove(src)
                log(f"Removed {glb_file} from input")
        except Exception as e:
            log(f"Could not remove {glb_file} from input: {e}")

# === Summary ===
log("\n=== SUMMARY ===")
log(f"Successful: {len(converted)} | Failed: {len(failed)}")
log(f"Logs: {LOG_DIR}")
