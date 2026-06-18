#!/usr/bin/env bash
set -euo pipefail

CACHE_DIR="$1"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

mkdir -p "$CACHE_DIR"

source_hash() {
  local dir="$1"
  find "$dir" \
    -not -path "*/node_modules/*" \
    -not -path "*/dist/*" \
    -type f \
    | sort \
    | xargs sha256sum 2>/dev/null \
    | sha256sum \
    | cut -d' ' -f1
}

"$SCRIPT_DIR/find-talks.sh" | while IFS= read -r talk_dir; do
  name=$(jq -r .name "${talk_dir}/package.json")
  current_hash=$(source_hash "$talk_dir")
  cached_hash=$(cat "${CACHE_DIR}/${name}/.source-hash" 2>/dev/null || echo "")

  if [ "$current_hash" = "$cached_hash" ] && [ -d "${CACHE_DIR}/${name}" ]; then
    echo "==> ${name}: unchanged — skipping build"
  else
    echo "==> ${name}: changed or uncached — building"
    "$SCRIPT_DIR/build-talk.sh" "$talk_dir" "$CACHE_DIR"
    echo "$current_hash" > "${CACHE_DIR}/${name}/.source-hash"
  fi
done
