# Building & Testing

All ReactiveUI repositories follow the same build conventions. If you can build one, you can build any of them.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) — most repos target .NET 8, 9, and 10
- An IDE that supports .NET (Visual Studio 2022+, Rider, VS Code with C# Dev Kit)
- For platform-specific projects (WPF, WinUI, MAUI), you may need workloads installed via `dotnet workload restore` from the `src/` directory

## Clone and build

```shell
git clone --recursive https://github.com/reactiveui/<repo-name>.git
cd <repo-name>/src
dotnet restore
dotnet build
```

> [!IMPORTANT]
> Use a full recursive clone. Shallow clones can fail because build and versioning depend on git history.

Most repositories keep their solution file under `src/`. Check the repo's README if the layout differs. Some repos use the newer `.slnx` format — it works the same way with `dotnet build/test`.

## Run tests

```shell
cd src
dotnet test
```

Tests use [TUnit](https://github.com/thomhurst/TUnit) with Microsoft Testing Platform (MTP). Some repos are still migrating from xUnit/NUnit.

Key differences from VSTest:
- TUnit/MTP arguments go **after** `--`
- Do **not** use `--no-build` — always build before testing
- Detailed output: `dotnet test -- --output Detailed`

### Approval tests

Several repos use approval tests to track changes to the public API surface. When you change a public API, a `*.received.txt` file is generated and compared against the corresponding `*.approved.txt` file. If they differ, the test fails.

If the change is intentional, replace the approved file with the received file and commit it alongside your code change.

## Code analysis

Builds run several analysers automatically:

- **StyleCop** — formatting and documentation rules
- **Roslynator** — code quality checks
- **.NET Analyzers** — built-in Roslyn analysers at `AnalysisLevel=latest`
- **Nullable reference types** — enabled across all projects; warnings are errors

The `.editorconfig` at the repository root configures all formatting rules. Your IDE should pick these up automatically. If an analyser flags something, fix it — CI treats warnings as errors.

> [!NOTE]
> **Zero pragma policy** — do not use `#pragma warning disable` in production code. StyleCop warnings must be fixed, not suppressed. CA warnings may use `[SuppressMessage]` with justification as a last resort only.

## Multi-targeting

ReactiveUI projects multi-target extensively. A typical project may target:

- `net8.0`, `net9.0`, `net10.0`
- `net462`, `net472`, `net481` (Windows desktop)
- Platform-specific TFMs for iOS, Android, macOS, Mac Catalyst
- MAUI and WinUI where applicable

You do not need all target platforms installed to contribute. Build and test against the TFMs you have available — CI will cover the rest.
