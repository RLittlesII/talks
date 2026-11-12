#!/bin/bash
# macOS Bash 3.2; no shell session is needed once Caddy is registered with launchd.
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"
STATE_DIR="${TALKS_LOCAL_HOME:-$HOME/Library/Application Support/Talks}"
URL="http://localhost:3030"
SERVICE="homebrew.mxcl.caddy"
AGENT=""
lock_held=false
staging=""

fail() { echo "Error: $*" >&2; exit 1; }
need() { command -v "$1" >/dev/null 2>&1 || fail "Required command not found: $1"; }

cleanup() {
  [ -z "$staging" ] || rm -rf "$staging"
  if $lock_held; then
    rm -f "$STATE_DIR/.current.$$" "$STATE_DIR/.previous.$$"
    rm -rf "$STATE_DIR/.deploy-lock"
  fi
}
trap cleanup EXIT
trap 'exit 130' INT
trap 'exit 143' TERM

lock() {
  mkdir -p "$STATE_DIR"
  if ! mkdir "$STATE_DIR/.deploy-lock" 2>/dev/null; then
    fail "Another deployment holds $STATE_DIR/.deploy-lock (PID $(cat "$STATE_DIR/.deploy-lock/pid" 2>/dev/null || echo unknown)). If it was interrupted, verify that process has exited before removing the lock directory."
  fi
  lock_held=true
  echo "$$" > "$STATE_DIR/.deploy-lock/pid"
}

switch_release() {
  local release="$1" old=""
  local link
  for link in current previous; do
    if [ -e "$STATE_DIR/$link" ] && [ ! -L "$STATE_DIR/$link" ]; then
      fail "$STATE_DIR/$link must be a symlink."
    fi
  done
  if [ -L "$STATE_DIR/current" ]; then
    old=$(readlink "$STATE_DIR/current")
    ln -s "$old" "$STATE_DIR/.previous.$$"
    /bin/mv -fh "$STATE_DIR/.previous.$$" "$STATE_DIR/previous"
  fi
  ln -s "$release" "$STATE_DIR/.current.$$"
  # macOS -h replaces a symlink to a directory rather than moving inside it.
  /bin/mv -fh "$STATE_DIR/.current.$$" "$STATE_DIR/current"
}

deploy() {
  need pnpm; need node; need jq; need shasum
  lock
  cd "$REPO_ROOT"
  mkdir -p "$STATE_DIR/releases"
  staging=$(mktemp -d "$STATE_DIR/releases/.staging.XXXXXX")
  "$SCRIPT_DIR/find-talks.sh" > "$staging/talks.list"
  [ -s "$staging/talks.list" ] || fail "No Slidev decks found."
  # Reject ambiguous or unsafe package names before they become directories.
  while IFS= read -r talk_dir; do
    jq -er '.name | select(test("^[a-z0-9][a-z0-9._-]*$"))' "$talk_dir/package.json"
  done < "$staging/talks.list" > "$staging/names.list"
  [ -z "$(sort "$staging/names.list" | uniq -d)" ] || fail "Talk package names must be unique."
  "$SCRIPT_DIR/sync-talks.sh" "$STATE_DIR/cache"
  "$SCRIPT_DIR/generate-site.sh" "$STATE_DIR/cache" "$staging/talks"
  while IFS= read -r name; do
    [ -s "$staging/talks/$name/index.html" ] || fail "Missing built deck: $name"
  done < "$staging/names.list"
  [ -s "$staging/talks/index.html" ] || fail "Missing talks index."
  rm "$staging/talks.list" "$staging/names.list"
  local release="$STATE_DIR/releases/$(date -u +%Y%m%dT%H%M%SZ)-$$"
  mv "$staging" "$release"
  staging=""
  switch_release "$release"
  echo "Deployed: $release"
  echo "Open $URL/talks/ (run pnpm local:setup once to enable the server)."
}

rollback() {
  lock
  [ -s "$STATE_DIR/previous/talks/index.html" ] || fail "No previous deployment available."
  local previous
  previous=$(readlink "$STATE_DIR/previous")
  switch_release "$previous"
  echo "Restored: $previous"
}

check_service_owner() {
  need jq
  local info config
  # Homebrew versions use different plist filenames; ask for the registered one.
  if brew list --versions caddy >/dev/null 2>&1; then
    info=$(brew services info caddy --json)
    if [ "$(printf '%s' "$info" | jq -r '.[0].registered')" = true ]; then
      AGENT=$(printf '%s' "$info" | jq -r '.[0].file')
    elif [ "$(printf '%s' "$info" | jq -r '.[0].loaded')" = true ]; then
      fail "Caddy is loaded outside this setup. Leaving it unchanged."
    fi
  fi
  if [ -n "$AGENT" ]; then
    config=$(/usr/libexec/PlistBuddy -c 'Print :ProgramArguments:3' "$AGENT" 2>/dev/null || true)
    [ "$config" = "$STATE_DIR/Caddyfile" ] || fail "An existing Caddy service uses a different configuration. Leaving it unchanged: $AGENT"
    SERVICE=$(/usr/libexec/PlistBuddy -c 'Print :Label' "$AGENT")
  fi
}

