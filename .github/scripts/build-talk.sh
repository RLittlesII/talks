#!/usr/bin/env bash
set -euo pipefail

TALK_DIR="$1"
CACHE_DIR="$2"
REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"

name=$(jq -r .name "${TALK_DIR}/package.json")

echo "==> Installing workspace dependencies"
pnpm --dir "$REPO_ROOT" install --frozen-lockfile

echo "==> Building ${name}"
# NOTE: no `--` separator — pnpm swallows it and the flag never reaches slidev.
pnpm --filter "$name" run build --base "/talks/${name}/" --router-mode hash

test -s "${TALK_DIR}/dist/index.html"
mkdir -p "$CACHE_DIR"
staging=$(mktemp -d "${CACHE_DIR}/.build.XXXXXX")
trap 'rm -rf "$staging"' EXIT
cp -R "${TALK_DIR}/dist/." "$staging/"
# A successful replacement must not retain deleted assets from an older build.
rm -rf "${CACHE_DIR:?}/${name:?}"
mv "$staging" "${CACHE_DIR}/${name}"
echo "==> Cached ${name}"
