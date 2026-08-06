#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

"$SCRIPT_DIR/find-talks.sh" | jq -R -s '
  split("\n")
  | map(select(length > 0))
  | map({
      dir: .,
      name: (
        . | ltrimstr("./")
          | sub("^\\s+"; "")
          | gsub("/"; "-")
      )
    })
  | {include: .}
'
