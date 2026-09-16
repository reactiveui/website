#!/usr/bin/env bash
# Environment setup for this repository.
#
# Installs the .NET SDKs the site build needs, and clones the sibling repositories
# the docs work reads from. Safe to run more than once: every step checks first.
#
# A blocked download does not fail the script. It prints what is missing and carries
# on, so a session still starts in an environment that cannot reach the install hosts.

set -uo pipefail

# A cloud setup script runs as root, and the session may then run as another user.
# So install somewhere every user can read rather than into root's home.
if [ -z "${DOTNET_ROOT:-}" ]; then
    if [ "$(id -u)" -eq 0 ]; then
        DOTNET_ROOT="/usr/share/dotnet"
    else
        DOTNET_ROOT="$HOME/.dotnet"
    fi
fi
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

# Install one SDK, asking for the channel alone.
#
# Naming only the channel is what makes this survive a release. The installer reads
# the channel's release metadata and takes whatever that channel currently points
# at, so one call covers preview, RC and GA. Channel 11.0 resolves to an RC build
# today and to the GA build the day it ships, with no change here.
#
# Do not ask for a quality band. Quality lookup resolves through an aka.ms redirect,
# which a restricted network refuses, and the installer then fails outright rather
# than falling back. The band would also have to be corrected by hand every time the
# channel moves: 11.0 is at "go-live", so "preview" no longer matches it.
#
# The feed is pinned so a lookup does not spend its retries on ci.dot.net, which some
# networks refuse. An unpinned retry follows, for a network that refuses the pin.
install_sdk() {
    local channel="$1" major="${1%%.*}"

    if have_sdk "$major"; then
        log ".NET $channel SDK already present"
        return 0
    fi

    fetch_install_script || return 1

    log "Installing .NET $channel SDK"
    if "$INSTALL_SCRIPT" --channel "$channel" --install-dir "$DOTNET_ROOT" \
        --azure-feed "https://builds.dotnet.microsoft.com/dotnet"; then
        log ".NET $channel SDK installed"
        return 0
    fi

    warn "The pinned feed did not answer. Retrying without it."
    if "$INSTALL_SCRIPT" --channel "$channel" --install-dir "$DOTNET_ROOT"; then
        log ".NET $channel SDK installed"
        return 0
    fi

    warn "Could not install .NET $channel."
    return 1
}

install_sdk "10.0"
install_sdk "11.0"

# Put dotnet on PATH for this process.
export DOTNET_ROOT
export PATH="$DOTNET_ROOT:$DOTNET_ROOT/tools:$PATH"
export DOTNET_NOLOGO=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Put dotnet on PATH for every later shell.
#
# A symlink in /usr/local/bin is the only one of these that always works. An agent
# runs its commands in a shell that is neither a login shell nor an interactive one,
# and such a shell reads no profile at all: /etc/profile.d is for login shells, and
# bash skips ~/.bashrc when it is not interactive. /usr/local/bin is on the default
# PATH whatever the shell, and the SDK finds its own root from the resolved path.
link_dotnet() {
    local target="$DOTNET_ROOT/dotnet"

    [ -x "$target" ] || return 1

    for dir in /usr/local/bin /usr/bin; do
        if [ -d "$dir" ] && [ -w "$dir" ]; then
            ln -sf "$target" "$dir/dotnet" && log "Linked $dir/dotnet -> $target" && return 0
        fi
    done

    warn "Could not link dotnet into a directory on the default PATH."
    return 1
}

link_dotnet

# The profile drops are for a human opening a login or interactive shell. They are
# not what puts dotnet on an agent's PATH.
PROFILE_SNIPPET="export DOTNET_ROOT=\"$DOTNET_ROOT\"
export PATH=\"\$DOTNET_ROOT:\$DOTNET_ROOT/tools:\$PATH\"
export DOTNET_NOLOGO=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1"

if [ -d /etc/profile.d ] && [ -w /etc/profile.d ]; then
    printf '%s\n' "$PROFILE_SNIPPET" > /etc/profile.d/dotnet.sh
fi

# ~/.bashrc is written even when it does not exist yet. A Codex setup script runs in
# its own Bash session, so an export does not reach the agent; ~/.bashrc is the
# documented way to carry a variable across, and DOTNET_ROOT rides along with it.
case "$(cat "$HOME/.bashrc" 2>/dev/null)" in
    *DOTNET_ROOT*) ;;
    *) printf '\n%s\n' "$PROFILE_SNIPPET" >> "$HOME/.bashrc" ;;
esac

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
# Clones are shallow and run in parallel, because a cloud setup script has about
# five minutes before it loses its filesystem snapshot. Shallow is enough to read
# source, which is what writing a page needs.
#
# Set RXUI_CLONE_FULL=1 for full history. Nerdbank.GitVersioning reads it, so any
# environment that has to *build* one of these repositories needs it.
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
    local depth_args=(--depth 1)

    if [ -d "$dest/.git" ]; then
        return 0
    fi

    if [ -n "${RXUI_CLONE_FULL:-}" ]; then
        depth_args=()
    fi

    git clone --quiet "${depth_args[@]}" "$url" "$dest" 2>/dev/null
}

log "Cloning ${#RXUI_REPOS[@]} repositories into $SIBLING_ROOT"
for repo in "${RXUI_REPOS[@]}"; do
    clone_sibling "$repo" &
done
wait

cloned=0
missing=""
for repo in "${RXUI_REPOS[@]}"; do
    if [ -d "$SIBLING_ROOT/$repo/.git" ]; then
        cloned=$((cloned + 1))
    else
        missing="$missing $repo"
    fi
done

log "$cloned of ${#RXUI_REPOS[@]} repositories ready"
if [ -n "$missing" ]; then
    warn "Not cloned:$missing"
    warn "The environment needs access to github.com."
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
