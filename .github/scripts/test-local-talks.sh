#!/bin/bash
# Exercise deployment state transitions with tiny builds, without touching a service.
set -euo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
test_root=$(mktemp -d "${TMPDIR:-/tmp}/talks-test.XXXXXX")
trap 'rm -rf "$test_root"' EXIT
repo="$test_root/repo with spaces"
export TALKS_LOCAL_HOME="$test_root/Library/Application Support/Talks"
mkdir -p "$repo/.github" "$repo/ deck one" "$repo/deck-two" "$test_root/bin"
cp -R "$SCRIPT_DIR" "$repo/.github/scripts"
cp -R "$SCRIPT_DIR/../site" "$repo/.github/site"
printf '{}\n' > "$repo/package.json"
touch "$repo/pnpm-workspace.yaml" "$repo/pnpm-lock.yaml"
printf '{"name":"one","dependencies":{"@slidev/cli":"*"}}\n' > "$repo/ deck one/package.json"
printf '{"name":"two","dependencies":{"@slidev/cli":"*"}}\n' > "$repo/deck-two/package.json"
printf 'first revision\n' > "$repo/ deck one/slides.md"
printf 'second deck\n' > "$repo/deck-two/slides.md"
touch "$repo/ deck one/include-asset"
cat > "$test_root/bin/pnpm" <<'MOCK'
#!/bin/bash
set -euo pipefail
[ "$1" = --filter ] || exit 0
name="$2"
[ "${MOCK_BUILD_FAIL:-}" != "$name" ] || exit 17
while IFS= read -r dir; do
  if [ "$(jq -r .name "$dir/package.json")" = "$name" ]; then
    rm -rf "$dir/dist"
    mkdir -p "$dir/dist"
    printf '<html><body>%s</body></html>\n' "$(cat "$dir/slides.md")" > "$dir/dist/index.html"
    if [ -f "$dir/include-asset" ]; then touch "$dir/dist/old-asset.js"; fi
  fi
done < <(.github/scripts/find-talks.sh)
MOCK
chmod +x "$test_root/bin/pnpm"
export PATH="$test_root/bin:$PATH"
local_script="$repo/.github/scripts/local-talks.sh"

bash "$local_script" deploy > "$test_root/first.log"
first=$(readlink "$TALKS_LOCAL_HOME/current")
test -f "$first/talks/one/old-asset.js"
test -s "$first/talks/two/index.html"
test ! -e "$first/talks/one/.source-hash"
grep -q '/talks/one/#/presenter/1' "$first/talks/index.html"

bash "$local_script" deploy > "$test_root/unchanged.log"
test "$(grep -c 'unchanged — skipping build' "$test_root/unchanged.log")" = 2

printf 'updated revision\n' > "$repo/ deck one/slides.md"
rm "$repo/ deck one/include-asset"
bash "$local_script" deploy > "$test_root/updated.log"
updated=$(readlink "$TALKS_LOCAL_HOME/current")
grep -q 'updated revision' "$updated/talks/one/index.html"
test ! -e "$updated/talks/one/old-asset.js"
test ! -e "$TALKS_LOCAL_HOME/cache/one/old-asset.js"
grep -q 'first revision' "$TALKS_LOCAL_HOME/previous/talks/one/index.html"

printf 'failed revision\n' > "$repo/deck-two/slides.md"
if MOCK_BUILD_FAIL=two bash "$local_script" deploy > "$test_root/failed.log" 2>&1; then
  echo 'Expected a failed deployment' >&2; exit 1
fi
test "$(readlink "$TALKS_LOCAL_HOME/current")" = "$updated"
test ! -d "$TALKS_LOCAL_HOME/.deploy-lock"
test -z "$(find "$TALKS_LOCAL_HOME/releases" -name '.staging.*' -print)"

mkdir "$TALKS_LOCAL_HOME/.deploy-lock"
echo $$ > "$TALKS_LOCAL_HOME/.deploy-lock/pid"
if bash "$local_script" deploy > "$test_root/locked.log" 2>&1; then
  echo 'Expected concurrent deployment rejection' >&2; exit 1
fi
grep -q 'Another deployment holds' "$test_root/locked.log"
test "$(readlink "$TALKS_LOCAL_HOME/current")" = "$updated"
rm -r "$TALKS_LOCAL_HOME/.deploy-lock"

bash "$local_script" rollback
grep -q 'first revision' "$TALKS_LOCAL_HOME/current/talks/one/index.html"
bash "$local_script" rollback
test "$(readlink "$TALKS_LOCAL_HOME/current")" = "$updated"

printf '{"name":"../unsafe","dependencies":{"@slidev/cli":"*"}}\n' > "$repo/deck-two/package.json"
if bash "$local_script" deploy > "$test_root/invalid.log" 2>&1; then
  echo 'Expected invalid package name rejection' >&2; exit 1
fi
test "$(readlink "$TALKS_LOCAL_HOME/current")" = "$updated"

# Service guards: Homebrew's reported filename need not match the launchd label.
export MOCK_BREW_INFO="$test_root/brew-info.json"
agent="$test_root/sh.brew.caddy.plist"
cat > "$agent" <<'PLIST'
<?xml version="1.0" encoding="UTF-8"?>
<plist version="1.0"><dict>
<key>Label</key><string>homebrew.mxcl.caddy</string>
<key>ProgramArguments</key><array>
<string>caddy</string><string>run</string><string>--config</string><string>placeholder</string>
</array></dict></plist>
PLIST
/usr/libexec/PlistBuddy -c "Set :ProgramArguments:3 $TALKS_LOCAL_HOME/Caddyfile" "$agent"
cp "$agent" "$TALKS_LOCAL_HOME/service.plist"
jq -n --arg file "$agent" '[{registered:true, loaded:false, file:$file}]' > "$MOCK_BREW_INFO"
cat > "$test_root/bin/brew" <<'MOCK'
#!/bin/bash
case "$1 $2" in
  'list --versions') exit 0 ;;
  'services info') cat "$MOCK_BREW_INFO" ;;
  *) echo 'Unexpected service mutation' >&2; exit 99 ;;
esac
MOCK
printf '#!/bin/bash\nexit 1\n' > "$test_root/bin/launchctl"
printf '#!/bin/bash\nprintf "node 4242 other-service 127.0.0.1:3030 (LISTEN)\\n"\n' > "$test_root/bin/lsof"
chmod +x "$test_root/bin/brew" "$test_root/bin/launchctl" "$test_root/bin/lsof"
if bash "$local_script" start > "$test_root/port.log" 2>&1; then
  echo 'Expected port conflict rejection' >&2; exit 1
fi
grep -q 'Port 3030 is already occupied' "$test_root/port.log"
grep -q '4242' "$test_root/port.log"
/usr/libexec/PlistBuddy -c 'Set :ProgramArguments:3 /another/site/Caddyfile' "$agent"
if bash "$local_script" stop > "$test_root/owner.log" 2>&1; then
  echo 'Expected unrelated service rejection' >&2; exit 1
fi
grep -q 'existing Caddy service uses a different configuration' "$test_root/owner.log"
echo 'PASS: paths with spaces, index links, incremental builds, clean caches, failed builds, deployment lock, rollback, invalid names, port conflicts, and service ownership.'
