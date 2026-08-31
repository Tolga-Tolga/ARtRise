#!/usr/bin/env bash
set -euo pipefail

# ARtRise server installer
#
# Installs the complete server stack described in server/README.md. The
# machine-learning components intentionally live in separate Conda
# environments: Animate3D and TRELLIS require incompatible PyTorch/CUDA
# versions, while rembg has its own ONNX Runtime choices.
#
# The script is safe to run more than once. Existing Conda environments,
# model files, cloned extensions and pipeline data are reused. Large
# Animate3D example data is opt-in (see ARTRISE_DOWNLOAD_ANIMATE3D_TEST_DATA).

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
SERVER_DIR="${SCRIPT_DIR}"
REPO_ROOT="$(cd -- "${SERVER_DIR}/.." && pwd)"
COMPONENTS_DIR="${SERVER_DIR}/components"
PIPELINE_DIR="${SERVER_DIR}/pipeline"

ANIMATE3D_DIR="${COMPONENTS_DIR}/Animate3D"
TRELLIS_DIR="${COMPONENTS_DIR}/TRELLIS"
REAL_ESRGAN_DIR="${COMPONENTS_DIR}/Real-ESRGAN"
REMBG_DIR="${COMPONENTS_DIR}/rembg"

# The server-specific file adds Blender's package index to the upstream
# Animate3D requirements. Fall back to the component file for older checkouts.
if [[ -f "${SERVER_DIR}/requirements_Animate3D.txt" ]]; then
    ANIMATE3D_REQUIREMENTS="${SERVER_DIR}/requirements_Animate3D.txt"
else
    ANIMATE3D_REQUIREMENTS="${ANIMATE3D_DIR}/requirements.txt"
fi

BASE_ENV="${ARTRISE_BASE_ENV:-art-rise-base}"
REALESRGAN_ENV="${ARTRISE_REALESRGAN_ENV:-art-rise-realesrgan}"
TRELLIS_ENV="${ARTRISE_TRELLIS_ENV:-art-rise-trellis}"
ANIMATE3D_ENV="${ARTRISE_ANIMATE3D_ENV:-art-rise-animate3d}"
REMBG_ENV="${ARTRISE_REMBG_ENV:-art-rise-rembg}"

download_file() {
    local url="$1"
    local destination="$2"
    local partial="${destination}.part"

    if [[ -s "${destination}" ]]; then
        echo "Already present: ${destination}"
        return
    fi

    mkdir -p -- "$(dirname -- "${destination}")"
    echo "Downloading ${url}"
    if command -v curl >/dev/null 2>&1; then
        if [[ -s "${partial}" ]]; then
            echo "Resuming partial download: ${partial}"
            curl --fail --location --retry 3 --continue-at - --output "${partial}" "${url}"
        else
            curl --fail --location --retry 3 --output "${partial}" "${url}"
        fi
    elif command -v wget >/dev/null 2>&1; then
        wget --continue --output-document="${partial}" "${url}"
    else
        echo "Neither curl nor wget is available." >&2
        exit 1
    fi
    [[ -s "${partial}" ]] || { echo "Download was empty: ${url}" >&2; exit 1; }
    mv -- "${partial}" "${destination}"
}

if [[ -n "${CONDA_EXE:-}" ]]; then
    CONDA_COMMAND="${CONDA_EXE}"
elif command -v conda >/dev/null 2>&1; then
    CONDA_COMMAND="$(command -v conda)"
elif [[ -f "${HOME}/miniforge3/etc/profile.d/conda.sh" ]]; then
    # shellcheck disable=SC1091
    source "${HOME}/miniforge3/etc/profile.d/conda.sh"
    CONDA_COMMAND="$(command -v conda)"
else
    echo "Conda was not found. Install Miniforge/Conda or set CONDA_EXE." >&2
    exit 1
fi

CONDA_BASE_DIR="$(${CONDA_COMMAND} info --base)"
if [[ -f "${CONDA_BASE_DIR}/etc/profile.d/conda.sh" ]]; then
    # shellcheck disable=SC1091
    source "${CONDA_BASE_DIR}/etc/profile.d/conda.sh"
fi

# On HPC systems CUDA is commonly provided through environment modules. We
# try the documented module by default; set ARTRISE_LOAD_CUDA_MODULE=0 to keep
# an already configured workstation/toolkit untouched.
if [[ "${ARTRISE_LOAD_CUDA_MODULE:-1}" == "1" ]] && type module >/dev/null 2>&1; then
    module load "${ARTRISE_CUDA_MODULE:-devel/cuda/11.8}" || \
        echo "Warning: CUDA module could not be loaded; using the existing CUDA setup."
