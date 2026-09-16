#!/usr/bin/env bash
# Environment setup for this repository.
#
# Installs the .NET SDKs the site build needs, and clones the sibling repositories
# the docs work reads from. Safe to run more than once: every step checks first.
#
# A blocked download does not fail the script. It prints what is missing and carries
# on, so a session still starts in an environment that cannot reach the install hosts.

set -uo pipefail

DOTNET_ROOT="${DOTNET_ROOT:-$HOME/.dotnet}"
INSTALL_SCRIPT="/tmp/dotnet-install.sh"
REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
SIBLING_ROOT="$(dirname "$REPO_ROOT")"

log() { printf '==> %s\n' "$*"; }
warn() { printf '!!! %s\n' "$*" >&2; }

# ---------------------------------------------------------------------------
# .NET SDKs
#
# The site build needs .NET 10. The ReactiveUI repositories target up to net11,
# so 11 goes on too. 11 is not GA, so it installs from the preview quality band.
# ---------------------------------------------------------------------------

have_sdk() {
    # $1 is a major version, such as 10 or 11.
    "$DOTNET_ROOT/dotnet" --list-sdks 2>/dev/null | grep -q "^$1\." && return 0
    command -v dotnet >/dev/null 2>&1 && dotnet --list-sdks 2>/dev/null | grep -q "^$1\."
}

fetch_install_script() {
    [ -s "$INSTALL_SCRIPT" ] && return 0
    log "Fetching dotnet-install.sh"
    if curl -fsSL --retry 3 --retry-delay 2 https://dot.net/v1/dotnet-install.sh -o "$INSTALL_SCRIPT"; then
        chmod +x "$INSTALL_SCRIPT"
        return 0
    fi
    warn "Could not download dotnet-install.sh."
    warn "The environment needs outbound access to dot.net and builds.dotnet.microsoft.com."
    return 1
}

install_sdk() {
    local channel="$1" quality="$2" major="${1%%.*}"

    if have_sdk "$major"; then
        log ".NET $channel SDK already present"
        return 0
    fi

    fetch_install_script || return 1

    log "Installing .NET $channel SDK ($quality)"
    if "$INSTALL_SCRIPT" --channel "$channel" --quality "$quality" --install-dir "$DOTNET_ROOT"; then
        return 0
    fi

    warn "Install of .NET $channel failed."
    return 1
}

install_sdk "10.0" "GA"
install_sdk "11.0" "preview"

# Put dotnet on PATH for this process and for later shells.
export DOTNET_ROOT
export PATH="$DOTNET_ROOT:$DOTNET_ROOT/tools:$PATH"
export DOTNET_NOLOGO=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

PROFILE_SNIPPET="export DOTNET_ROOT=\"$DOTNET_ROOT\"
export PATH=\"\$DOTNET_ROOT:\$DOTNET_ROOT/tools:\$PATH\"
export DOTNET_NOLOGO=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1"

if [ -w /etc/profile.d ] 2>/dev/null; then
    printf '%s\n' "$PROFILE_SNIPPET" > /etc/profile.d/dotnet.sh
    log "Wrote /etc/profile.d/dotnet.sh"
elif ! grep -q 'DOTNET_ROOT' "$HOME/.bashrc" 2>/dev/null; then
    printf '\n%s\n' "$PROFILE_SNIPPET" >> "$HOME/.bashrc"
    log "Appended the dotnet PATH to ~/.bashrc"
fi

# ---------------------------------------------------------------------------
# NuGet
#
# The site build restores NuStreamDocs, and then downloads every package in the
# API reference. Both need api.nuget.org.
# ---------------------------------------------------------------------------

if curl -fsS --max-time 20 https://api.nuget.org/v3/index.json -o /dev/null; then
    log "api.nuget.org is reachable"
else
    warn "api.nuget.org is not reachable. Restore and the API reference build will fail."
fi

# ---------------------------------------------------------------------------
# Sibling repositories
#
# The site documents the whole ReactiveUI ecosystem, so every active repository is
# cloned beside this one. A page is then written from the real source rather than
# from memory: the Primitives pages read that repository's PublicAPI baselines, and
# any other page can check the type it describes.
#
# Clones carry full history. Nerdbank.GitVersioning reads it, so a shallow clone
# cannot build. Set RXUI_CLONE_DEPTH=1 for a read-only environment that only needs
# to read source, and accept that nothing in it builds.
# ---------------------------------------------------------------------------

RXUI_REPOS=(
    "actions-common"
    "Akavache"
    "Extensions"
    "Fusillade"
    "Maui.Plugins.Popup"
    "Primitives"
    "punchclock"
    "reactiveui"
    "Reactive.Wasm"
    "ReactiveUI.Avalonia"
    "ReactiveUI.Binding.SourceGenerators"
    "ReactiveUI.SourceGenerators"
    "ReactiveUI.Uno"
    "ReactiveUI.Validation"
    "refit"
    "Sextant"
    "splat"
    "Splat.DI.SourceGenerator"
)

clone_sibling() {
    local name="$1" dest="$SIBLING_ROOT/$1"
    local url="https://github.com/reactiveui/$1.git"
    local depth_args=()

    if [ -d "$dest/.git" ]; then
        log "$name already cloned"
        return 0
    fi

    if [ -n "${RXUI_CLONE_DEPTH:-}" ]; then
        depth_args=(--depth "$RXUI_CLONE_DEPTH")
    fi

    log "Cloning $name"
    if ! git clone --quiet "${depth_args[@]}" "$url" "$dest"; then
        warn "Could not clone $name from $url."
        return 1
    fi
}

cloned=0
failed=0
for repo in "${RXUI_REPOS[@]}"; do
    if clone_sibling "$repo"; then
        cloned=$((cloned + 1))
    else
        failed=$((failed + 1))
    fi
done

log "$cloned of ${#RXUI_REPOS[@]} repositories ready in $SIBLING_ROOT"
if [ "$failed" -gt 0 ]; then
    warn "$failed could not be cloned. The environment needs access to github.com."
fi

# ---------------------------------------------------------------------------
# Report
# ---------------------------------------------------------------------------

log "Installed SDKs:"
if command -v dotnet >/dev/null 2>&1; then
    dotnet --list-sdks || true
else
    warn "dotnet is not on PATH. Nothing that builds or runs C# will work."
fi

log "Setup finished"
exit 0
