#!/usr/bin/env bash
set -eo pipefail
SCRIPT_DIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd)

export DOTNET_NOLOGO=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

BUILD_CONFIGURATION="${BUILD_CONFIGURATION:-Release}"
BUILD_PROJECT_FILE="$SCRIPT_DIR/build/_build.csproj"

# Default verb when none is supplied, so `./build.sh` does a strict build.
if [ "$#" -eq 0 ]; then
    set -- build
fi

dotnet build "$BUILD_PROJECT_FILE" --configuration "$BUILD_CONFIGURATION" -clp:NoSummary --verbosity minimal
dotnet run --project "$BUILD_PROJECT_FILE" --no-build --configuration "$BUILD_CONFIGURATION" -- "$@"
