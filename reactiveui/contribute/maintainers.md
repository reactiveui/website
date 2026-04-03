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

Releases are driven by git tags and automated through the shared CI workflows in [actions-common](https://github.com/reactiveui/actions-common).

### Workflow

1. Ensure `main` is in a releasable state — CI green, no known regressions.
2. Update `version.json` if needed (Nerdbank.GitVersioning controls the version).
3. Create and push a git tag matching the version (e.g. `v23.2.0`).
4. CI picks up the tag, builds a release configuration, and publishes to NuGet.
5. Release notes are generated automatically from commit messages since the last tag.
6. Create a GitHub Release from the tag with the generated notes.

### Versioning rules

We follow [Semantic Versioning](https://semver.org/):

- **Major** — breaking changes to the public API
- **Minor** — new features, non-breaking additions
- **Patch** — bug fixes, dependency updates, documentation

Use the `break:` commit prefix for breaking changes. The release note generator will highlight these prominently.

## Team management

- Repository access is managed through GitHub teams under the [reactiveui organisation](https://github.com/reactiveui).
- Each repo has a `CODEOWNERS` file that determines required reviewers.
- New maintainers are added after sustained, quality contributions and a consensus among existing maintainers.