fi

configure_cuda_home() {
    local candidate="${ARTRISE_CUDA_HOME:-${CUDA_HOME:-}}"
    if [[ -n "${candidate}" ]]; then
        export CUDA_HOME="${candidate}"
        echo "Using CUDA_HOME=${CUDA_HOME}"
        return
    fi

    if command -v nvcc >/dev/null 2>&1; then
        local nvcc_path
        nvcc_path="$(readlink -f -- "$(command -v nvcc)")"
        export CUDA_HOME="$(cd -- "$(dirname -- "${nvcc_path}")/.." && pwd)"
        echo "Detected CUDA_HOME=${CUDA_HOME} from nvcc"
    elif [[ -d /usr/local/cuda ]]; then
        export CUDA_HOME=/usr/local/cuda
        echo "Detected CUDA_HOME=${CUDA_HOME}"
    else
        echo "CUDA toolkit (nvcc) not found. Native extensions will need ARTRISE_CUDA_HOME or a CUDA module." >&2
    fi
}

configure_cuda_home

# Native PyTorch extensions otherwise compile every architecture supported by
# the installed toolkit (for example, both sm_80 and sm_90 on an A100 node).
# Restrict builds to the local GPU by default; callers can provide a
# comma-separated list through ARTRISE_TORCH_CUDA_ARCH_LIST when portability
# across several GPU generations is desired.
if [[ -z "${TORCH_CUDA_ARCH_LIST:-}" ]]; then
    gpu_arch=""
    if command -v nvidia-smi >/dev/null 2>&1; then
        gpu_arch="$(nvidia-smi --query-gpu=compute_cap --format=csv,noheader 2>/dev/null | head -n 1 | tr -d '[:space:]' || true)"
    fi
    export TORCH_CUDA_ARCH_LIST="${ARTRISE_TORCH_CUDA_ARCH_LIST:-${gpu_arch:-8.0}}"
    echo "Using TORCH_CUDA_ARCH_LIST=${TORCH_CUDA_ARCH_LIST}"
fi
if [[ -z "${FLASH_ATTN_CUDA_ARCHS:-}" ]]; then
    # flash-attn uses its own semicolon-separated architecture variable and
    # otherwise builds a much larger default set than the target GPU needs.
    flash_attn_archs="${TORCH_CUDA_ARCH_LIST//,/;}"
    flash_attn_archs="${flash_attn_archs//./}"
    export FLASH_ATTN_CUDA_ARCHS="${ARTRISE_FLASH_ATTN_CUDA_ARCHS:-${flash_attn_archs}}"
    echo "Using FLASH_ATTN_CUDA_ARCHS=${FLASH_ATTN_CUDA_ARCHS}"
fi

conda_env_exists() {
    "${CONDA_COMMAND}" env list | awk '{print $1}' | grep -Fxq "$1"
}

ensure_env() {
    local environment="$1"
    local python_version="$2"
    if ! conda_env_exists "${environment}"; then
        echo "Creating Conda environment ${environment} (Python ${python_version})"
        "${CONDA_COMMAND}" create -y -n "${environment}" \
            --override-channels -c conda-forge "python=${python_version}"
    else
        echo "Conda environment already present: ${environment}"
    fi
}

conda_run() {
    local environment="$1"
    shift
    "${CONDA_COMMAND}" run --no-capture-output -n "${environment}" "$@"
}

