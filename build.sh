#!/usr/bin/env bash
set -eo pipefail
SCRIPT_DIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd)

export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

BUILD_CONFIGURATION="${BUILD_CONFIGURATION:-Release}"
BUILD_PROJECT_FILE="$SCRIPT_DIR/build/_build.csproj"

dotnet build "$BUILD_PROJECT_FILE" --configuration "$BUILD_CONFIGURATION" --nologo -clp:NoSummary --verbosity minimal
dotnet run --project "$BUILD_PROJECT_FILE" --no-build --configuration "$BUILD_CONFIGURATION" -- "$@"
