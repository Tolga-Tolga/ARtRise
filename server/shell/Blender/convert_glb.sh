#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
SERVER_DIR="$(cd -- "${SCRIPT_DIR}/../.." && pwd)"
BLENDER_EXECUTABLE="${BLENDER_EXECUTABLE:-${SERVER_DIR}/components/Blender/blender}"
CONVERTER_SCRIPT="${SERVER_DIR}/scripts/workers/Blender/glb_to_obj.py"

if [[ ! -x "${BLENDER_EXECUTABLE}" ]]; then
    echo "Blender executable not found or not executable: ${BLENDER_EXECUTABLE}" >&2
    exit 1
fi

exec "${BLENDER_EXECUTABLE}" --background --python "${CONVERTER_SCRIPT}" "$@"
