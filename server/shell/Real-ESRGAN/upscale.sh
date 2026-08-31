#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
SERVER_DIR="$(cd -- "${SCRIPT_DIR}/../.." && pwd)"
INPUT_DIR="${SERVER_DIR}/pipeline/stages/image_preparation/flipped_image"
OUTPUT_DIR="${SERVER_DIR}/pipeline/stages/image_preparation/upscaled_image"

exec "${PYTHON:-python3}" \
    "${SERVER_DIR}/scripts/workers/Real-ESRGAN/upscale.py" \
    --input "${INPUT_DIR}" \
    --output "${OUTPUT_DIR}" \
    "$@"
