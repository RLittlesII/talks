#!/usr/bin/env bash
set -euo pipefail

CACHE_DIR="$1"
OUTPUT_DIR="$2"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

mkdir -p "$OUTPUT_DIR"

talk_names=()

while IFS= read -r talk_dir; do
  name=$(jq -r .name "${talk_dir}/package.json")

  if [ ! -d "${CACHE_DIR}/${name}" ]; then
    echo "==> Warning: ${name} has no cached build — skipping" >&2
    continue
  fi

  mkdir -p "${OUTPUT_DIR}/${name}"
  cp -r "${CACHE_DIR}/${name}/." "${OUTPUT_DIR}/${name}/"
  talk_names+=("$name")
  echo "==> Assembled ${name}"
done < <("$SCRIPT_DIR/find-talks.sh")

IFS=$'\n' sorted_names=($(sort <<<"${talk_names[*]}")); unset IFS

list_items=""
for name in "${sorted_names[@]}"; do
  list_items+="    <li><a href=\"/talks/${name}/\">${name}</a></li>"$'\n'
done

cat > "${OUTPUT_DIR}/index.html" <<HTML
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Talks — RLittlesII</title>
  <style>
    body { font-family: system-ui, sans-serif; max-width: 700px; margin: 4rem auto; padding: 0 1.5rem; line-height: 1.6; }
    h1 { font-size: 2rem; margin-bottom: 0.5rem; }
    p  { color: #555; margin-bottom: 2rem; }
    ul { list-style: none; padding: 0; }
    li { margin-bottom: 0.75rem; }
    a  { color: #0969da; text-decoration: none; font-size: 1.1rem; }
    a:hover { text-decoration: underline; }
  </style>
</head>
<body>
  <h1>Talks</h1>
  <p>Slide decks by Rodney Littles II</p>
  <ul>
${list_items}  </ul>
</body>
</html>
HTML

echo "==> Generated index with ${#sorted_names[@]} talk(s): ${sorted_names[*]}"