link_animate3d_torch_cuda_runtime() {
    # The CUDA-enabled PyTorch wheels bundle their runtime libraries below
    # site-packages/torch/lib using collision-safe filenames such as
    # libnvrtc-d833c4f3.so.11.2.  Animate3D's tiny-cuda-nn extension is linked
    # against canonical SONAMEs (libnvrtc.so.11.2 and libcudart.so.11.0), so
    # the dynamic loader cannot resolve them through the wheel's hashed
    # filenames alone. NVRTC also loads libnvrtc-builtins.so.11.7 at runtime.
    # bitsandbytes additionally needs the CUDA BLAS, cuRAND, and cuSPARSE
    # SONAMEs, which are supplied by the CUDA toolkit rather than this older
    # PyTorch wheel.
    # Provide canonical aliases in the Conda environment. This avoids relying
    # on a module remaining loaded in the shell that starts the pipeline while
    # still leaving the NVIDIA driver (libcuda.so.1) to the GPU node.
    local env_prefix
    local torch_lib
    local env_lib
    env_prefix="$(conda_run "${ANIMATE3D_ENV}" python -c 'import sys; print(sys.prefix)')"
    torch_lib="$(conda_run "${ANIMATE3D_ENV}" python -c 'from pathlib import Path; import torch; print(Path(torch.__file__).resolve().parent / "lib")')"
    env_lib="${env_prefix}/lib"
    mkdir -p -- "${env_lib}"

    local soname source
    for soname in \
        libnvrtc.so.11.2 \
        libnvrtc-builtins.so.11.7 \
        libcudart.so.11.0 \
        libcublas.so.11 \
        libcublasLt.so.11 \
        libcurand.so.10 \
        libcusparse.so.11; do
        if [[ -e "${env_lib}/${soname}" ]]; then
            echo "CUDA runtime already available: ${env_lib}/${soname}"
            continue
        fi

        case "${soname}" in
            libnvrtc.so.11.2)
                source="$(find "${torch_lib}" -maxdepth 1 -type f -name 'libnvrtc*.so.11.2' -print -quit)"
                ;;
            libnvrtc-builtins.so.11.7)
                source="$(find "${torch_lib}" -maxdepth 1 -type f -name 'libnvrtc-builtins.so.11.7*' -print -quit)"
                ;;
            libcudart.so.11.0)
                source="$(find "${torch_lib}" -maxdepth 1 -type f -name 'libcudart*.so.11.0' -print -quit)"
                ;;
            libcublas.so.11)
                source="$(find "${torch_lib}" -maxdepth 1 -type f -name 'libcublas.so.11*' -print -quit)"
                ;;
            libcublasLt.so.11)
                source="$(find "${torch_lib}" -maxdepth 1 -type f -name 'libcublasLt.so.11*' -print -quit)"
                ;;
            libcurand.so.10)
                source="$(find "${torch_lib}" -maxdepth 1 -type f -name 'libcurand*.so.10*' -print -quit)"
                ;;
            libcusparse.so.11)
                source="$(find "${torch_lib}" -maxdepth 1 -type f -name 'libcusparse*.so.11*' -print -quit)"
                ;;
        esac

        # CUDA toolkit libraries not bundled by the PyTorch wheel are resolved
        # from the active toolkit. This also provides the fallback for a
        # library whose wheel filename cannot be mapped to a canonical SONAME.
        if [[ -z "${source}" && -n "${CUDA_HOME:-}" ]]; then
            case "${soname}" in
                libcublas.so.11) cuda_pattern='libcublas.so.11*' ;;
                libcublasLt.so.11) cuda_pattern='libcublasLt.so.11*' ;;
                libcusparse.so.11) cuda_pattern='libcusparse.so.11*' ;;
                *) cuda_pattern="${soname}*" ;;
            esac
            for cuda_lib_dir in \
                "${CUDA_HOME}/lib64" \
                "${CUDA_HOME}/lib" \
                "${CUDA_HOME}/targets/x86_64-linux/lib"; do
                [[ -d "${cuda_lib_dir}" ]] || continue
                for candidate in "${cuda_lib_dir}"/${cuda_pattern}; do
                    if [[ -e "${candidate}" ]]; then
                        source="${candidate}"
                        break 2
                    fi
                done
            done
        fi

        if [[ -z "${source}" ]]; then
            echo "Required CUDA library ${soname} was not found in ${torch_lib} or CUDA_HOME." >&2
            echo "Install a CUDA ${ARTRISE_ANIMATE3D_CUDA_VERSION:-11.7} runtime or set up the CUDA module before running Animate3D." >&2
            return 1
        fi

        ln -s -- "${source}" "${env_lib}/${soname}"
        echo "Linked ${env_lib}/${soname} -> ${source}"
    done
}

require_cuda_home() {
    if [[ -z "${CUDA_HOME:-}" || ! -x "${CUDA_HOME}/bin/nvcc" ]]; then
        echo "A CUDA toolkit with nvcc is required for ${1}. Set ARTRISE_CUDA_HOME or load a CUDA module before running install.sh." >&2
        return 1
    fi
}

