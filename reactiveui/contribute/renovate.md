# Renovate & Dependency Management

All ReactiveUI repositories use [Renovate](https://docs.renovatebot.com/) to keep dependencies up to date automatically. The configuration is centralised so every repo behaves the same way.

## How it works

Renovate scans each repo on a schedule, detects outdated NuGet packages, GitHub Actions, and other dependencies, and opens PRs to update them. These PRs use the `chore(deps):` commit prefix so they are categorised as dependency updates in [release notes](software-style-guide/commit-message-convention.md).

## Shared configuration

The shared Renovate preset lives in the [reactiveui/.github](https://github.com/reactiveui/.github) repo at `renovate.json`. Individual repos opt in with a single-line config:

```json
{
    "$schema": "https://docs.renovatebot.com/renovate-schema.json",
    "extends": ["local>reactiveui/.github:renovate"]
}
```

This file goes in `.github/renovate.json` in each repo. That is all that is needed — the shared preset handles everything else.

## Default settings

The shared preset extends Renovate's [recommended config](https://docs.renovatebot.com/presets-config/#configrecommended) and adds the following:

### Update strategy

| Setting | Value | Why |
|---|---|---|
| Separate major releases | Major version bumps get their own PR | Breaking changes need individual review |
| Combine patch + minor | Patch and minor updates are grouped into a single PR | Reduces PR noise |
| Minimum release age | 3 days | Avoids updating to a version that gets yanked immediately |
| Vulnerability alerts | Enabled, labelled `security` | Security fixes are flagged for fast review |

### Rate limits

| Setting | Value |
|---|---|
| PRs per hour | 10 |
| Concurrent open PRs | 10 |

### Labels

All Renovate PRs are labelled **Dependency Management**.

### PR behaviour

- `recreateWhen: never` — if you close a Renovate PR, it will not reopen until a newer version is available. This avoids PRs coming back after deliberate decisions to skip a version.

## Package grouping

Related packages are grouped into single PRs to reduce noise:

| Group | Packages |
|---|---|
| **.NET monorepo** | `Microsoft.NETCore.*`, runtime packages |
| **NuGet monorepo** | NuGet client packages |
| **Xamarin & AndroidX** | `Xamarin.*`, `Xamarin.AndroidX.*`, `Xamarin.Android.Support.*` |
| **Windows SDK & App SDK** | `Microsoft.WindowsAppSDK`, `Microsoft.Windows.SDK.BuildTools` |
| **Rx.NET** | `System.Reactive.*`, `Microsoft.Reactive.*`, `System.Interactive.*`, `System.Linq.Async.*` |
| **.NET test stack** | `xunit.*`, `NUnit*`, `Verify.*`, `Microsoft.NET.Test.Sdk`, `coverlet.*` |
| **Refit** | `Refit.*` |
| **ReactiveUI** | `ReactiveUI.*` |
| **Avalonia** | `Avalonia.*` |
| **Uno Platform** | `Uno.*` |
| **Splat** | `Splat.*` |

## For maintainers

- **Merging Renovate PRs** — if CI is green, these are generally safe to merge. Check the Renovate PR body for release notes and breaking change warnings.
- **Skipping a version** — close the PR. Renovate will not recreate it until the next version is published.
- **Adding a repo** — create `.github/renovate.json` with the `extends` line above. Renovate will pick it up on its next run.
- **Changing defaults** — edit `renovate.json` in the [reactiveui/.github](https://github.com/reactiveui/.github) repo. Changes apply to all repos that extend the shared preset.
