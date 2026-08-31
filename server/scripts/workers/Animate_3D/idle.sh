#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
SERVER_DIR="$(cd -- "${SCRIPT_DIR}/../../.." && pwd)"
COMPONENT_DIR="${SERVER_DIR}/components/Animate3D"
PYTHON="${PYTHON:-python3}"
MODEL_NAME="${1:?Usage: $0 <model_name>}"

# Animate3D's launch.py enables every GPU visible in the process environment.
# A pipeline job is independent and must run on one device; otherwise a Slurm
# allocation with multiple GPUs starts an unnecessary DDP job and can fail in
# the static-view matmul before the animation stage begins.  Keep the first
# allocated device (or an explicitly selected device when none was allocated).
if [[ -n "${CUDA_VISIBLE_DEVICES:-}" ]]; then
    export CUDA_VISIBLE_DEVICES="${CUDA_VISIBLE_DEVICES%%,*}"
else
    export CUDA_VISIBLE_DEVICES="${ARTRISE_ANIMATE3D_GPU:-0}"
fi

ANIMATION_GIF_ROOT="${COMPONENT_DIR}/outputs/animate3d/animation_gif"
ANIMATION_GIF_WORK_DIR="${ANIMATION_GIF_ROOT}/${MODEL_NAME}"
ANIMATION_GIF_PATH="${ANIMATION_GIF_ROOT}/idle-${MODEL_NAME}.gif"

# Native Animate3D extensions (notably tiny-cuda-nn) resolve CUDA libraries
# from the active Conda environment.  The installer creates canonical runtime
# aliases there for the hashed libraries shipped with the PyTorch wheel.
if [[ -n "${CONDA_PREFIX:-}" && -d "${CONDA_PREFIX}/lib" ]]; then
    export LD_LIBRARY_PATH="${CONDA_PREFIX}/lib${LD_LIBRARY_PATH:+:${LD_LIBRARY_PATH}}"
    # PyTorch 1.13.1 stores libnvrtc-builtins in its wheel-specific library
    # directory. Keep that directory as a fallback for NVRTC's runtime
    # dlopen, while preserving the canonical aliases in CONDA_PREFIX/lib.
    for torch_lib_dir in "${CONDA_PREFIX}"/lib/python*/site-packages/torch/lib; do
        if [[ -d "${torch_lib_dir}" ]]; then
            export LD_LIBRARY_PATH="${LD_LIBRARY_PATH:+${LD_LIBRARY_PATH}:}${torch_lib_dir}"
        fi
    done
fi
if [[ -n "${CUDA_HOME:-}" ]]; then
    for cuda_lib_dir in \
        "${CUDA_HOME}/lib64" \
        "${CUDA_HOME}/lib" \
        "${CUDA_HOME}/targets/x86_64-linux/lib"; do
        if [[ -d "${cuda_lib_dir}" ]]; then
            # Keep the canonical aliases in the Conda environment first;
            # toolkit directories are a fallback for libraries not bundled by
            # the PyTorch wheel (notably cuRAND).
            export LD_LIBRARY_PATH="${LD_LIBRARY_PATH:+${LD_LIBRARY_PATH}:}${cuda_lib_dir}"
        fi
    done
fi