patch_animate3d_config_types() {
    local config_file="${ANIMATE3D_DIR}/threestudio/utils/config.py"
    if [[ ! -f "${config_file}" ]]; then
        echo "Animate3D config module not found: ${config_file}" >&2
        return 1
    fi

    # OmegaConf infers a numeric CLI value such as tag=5 as int, even though
    # ExperimentConfig declares tag as str. The upstream __post_init__ then
    # attempts int + timestamp(str). Make the conversion explicit so direct
    # Animate3D invocations are safe as well as the ARtRise worker.
    conda_run "${ANIMATE3D_ENV}" python - "${config_file}" <<'PY'
from pathlib import Path
import sys

path = Path(sys.argv[1])
text = path.read_text(encoding="utf-8")
old = "        self.trial_name = self.tag"
new = "        self.trial_name = str(self.tag)"
if new in text:
    print(f"Animate3D config already patched: {path}")
elif old in text:
    path.write_text(text.replace(old, new, 1), encoding="utf-8")
    print(f"Patched Animate3D config: {path}")
else:
    raise SystemExit(f"Could not locate the trial_name assignment in {path}")
PY
}

install_submodules() {
    if [[ -e "${ANIMATE3D_DIR}/.git" ]]; then
        echo "==> Repository submodules already initialised"
    else
        echo "==> Initialising repository submodules"
        git -C "${REPO_ROOT}" submodule update --init --recursive
    fi
}

create_pipeline_directories() {
    echo "==> Creating ARtRise pipeline directories"
    mkdir -p \
        "${PIPELINE_DIR}/input/original_image" \
        "${PIPELINE_DIR}/stages/image_preparation/flipped_image" \
        "${PIPELINE_DIR}/stages/image_preparation/upscaled_image" \
        "${PIPELINE_DIR}/stages/image_preparation/background_removed" \
        "${PIPELINE_DIR}/stages/model_generation/generated_glb" \
        "${PIPELINE_DIR}/output/animated_models" \
        "${PIPELINE_DIR}/output/static_models" \
        "${PIPELINE_DIR}/stages/animation"
}

create_webserver_directories() {
    echo "==> Creating webserver transport directories"
    # Uploads/downloads are deliberately separate from the pipeline folders:
    # StorageService moves completed uploads into pipeline/input and removes
    # delivered assets from downloads after the HTTP response is prepared.
    mkdir -p \
        "${SERVER_DIR}/webserver/uploads" \
        "${SERVER_DIR}/webserver/downloads"
}

create_animate3d_directories() {
    echo "==> Creating Animate3D runtime directory structure"

    # Keep the layout used by the upstream Animate3D examples and by the
    # ARtRise worker.  The directories are intentionally created even when
    # the optional example archive is not downloaded: mesh conversion,
    # tracking and animation jobs write into these locations at runtime.
    mkdir -p \
        "${ANIMATE3D_DIR}/custom" \
        "${ANIMATE3D_DIR}/data/animate3d/mesh/converted_gaussian" \
        "${ANIMATE3D_DIR}/data/animate3d/mesh/obj_file" \
        "${ANIMATE3D_DIR}/data/animate3d/mesh/tracking_rgba_images" \
        "${ANIMATE3D_DIR}/data/animate3d/testset/pretrained_gaussian" \
        "${ANIMATE3D_DIR}/data/animate3d/testset/tracking_rgba_images" \
        "${ANIMATE3D_DIR}/data/vdm/examples/images" \
        "${ANIMATE3D_DIR}/data/vdm/meta" \
        "${ANIMATE3D_DIR}/outputs/animate3d/animation_gif" \
        "${ANIMATE3D_DIR}/outputs/animate3d/animation_images" \
        "${ANIMATE3D_DIR}/outputs/animate3d/data/animate3d" \
        "${ANIMATE3D_DIR}/outputs/animate3d/mesh" \
        "${ANIMATE3D_DIR}/outputs/animate3d/mesh_vis" \
        "${ANIMATE3D_DIR}/outputs/animate3d/static_vis" \
        "${ANIMATE3D_DIR}/tools/tracking_anything/checkpoints" \
        "${ANIMATE3D_DIR}/pretrained_models"

    # The upstream tree treats custom as a Python package.  Keep an empty
    # marker for clean checkouts where the directory was just created.
    if [[ ! -e "${ANIMATE3D_DIR}/custom/__init__.py" ]]; then
        touch "${ANIMATE3D_DIR}/custom/__init__.py"
    fi
}

