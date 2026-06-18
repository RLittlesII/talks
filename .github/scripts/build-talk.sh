#!/usr/bin/env bash
set -euo pipefail

TALK_DIR="$1"
CACHE_DIR="$2"

name=$(jq -r .name "${TALK_DIR}/package.json")

echo "==> Installing dependencies for ${name}"
npm ci --prefix "$TALK_DIR"

echo "==> Building ${name}"
npm run build --prefix "$TALK_DIR" -- --base "/talks/${name}/"

mkdir -p "${CACHE_DIR}/${name}"
cp -r "${TALK_DIR}/dist/." "${CACHE_DIR}/${name}/"
echo "==> Cached ${name}"
