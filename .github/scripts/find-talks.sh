#!/usr/bin/env bash
set -euo pipefail

find . -type d \( -name node_modules -o -name .git -o -name .pnpm-store -o -name .cache -o -name dist \) -prune -o \
  -type f -name "package.json" \
  -exec grep -l '"@slidev/cli"' {} \; \
  | sed 's|/package.json$||' \
  | sort