install_base() {
    echo "==> Installing base image-processing environment (${BASE_ENV})"
    ensure_env "${BASE_ENV}" "${ARTRISE_BASE_PYTHON:-3.10}"
    conda_run "${BASE_ENV}" python -m pip install --upgrade pip
    conda_run "${BASE_ENV}" python -m pip install Pillow
}

install_realesrgan() {
    echo "==> Installing Real-ESRGAN (${REALESRGAN_ENV})"
    ensure_env "${REALESRGAN_ENV}" "${ARTRISE_REALESRGAN_PYTHON:-3.10}"

    # Use the CUDA 11.8 build by default. Versions/indexes can be overridden
    # for a different driver or a CPU-only development machine.
    local torch_version="${ARTRISE_REALESRGAN_TORCH_VERSION:-2.4.0}"
    local torchvision_version="${ARTRISE_REALESRGAN_TORCHVISION_VERSION:-0.19.0}"
    local cuda_version="${ARTRISE_REALESRGAN_CUDA_VERSION:-11.8}"
    "${CONDA_COMMAND}" install -y -n "${REALESRGAN_ENV}" \
        --override-channels -c pytorch -c nvidia -c conda-forge \
        "pytorch=${torch_version}" "torchvision=${torchvision_version}" \
        "pytorch-cuda=${cuda_version}"

    conda_run "${REALESRGAN_ENV}" python -m pip install --upgrade pip
    conda_run "${REALESRGAN_ENV}" python -m pip install -r "${REAL_ESRGAN_DIR}/requirements.txt"
    conda_run "${REALESRGAN_ENV}" python -m pip install --no-deps -e "${REAL_ESRGAN_DIR}"

    # BasicSR 1.4.2 still imports the pre-0.17 torchvision module name
    # `transforms.functional_tensor`. Recent torchvision releases keep the
    # implementation under `_functional_tensor`; add the compatibility alias
    # so the pinned Real-ESRGAN dependency remains importable.
    conda_run "${REALESRGAN_ENV}" python -c '
from pathlib import Path
import torchvision.transforms as transforms

compatibility_module = Path(transforms.__file__).with_name("functional_tensor.py")
if not compatibility_module.exists():
    compatibility_module.write_text("from ._functional_tensor import *\n", encoding="utf-8")
'

    mkdir -p "${REAL_ESRGAN_DIR}/weights"
    download_file \
        "${ARTRISE_REALESRGAN_MODEL_URL:-https://github.com/xinntao/Real-ESRGAN/releases/download/v0.1.0/RealESRGAN_x4plus.pth}" \
        "${REAL_ESRGAN_DIR}/weights/RealESRGAN_x4plus.pth"

    if [[ "${ARTRISE_DOWNLOAD_ANIME_ESRGAN:-0}" == "1" ]]; then
        download_file \
            "https://github.com/xinntao/Real-ESRGAN/releases/download/v0.2.2.4/RealESRGAN_x4plus_anime_6B.pth" \
            "${REAL_ESRGAN_DIR}/weights/RealESRGAN_x4plus_anime_6B.pth"
    fi
}

install_rembg() {
    echo "==> Installing rembg (${REMBG_ENV})"
    ensure_env "${REMBG_ENV}" "${ARTRISE_REMBG_PYTHON:-3.11}"
    conda_run "${REMBG_ENV}" python -m pip install --upgrade pip

    local backend="${ARTRISE_REMBG_BACKEND:-cpu}"
    case "${backend}" in
        cpu|gpu|rocm) ;;
        *) echo "ARTRISE_REMBG_BACKEND must be cpu, gpu or rocm (got ${backend})." >&2; exit 1 ;;
    esac
    (cd "${REMBG_DIR}" && conda_run "${REMBG_ENV}" python -m pip install ".[${backend},cli]")
}

