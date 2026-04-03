# CI & Shared Tooling

ReactiveUI maintains consistent build infrastructure across all repositories through shared configurations and reusable GitHub Actions.

## actions-common

The [actions-common](https://github.com/reactiveui/actions-common) repository contains reusable GitHub Actions workflows that all ReactiveUI projects reference:

- **`workflow-common-setup-and-build.yml`** — the main build workflow. Handles restore, build, test, and code coverage collection. Highly parameterised to support different project types.
- **`workflow-common-release.yml`** — release automation and NuGet publishing.
- **`workflow-common-codeql.yml`** — CodeQL security scanning.

Individual repos call these workflows with project-specific parameters (target frameworks, workloads, platform flags). This means CI behaviour is consistent — a fix to the shared workflow improves all repos at once.

## Shared code quality configuration

Every repo uses the same set of analysers and formatting rules:

| Tool | Purpose | Configuration |
|---|---|---|
| `.editorconfig` | Formatting, naming conventions, C# style rules | Repository root |
| StyleCop Analyzers | Documentation and layout enforcement | `src/stylecop.json` |
| Roslynator Analyzers | Additional code quality checks | Via NuGet in `Directory.Build.props` |
| .NET Analyzers | Built-in Roslyn analysis at latest level | `Directory.Build.props` |

These are pulled in via `Directory.Build.props` so individual projects don't need to reference them. The `.editorconfig` is comprehensive — your IDE should auto-format correctly if it supports EditorConfig.

## Versioning

Repositories use [Nerdbank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning) for automatic version calculation from git history:

- **Development builds** — versions include an alpha suffix derived from git height
- **Pull request builds** — versions include a PR identifier
- **Release builds** — clean semver versions from tagged commits

Version configuration lives in `version.json` at the repo root.

## Dependency management

All repos use [Renovate](https://github.com/renovatebot/renovate) for automated dependency updates with a shared configuration. See the dedicated [Renovate & Dependencies](renovate.md) page for full details on the setup, defaults, and package grouping rules.

## Release notes

Release notes are generated automatically from commit messages using [GitReleaseNoteGenerator](https://github.com/glennawatson/GitReleaseNoteGenerator). This is why following the [commit conventions](software-style-guide/commit-message-convention.md) matters — your commit type determines which section of the changelog your change appears in.