if [[ "${MODEL_NAME}" == */* || "${MODEL_NAME}" == *..* ]]; then
    echo "Invalid model name: ${MODEL_NAME}" >&2
    exit 1
fi

cleanup() {
    echo "Cleaning temporary Animate3D data for ${MODEL_NAME}..."
    rm -rf -- \
        "${COMPONENT_DIR}/data/animate3d/mesh/obj_file/${MODEL_NAME}" \
        "${COMPONENT_DIR}/data/animate3d/mesh/converted_gaussian/${MODEL_NAME}.ply" \
        "${COMPONENT_DIR}/data/animate3d/mesh/converted_gaussian/${MODEL_NAME}.json" \
        "${COMPONENT_DIR}/data/animate3d/mesh/tracking_rgba_images/idle-${MODEL_NAME}" \
        "${COMPONENT_DIR}/outputs/animate3d/static_vis/${MODEL_NAME}" \
        "${COMPONENT_DIR}/outputs/animate3d/animation_images/idle-${MODEL_NAME}" \
        "${ANIMATION_GIF_WORK_DIR}" \
        "${ANIMATION_GIF_PATH}" \
        "${COMPONENT_DIR}/outputs/animate3d/mesh/${MODEL_NAME}" \
        "${COMPONENT_DIR}/outputs/animate3d/mesh_vis/${MODEL_NAME}"
}

trap cleanup EXIT

if [[ ! -d "${COMPONENT_DIR}" ]]; then
    echo "Animate3D component not found: ${COMPONENT_DIR}" >&2
    exit 1
fi

cd "${COMPONENT_DIR}"
# OmegaConf parses an unquoted numeric CLI value such as tag=5 as an integer,
# although threestudio's ExperimentConfig declares tag as a string. Keep the
# model identifier quoted in every launch command.
# Step1 mesh2gaussian: we provide a simple script to extract coarse gaussian model from mesh object.  This script typically yields beeter results when applied to generated mesh objects featuring evenly distributed vertices and faces.
# results saved to data/animate3d/mesh/converted_gaussian/
"${PYTHON}" tools/mesh_animation/mesh2gaussian.py \
    --input_obj data/animate3d/mesh/obj_file/$1/base.obj \
    --output_dir data/animate3d/mesh/converted_gaussian \
    --output_name $1

# Step2 rendering 4-views of the gaussian object: the renders serve as the image condition for mv-vdm. Note that the coordinate of the mesh and the pre-defined gaussian system might not be the same, so you should manually check the system.geometry.load_ply_cfg !!! The load_ply_cfg used here is set for the given example (objects from Rodin-Gen1 could use this cfg too).
# results saved to outputs/animate3d/static_vis/
"${PYTHON}" launch.py \
    --config custom/threestudio_animate3d/configs/visualize_four_view_static.yaml \
    --test \
    name="static_vis" \
    tag="'${MODEL_NAME}'" \
    system.prompt_processor.prompt="visualize" \
    system.geometry.geometry_convert_from="data/animate3d/mesh/converted_gaussian/$1.ply" \
    system.geometry.load_ply_cfg.rot_x_degree=90. \
    system.geometry.load_ply_cfg.rot_z_degree=90. \
    system.geometry.load_ply_cfg.scale_factor=0.76 

# Step3 mv-vdm inference. The upstream script derives the GIF filename from
# the prompt and does not support an --gif_name option. Use an isolated work
# directory, then publish the result under the stable pipeline filename.
# results saved to outputs/animate3d/animation_gif/
"${PYTHON}" inference.py \
    --config "configs/inference/inference.yaml" \
    --pretrained_unet_path "pretrained_models/animate3d_motion_modules.ckpt" \
    --W 256 \
    --H 256 \
    --L 16 \
    --N 4 \
    --ip_image_root "outputs/animate3d/static_vis/$1/save/images" \
    --ip_image_name "" \
    --prompt "A person standing still in a relaxed pose, subtle breathing and small natural movements" \
    --save_name "animate3d/animation_gif/${MODEL_NAME}"

shopt -s nullglob
generated_gifs=("${ANIMATION_GIF_WORK_DIR}"/*.gif)
shopt -u nullglob
if [[ "${#generated_gifs[@]}" -ne 1 ]]; then
    echo "Expected exactly one generated animation GIF in ${ANIMATION_GIF_WORK_DIR}; found ${#generated_gifs[@]}." >&2
    exit 1
fi
mv -- "${generated_gifs[0]}" "${ANIMATION_GIF_PATH}"
rmdir -- "${ANIMATION_GIF_WORK_DIR}" 2>/dev/null || true

# Step4 split the gif file to images and segment the foreground object
# results saved to outputs/animate3d/animation_images/
"${PYTHON}" tools/split_gif.py \
    --gif_path "${ANIMATION_GIF_PATH}" \
    --output_folder outputs/animate3d/animation_images

# results saved to data/animate3d/mesh/tracking_rgba_images/
cd tools/tracking_anything
"${PYTHON}" custom_inference.py \
    --folder_path ../../outputs/animate3d/animation_images/idle-$1 \
    --save_path ../../data/animate3d/mesh/tracking_rgba_images/idle-$1 \
    --template_mask_folder ../../outputs/animate3d/static_vis/$1/save/images

# Step5 Animate Mesh!
# results saved to outputs/animate3d/mesh
cd ../..
"${PYTHON}" launch.py \
    --config custom/threestudio_animate3d/configs/mesh_animation_frame_16.yaml  \
    --train \
    --gpu 0 \
    tag="'${MODEL_NAME}'" \
    system.prompt_processor.prompt="A human is idling." \
    system.geometry.geometry_convert_from="data/animate3d/mesh/converted_gaussian/$1.ply" \
    system.geometry.load_ply_cfg.rot_x_degree=90. \
    system.geometry.load_ply_cfg.rot_z_degree=90. \
    system.geometry.load_ply_cfg.scale_factor=0.76 \
    data.image_root="data/animate3d/mesh/tracking_rgba_images/idle-$1" \
    system.connected_vertices_info_path="data/animate3d/mesh/converted_gaussian/$1.json" 

# Step6 Visualize the mesh and save gaussian trajectory
# results saved to outputs/animate3d/mesh_vis
"${PYTHON}" launch.py \
    --config custom/threestudio_animate3d/configs/visualize_four_view_frame_16.yaml  \
    --test \
    --gpu 0 \
    name="mesh_vis" \
    tag="'${MODEL_NAME}'" \
    system.prompt_processor.prompt="visualize" \
    resume="outputs/animate3d/mesh/$1/ckpts/epoch=0-step=800.ckpt" \
    system.save_gaussian_trajectory=True 

# Step7 export animated mesh in fbx format
# results saved to outputs/animate3d/mesh_vis
"${PYTHON}" tools/mesh_animation/export_animated_mesh.py \
    --obj_dir data/animate3d/mesh/obj_file/$1 \
    --npy_dir outputs/animate3d/mesh_vis/$1/save/mesh_trajectory \
    --output_path outputs/animate3d/mesh_vis/$1/save/animate3d_model.fbx \
    --theta_x_degree 90. \
    --theta_z_degree 90. \
    --scale_factor 0.76

FINAL_FBX="${COMPONENT_DIR}/outputs/animate3d/mesh_vis/${MODEL_NAME}/save/animate3d_model.fbx"
ANIMATION_OUTPUT_DIR="${SERVER_DIR}/pipeline/output/animated_models/${MODEL_NAME}"
BLENDER_EXECUTABLE="${BLENDER_EXECUTABLE:-${SERVER_DIR}/components/Blender/blender}"
WEB_DOWNLOAD_DIR="${WEB_DOWNLOAD_DIR:-${SERVER_DIR}/webserver/downloads}"
mkdir -p "${ANIMATION_OUTPUT_DIR}"
cp -- "${FINAL_FBX}" "${ANIMATION_OUTPUT_DIR}/animate3d_model.fbx"

# The Quest client consumes a self-contained GLTF document. Keep the FBX as a
# local intermediate for compatibility, but publish the embedded GLTF as the
# progressive animation result served by /files/animated/<id>.gltf.
FINAL_GLTF="${ANIMATION_OUTPUT_DIR}/animate3d_model.gltf"
if [[ -x "${BLENDER_EXECUTABLE}" ]]; then
    BLENDER_COMMAND=("${BLENDER_EXECUTABLE}")
elif command -v "${BLENDER_EXECUTABLE}" >/dev/null 2>&1; then
    BLENDER_COMMAND=("${BLENDER_EXECUTABLE}")
else
    echo "Blender executable not found or not executable: ${BLENDER_EXECUTABLE}" >&2
    exit 1
fi
"${BLENDER_COMMAND[@]}" --background --python \
    "${SERVER_DIR}/scripts/workers/Blender/fbx_to_gltf.py" -- \
    "${FINAL_FBX}" "${FINAL_GLTF}"
mkdir -p "${WEB_DOWNLOAD_DIR}"
cp -- "${FINAL_GLTF}" "${WEB_DOWNLOAD_DIR}/${MODEL_NAME}.gltf"