check_port() {
  local listeners
  listeners=$(lsof -nP -iTCP:3030 -sTCP:LISTEN 2>/dev/null || true)
  if [ -n "$listeners" ]; then
    echo "$listeners" >&2
    fail "Port 3030 is already occupied. No process was stopped."
  fi
}

xml_escape() {
  printf '%s' "$1" | sed 's/\&/\&amp;/g; s/</\&lt;/g; s/>/\&gt;/g; s/"/\&quot;/g; s/'"'"'/\&apos;/g'
}

configure() {
  local caddy
  caddy="$(brew --prefix caddy)/bin/caddy"
  mkdir -p "$STATE_DIR/logs"
  # jq quotes paths safely for Caddyfile, including spaces and backslashes.
  cat > "$STATE_DIR/Caddyfile" <<EOF
{
    admin off
    auto_https off
}
http://localhost:3030 {
    bind 127.0.0.1 ::1
    root * $(printf '%s' "$STATE_DIR/current" | jq -Rs .)
    redir / /talks/ 302
    header Cache-Control "no-cache"
    file_server {
        hide .*
    }
}
EOF
  "$caddy" fmt --overwrite "$STATE_DIR/Caddyfile"
  "$caddy" validate --config "$STATE_DIR/Caddyfile" --adapter caddyfile
  cat > "$STATE_DIR/service.plist" <<EOF
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0"><dict>
  <key>Label</key><string>$SERVICE</string>
  <key>ProgramArguments</key><array>
    <string>$(xml_escape "$caddy")</string><string>run</string><string>--config</string>
    <string>$(xml_escape "$STATE_DIR/Caddyfile")</string><string>--adapter</string><string>caddyfile</string>
  </array>
  <key>RunAtLoad</key><true/>
  <key>KeepAlive</key><true/>
  <key>ThrottleInterval</key><integer>10</integer>
  <key>StandardOutPath</key><string>$(xml_escape "$STATE_DIR/logs/caddy.log")</string>
  <key>StandardErrorPath</key><string>$(xml_escape "$STATE_DIR/logs/caddy-error.log")</string>
</dict></plist>
EOF
  plutil -lint "$STATE_DIR/service.plist"
}

ready() {
  local pid
  pid=$(launchctl print "gui/$(id -u)/$SERVICE" 2>/dev/null | awk '$1 == "pid" {print $3}' || true)
  [ -n "$pid" ] || return 1
  lsof -nP -t -iTCP:3030 -sTCP:LISTEN 2>/dev/null | grep -qx "$pid" || return 1
  curl --noproxy '*' -fsS --max-time 2 "$URL/talks/" 2>/dev/null | cmp -s - "$STATE_DIR/current/talks/index.html"
}

start() {
  need brew
  [ -s "$STATE_DIR/service.plist" ] || fail "Run pnpm local:setup first."
  [ -s "$STATE_DIR/current/talks/index.html" ] || fail "Run pnpm local:deploy first."
  check_service_owner
  if launchctl print "gui/$(id -u)/$SERVICE" >/dev/null 2>&1; then
    [ -f "$AGENT" ] || fail "Caddy is loaded outside this setup. Leaving it unchanged."
  else
    check_port
    brew services start caddy --file="$STATE_DIR/service.plist"
  fi
  local attempt
  for attempt in 1 2 3 4 5 6 7 8 9 10; do
    if ready; then
      echo "Talks are available at $URL/talks/"
      return
    fi
    sleep 1
  done
  lsof -nP -iTCP:3030 -sTCP:LISTEN >&2 || true
  fail "Caddy did not become ready. See $STATE_DIR/logs/caddy-error.log"
}

setup() {
  need brew; need jq
  check_service_owner
  if ! launchctl print "gui/$(id -u)/$SERVICE" >/dev/null 2>&1; then
    check_port
  else
    [ -f "$AGENT" ] || fail "Caddy is loaded outside this setup. Leaving it unchanged."
  fi
  if ! brew list --versions caddy >/dev/null 2>&1; then
    HOMEBREW_NO_AUTO_UPDATE=1 HOMEBREW_NO_INSTALL_CLEANUP=1 brew install caddy
  fi
  deploy
  configure
  if launchctl print "gui/$(id -u)/$SERVICE" >/dev/null 2>&1; then
    brew services restart caddy --file="$STATE_DIR/service.plist"
  fi
  start
}

[ "$(uname -s)" = Darwin ] || fail "Local service commands currently support macOS only."
case "$STATE_DIR" in /*) ;; *) fail "TALKS_LOCAL_HOME must be an absolute path." ;; esac
case "${1:-}" in
  setup) setup ;;
  deploy) deploy ;;
  rollback) rollback ;;
  start) start ;;
  stop)
    need brew
    check_service_owner
    [ -f "$AGENT" ] || fail "No managed talks service is registered."
    brew services stop caddy
    ;;
  status)
    need brew
    check_service_owner
    brew services info caddy
    echo "Current release: $(readlink "$STATE_DIR/current" 2>/dev/null || echo none)"
    echo "Previous release: $(readlink "$STATE_DIR/previous" 2>/dev/null || echo none)"
    echo "Logs: $STATE_DIR/logs"
    ready || fail "The talks service is not ready. Run pnpm local:start or inspect the logs."
    echo "Ready: $URL/talks/"
    ;;
  *) fail "Usage: $0 {setup|deploy|rollback|start|stop|status}" ;;
esac
