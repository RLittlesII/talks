#!/usr/bin/env bash
set -euo pipefail

CACHE_DIR="$1"
OUTPUT_DIR="$2"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

mkdir -p "$OUTPUT_DIR"

talk_dirs=()

while IFS= read -r talk_dir; do
  name=$(jq -r .name "${talk_dir}/package.json")

  if [ ! -d "${CACHE_DIR}/${name}" ]; then
    echo "==> Warning: ${name} has no cached build — skipping" >&2
    continue
  fi

  mkdir -p "${OUTPUT_DIR}/${name}"
  cp -r "${CACHE_DIR}/${name}/." "${OUTPUT_DIR}/${name}/"
  talk_dirs+=("$talk_dir")
  echo "==> Assembled ${name}"
done < <("$SCRIPT_DIR/find-talks.sh")

if [ "${#talk_dirs[@]}" -gt 0 ]; then
  node "$SCRIPT_DIR/render-talk-index.mjs" "$OUTPUT_DIR" "${talk_dirs[@]}"
else
  node "$SCRIPT_DIR/render-talk-index.mjs" "$OUTPUT_DIR"
fi
