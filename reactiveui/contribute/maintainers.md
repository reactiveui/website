# Maintainers Guide

This page covers the day-to-day processes for ReactiveUI maintainers.

## Merging pull requests

1. Ensure CI is green and at least one maintainer has approved.
2. Rename the PR title to follow [commit conventions](software-style-guide/commit-message-convention.md) — this becomes the squash-merge commit message.
3. **Squash and merge** into `main`.
4. Thank the contributor. If it is their first contribution, welcome them.

## Issue triage

- Label new issues appropriately. Use `help wanted` for issues suitable for external contributors.
- Close stale issues that have no activity and no clear reproduction. The repos use auto-close bots for this.
- For proposals or non-trivial feature requests, ask the author to open a GitHub Discussion first.

## Creating a release

Releases are automated through a GitHub Actions pipeline defined in [actions-common](https://github.com/reactiveui/actions-common). The [ReactiveUI](https://github.com/reactiveui/ReactiveUI) repo is the reference implementation — not all repos have been converted yet, but this is the pattern we are rolling out across the organisation.

### Before you release

1. Ensure `main` is in a releasable state — CI green, no known regressions.
2. Bump the version in `version.json` at the repo root. This file controls [Nerdbank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning), which calculates the full semver version from the base version plus git history. For example, changing `"version": "23.1"` to `"version": "23.2"` sets up the next minor release.
3. Commit and push the version bump to `main`.

### Trigger the release

1. Go to the repo's **Actions** tab on GitHub.
2. Select the **Release** workflow from the left sidebar.
3. Click **Run workflow** and confirm.

That is it. The pipeline handles everything else:

| Stage | Runner | What it does |
|---|---|---|
| **Build & Sign** | Windows | Builds the solution in Release, packs NuGet packages, signs them with SSL.com eSigner |
| **Publish** | Linux | Pushes signed packages to NuGet.org via OIDC trusted publishing |
| **GitHub Release** | Linux | Generates release notes from commit history using [GitReleaseNoteGenerator](https://github.com/glennawatson/GitReleaseNoteGenerator), creates a GitHub Release with the notes and package assets attached |

The pipeline runs in a protected `release` environment — only authorised maintainers can trigger it.

### Versioning rules

We follow [Semantic Versioning](https://semver.org/):

- **Major** — breaking changes to the public API
- **Minor** — new features, non-breaking additions
- **Patch** — bug fixes, dependency updates, documentation

Use the `break:` commit prefix for breaking changes. The release note generator will highlight these prominently.

### Pushing directly (authorised users)

Maintainers with write access can push directly to `main` for version bumps and release preparation. For the ReactiveUI repo as an example:

```shell
# Clone or update your local copy
git clone https://github.com/reactiveui/ReactiveUI.git
cd ReactiveUI

# Bump the version
# Edit version.json — change the "version" field
git add version.json
git commit -m "chore: bump version to 23.3"
git push origin main
```

Then go to **Actions > Release > Run workflow** to publish.

## Team management

- Repository access is managed through GitHub teams under the [reactiveui organisation](https://github.com/reactiveui).
- Each repo has a `CODEOWNERS` file that determines required reviewers.
- New maintainers are added after sustained, quality contributions and a consensus among existing maintainers.