install_trellis() {
    echo "==> Installing TRELLIS (${TRELLIS_ENV})"
    ensure_env "${TRELLIS_ENV}" "${ARTRISE_TRELLIS_PYTHON:-3.10}"
    "${CONDA_COMMAND}" install -y -n "${TRELLIS_ENV}" \
        --override-channels -c pytorch -c nvidia -c conda-forge \
        "pytorch=${ARTRISE_TRELLIS_TORCH_VERSION:-2.4.0}" \
        "torchvision=${ARTRISE_TRELLIS_TORCHVISION_VERSION:-0.19.0}" \
        "pytorch-cuda=${ARTRISE_TRELLIS_CUDA_VERSION:-11.8}"

    # setup.sh is designed to be sourced. We create the named environment
    # above, then run the switches from the upstream README without
    # --new-env (which would create a separate `trellis` environment).
    local flags_string="${ARTRISE_TRELLIS_FLAGS---basic --xformers --flash-attn --diffoctreerast --spconv --mipgaussian --kaolin --nvdiffrast}"
    read -r -a trellis_flags <<< "${flags_string}"
    local setup_flags=()
    local install_flash_attn=0
    local install_nvdiffrast=0
    local install_diffoctreerast=0
    local install_mipgaussian=0
    for flag in "${trellis_flags[@]}"; do
        case "${flag}" in
            --flash-attn)
                # TRELLIS' setup.sh invokes pip without --no-build-isolation.
                # Its PEP-517 metadata build therefore cannot import the
                # already installed torch package.
                install_flash_attn=1 ;;
            --nvdiffrast) install_nvdiffrast=1 ;;
            --diffoctreerast) install_diffoctreerast=1 ;;
            --mipgaussian) install_mipgaussian=1 ;;
            *) setup_flags+=("${flag}") ;;
        esac
    done
    if [[ " ${flags_string} " == *"--flash-attn"* || " ${flags_string} " == *"--diffoctreerast"* \
        || " ${flags_string} " == *"--mipgaussian"* || " ${flags_string} " == *"--nvdiffrast"* ]]; then
        require_cuda_home "TRELLIS native extensions"
    fi
    (cd "${TRELLIS_DIR}" && \
        conda_run "${TRELLIS_ENV}" bash -c 'set -e; cd "$1"; shift; source ./setup.sh "$@"' _ "${TRELLIS_DIR}" "${setup_flags[@]}")

    if (( install_flash_attn )); then
        echo "Installing flash-attn without PEP-517 build isolation"
        # flash-attn's setup.py imports psutil while generating metadata.
        # Because build isolation is deliberately disabled, provide that
        # small build-time dependency in the target environment explicitly.
        conda_run "${TRELLIS_ENV}" python -m pip install --upgrade psutil
        conda_run "${TRELLIS_ENV}" python -m pip install --no-build-isolation \
            "${ARTRISE_FLASH_ATTN_VERSION:-flash-attn}"
    fi

    # The remaining TRELLIS CUDA extensions also need torch during their
    # build. Install them one by one, explicitly disabling pip's isolated
    # build environment. Keeping the checkouts below the server tree makes
    # reruns idempotent and avoids relying on a node-local /tmp directory.
    local extensions_dir="${PIPELINE_DIR}/trellis-extensions"
    mkdir -p "${extensions_dir}"
    if (( install_nvdiffrast )); then
        local nvdiffrast_dir="${extensions_dir}/nvdiffrast"
        if [[ ! -d "${nvdiffrast_dir}/.git" ]]; then
            git clone https://github.com/NVlabs/nvdiffrast.git "${nvdiffrast_dir}"
        fi
        conda_run "${TRELLIS_ENV}" python -m pip install --no-build-isolation "${nvdiffrast_dir}"
    fi
    if (( install_diffoctreerast )); then
        local diffoctreerast_dir="${extensions_dir}/diffoctreerast"
        if [[ ! -d "${diffoctreerast_dir}/.git" ]]; then
            git clone --recurse-submodules https://github.com/JeffreyXiang/diffoctreerast.git "${diffoctreerast_dir}"
        fi
        conda_run "${TRELLIS_ENV}" python -m pip install --no-build-isolation "${diffoctreerast_dir}"
    fi
    if (( install_mipgaussian )); then
        local mipgaussian_dir="${extensions_dir}/mip-splatting"
        if [[ ! -d "${mipgaussian_dir}/.git" ]]; then
            git clone --recurse-submodules https://github.com/autonomousvision/mip-splatting.git "${mipgaussian_dir}"
        fi
        conda_run "${TRELLIS_ENV}" python -m pip install --no-build-isolation \
            "${mipgaussian_dir}/submodules/diff-gaussian-rasterization"
    fi

    conda_run "${TRELLIS_ENV}" python -m pip install --upgrade huggingface_hub
    # TRELLIS-image-large is a separate Hugging Face model repository, not
    # part of the TRELLIS code submodule. Keep it as a sibling so the worker's
    # `components/TRELLIS-image-large` path stays independent of vendor code.
    local model_dir="${COMPONENTS_DIR}/TRELLIS-image-large"
    local model_revision="${ARTRISE_TRELLIS_MODEL_REVISION:-main}"
    mkdir -p "${model_dir}"
    conda_run "${TRELLIS_ENV}" python -c \
        "from huggingface_hub import snapshot_download; snapshot_download(repo_id='microsoft/TRELLIS-image-large', revision='${model_revision}', local_dir=r'${model_dir}')"
}

