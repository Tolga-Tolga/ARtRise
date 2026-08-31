import os
from pathlib import Path
import time
import sys

# The worker is named ``trellis.py`` and is executed by absolute path.  Python
# therefore places ``scripts/workers/TRELLIS`` before the repository on
# sys.path; without this insertion ``from trellis...`` resolves back to this
# worker file and reports that it is not a package.
SERVER_DIR = Path(__file__).resolve().parents[3]
TRELLIS_CODE_DIR = SERVER_DIR / "components" / "TRELLIS"
sys.path.insert(0, str(TRELLIS_CODE_DIR))

import torch
from PIL import Image
from trellis.pipelines import TrellisImageTo3DPipeline
from trellis.utils import render_utils, postprocessing_utils

os.environ['SPCONV_ALGO'] = 'native'

device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
print("Using device:", device)

# Loading pipeline
local_model_path = SERVER_DIR / "components" / "TRELLIS-image-large"
pipeline = TrellisImageTo3DPipeline.from_pretrained(local_model_path)
pipeline.cuda()

STOPFILE = SERVER_DIR / "STOP_PIPELINE"
INPUT_DIR = SERVER_DIR / "pipeline" / "stages" / "image_preparation" / "background_removed"
OUTPUT_DIR = SERVER_DIR / "pipeline" / "stages" / "model_generation" / "generated_glb"
INPUT_DIR.mkdir(parents=True, exist_ok=True)
OUTPUT_DIR.mkdir(parents=True, exist_ok=True)

def check_stop():
    if os.path.exists(STOPFILE):
        print("Detected stop file - stopping Python process...")
        sys.exit(0)

while True:
    check_stop()
    # Grabbing all PNGs in Folder
    files = [f for f in os.listdir(INPUT_DIR) if f.lower().endswith(".png")]
    
    for filename in files:
        path = INPUT_DIR / filename
        try:
            with Image.open(path).convert("RGB") as image:
                print("Opened picture:", filename)

            # start pipeline
            outputs = pipeline.run(
                image,
                seed=1,
            )


            # export GLB
            glb = postprocessing_utils.to_glb(
                outputs['gaussian'][0],
                outputs['mesh'][0],
                simplify=0.95,
                texture_size=1024,
            )
            name, ext = filename.split(".")
            glb_output_path = OUTPUT_DIR / name
            glb.export(f"{glb_output_path}.glb")
            
            # Remove file
            os.remove(path)
            print("Removed image:", filename)

        except Exception as e:
            print("Error with", filename, ":", e)

    # short break for cpu, otherwise cpu will run with 100%
    time.sleep(1)
