[CmdletBinding()]
Param(
    [Parameter(Position=0,Mandatory=$false,ValueFromRemainingArguments=$true)]
    [string[]]$BuildArguments
)

Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"
trap { Write-Error $_ -ErrorAction Continue; exit 1 }
$PSScriptRoot = Split-Path $MyInvocation.MyCommand.Path -Parent

$env:DOTNET_NOLOGO = 1
$env:DOTNET_CLI_TELEMETRY_OPTOUT = 1
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 1

$BuildConfiguration = if ($env:BUILD_CONFIGURATION) { $env:BUILD_CONFIGURATION } else { "Release" }
$BuildProjectFile = "$PSScriptRoot\build\_build.csproj"

# Default verb when none is supplied, so `./build.ps1` does a strict build.
if (-not $BuildArguments) {
    $BuildArguments = @("build")
}

function ExecSafe([scriptblock] $cmd) {
    & $cmd
    if ($LASTEXITCODE) { exit $LASTEXITCODE }
}

ExecSafe { & dotnet build $BuildProjectFile --configuration $BuildConfiguration -clp:NoSummary --verbosity minimal }
ExecSafe { & dotnet run --project $BuildProjectFile --no-build --configuration $BuildConfiguration -- $BuildArguments }