install_animate3d() {
    echo "==> Installing Animate3D (${ANIMATE3D_ENV})"
    ensure_env "${ANIMATE3D_ENV}" "${ARTRISE_ANIMATE3D_PYTHON:-3.10}"
    require_cuda_home "Animate3D native extensions"

    local custom_dir="${ANIMATE3D_DIR}/custom"
    local animate_plugin_dir="${custom_dir}/threestudio_animate3d"
    local legacy_animate_plugin_dir="${custom_dir}/threestudio-animate3d"
    if [[ ! -e "${animate_plugin_dir}" && -e "${legacy_animate_plugin_dir}" ]]; then
        echo "Migrating Animate3D plugin directory to ${animate_plugin_dir}"
        mv -- "${legacy_animate_plugin_dir}" "${animate_plugin_dir}"
    fi
    if [[ ! -d "${animate_plugin_dir}" ]]; then
        echo "Animate3D plugin is missing: ${animate_plugin_dir}" >&2
        exit 1
    fi
    patch_animate3d_config_types

    conda_run "${ANIMATE3D_ENV}" python -m pip install --upgrade pip
    conda_run "${ANIMATE3D_ENV}" python -m pip install \
        torch==1.13.1+cu117 torchvision==0.14.1+cu117 torchaudio==0.13.1 \
        --extra-index-url https://download.pytorch.org/whl/cu117
    link_animate3d_torch_cuda_runtime
    conda_run "${ANIMATE3D_ENV}" python -m pip install \
        "setuptools==69.5.1" wheel ninja packaging pybind11
    local requirements_args=(-r "${ANIMATE3D_REQUIREMENTS}" --no-build-isolation)
    if [[ "${ANIMATE3D_REQUIREMENTS}" == "${ANIMATE3D_DIR}/requirements.txt" ]]; then
        requirements_args+=(--extra-index-url https://download.blender.org/pypi/)
    fi
    conda_run "${ANIMATE3D_ENV}" python -m pip install "${requirements_args[@]}"

    # Keep this pin in the installer as well as in the requirements file so a
    # checkout that still carries the unpinned upstream file is corrected
    # automatically.  Animate3D's threestudio code imports the legacy
    # read_obj/fast_winding_number_for_meshes API from libigl 2.4.1.
    conda_run "${ANIMATE3D_ENV}" python -m pip install --force-reinstall --no-deps \
        "libigl==${ARTRISE_ANIMATE3D_LIBIGL_VERSION:-2.4.1}"

    # diffusers 0.28.0 still imports cached_download, which was removed from
    # huggingface_hub 0.26.0.  controlnet_aux also exposes optional MediaPipe
    # detectors; install it explicitly so the import does not degrade to a
    # warning-only, partially functional package.
    conda_run "${ANIMATE3D_ENV}" python -m pip install \
        "huggingface_hub==${ARTRISE_ANIMATE3D_HF_HUB_VERSION:-0.25.2}" \
        "mediapipe==${ARTRISE_ANIMATE3D_MEDIAPIPE_VERSION:-0.10.14}"
    # bitsandbytes 0.38.1 imports triton.ops, which was removed from recent
    # Triton releases. Keep the legacy Triton API required by Tracking
    # Anything's mmcv import path instead of accepting the latest release.
    conda_run "${ANIMATE3D_ENV}" python -m pip install --force-reinstall --no-deps \
        "triton==${ARTRISE_ANIMATE3D_TRITON_VERSION:-2.0.0}"

    # The current libigl bindings (2.5+) renamed/removed APIs used by
    # threestudio.  Fail during installation with a clear dependency error
    # instead of discovering it only when the first animation is submitted.
    conda_run "${ANIMATE3D_ENV}" python -c \
        'from igl import fast_winding_number_for_meshes, point_mesh_squared_distance, read_obj; print("Compatible libigl bindings detected")'
    conda_run "${ANIMATE3D_ENV}" python -c \
        'from huggingface_hub import cached_download; import mediapipe; print("Compatible Hugging Face and MediaPipe dependencies detected")'
    conda_run "${ANIMATE3D_ENV}" python -c \
        'from triton.ops.matmul_perf_model import early_config_prune, estimate_matmul_time; print("Compatible Triton API detected")'

    # The upstream install guide requires these native extensions in the
    # threestudio-3dgs plugin. Clone only when absent so reruns are cheap.
    local plugin_dir="${custom_dir}/threestudio_3dgs"
    local legacy_plugin_dir="${custom_dir}/threestudio-3dgs"
    mkdir -p "${custom_dir}"

    # ARtRise follows the importable underscore names used by the reference
    # Animate3D checkout.  Migrate an older installer layout once, without
    # downloading the 3DGS plugin a second time.
    if [[ ! -e "${plugin_dir}" && -e "${legacy_plugin_dir}" ]]; then
        mv -- "${legacy_plugin_dir}" "${plugin_dir}"
    fi
    if [[ ! -d "${plugin_dir}" ]]; then
        git clone https://github.com/DSaurus/threestudio-3dgs.git "${plugin_dir}"
    fi
    if [[ ! -d "${plugin_dir}/diff-gaussian-rasterization" ]]; then
        git -C "${plugin_dir}" clone --recursive https://github.com/ashawkey/diff-gaussian-rasterization
    fi
    if [[ ! -d "${plugin_dir}/simple-knn" ]]; then
        git -C "${plugin_dir}" clone https://github.com/DSaurus/simple-knn.git
    fi
    # Both extensions import torch from setup.py while compiling CUDA code;
    # pip's isolated PEP-517 environment does not contain the environment's
    # PyTorch installation, so explicitly disable build isolation.
    conda_run "${ANIMATE3D_ENV}" python -m pip install --no-build-isolation \
        "${plugin_dir}/diff-gaussian-rasterization"
    conda_run "${ANIMATE3D_ENV}" python -m pip install --no-build-isolation \
        "${plugin_dir}/simple-knn"

    download_file \
        "https://huggingface.co/yanqinJiang/animate3d/resolve/${ARTRISE_ANIMATE3D_CHECKPOINT_REVISION:-main}/animate3d_motion_modules.ckpt" \
        "${ANIMATE3D_DIR}/pretrained_models/animate3d_motion_modules.ckpt"

    # The Google Drive archive contains the upstream test set. It is useful
    # for reproducing Animate3D examples but not required for ARtRise meshes,
    # so downloading it is deliberately opt-in.
    if [[ "${ARTRISE_DOWNLOAD_ANIMATE3D_TEST_DATA:-0}" == "1" ]]; then
        local test_tmp
        test_tmp="$(mktemp -d)"
        conda_run "${ANIMATE3D_ENV}" python -m pip install gdown
        conda_run "${ANIMATE3D_ENV}" gdown --fuzzy \
            "https://drive.google.com/file/d/1iFSuCAwWBVzlLCQH32yoikz8M2qBJ8rP/view?usp=sharing" \
            -O "${test_tmp}/animate3d_test_data.zip"
        unzip -q "${test_tmp}/animate3d_test_data.zip" -d "${test_tmp}/extracted"
        local archive_root="${test_tmp}/extracted"
        shopt -s nullglob
        local archive_entries=("${archive_root}"/*)
        if [[ "${#archive_entries[@]}" -eq 1 && -d "${archive_entries[0]}" \
            && "$(basename -- "${archive_entries[0]}")" == "Animate3D" ]]; then
            archive_root="${archive_entries[0]}"
        fi
        cp -a "${archive_root}/." "${ANIMATE3D_DIR}/"
        shopt -u nullglob
        rm -rf -- "${test_tmp}"
    else
        echo "Skipping optional Animate3D test data (set ARTRISE_DOWNLOAD_ANIMATE3D_TEST_DATA=1 to enable)."
    fi
}

build_webserver() {
    echo "==> Building Spring Boot webserver"
    (cd "${SERVER_DIR}/webserver" && bash ./mvnw -DskipTests package)
}

install_submodules
create_pipeline_directories
create_webserver_directories
create_animate3d_directories
install_base
install_realesrgan
install_rembg
install_trellis
install_animate3d
echo "==> Skipping Blender download (install Blender separately and set BLENDER_EXECUTABLE when starting the server)"
build_webserver

echo
echo "ARtRise server installation complete."
echo "Conda environments: ${BASE_ENV}, ${REALESRGAN_ENV}, ${REMBG_ENV}, ${TRELLIS_ENV}, ${ANIMATE3D_ENV}"
echo "Start the pipeline from ${SERVER_DIR} with: python scripts/run.py"
