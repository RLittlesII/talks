#!/usr/bin/env bash
set -euo pipefail

find . -name "package.json" \
  -not -path "*/node_modules/*" \
  -not -path "*/.git/*" \
  -exec grep -l '"@slidev/cli"' {} \; \
  | sed 's|/package.json$||' \
  | sort
