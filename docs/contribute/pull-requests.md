# Pull Requests

## Before you start

- **Trivial fixes** (typos, small bug fixes, one-liner corrections) — go ahead and open a PR directly. These are the most likely to be accepted quickly.
- **Anything non-trivial** (new features, API changes, refactors, large additions) — **open an issue or [GitHub Discussion](https://github.com/reactiveui/ReactiveUI/discussions) first** and get agreement from a maintainer before writing code. This is important. We have had to decline well-intentioned PRs that conflicted with ongoing work or didn't align with the project's direction, and that is a bad experience for everyone. A quick conversation upfront saves you from investing time on something that may not be merged.
- Search [existing PRs](https://github.com/reactiveui/reactiveui/pulls) and issues to avoid duplicate effort.

## Forking and cloning

Most contributors will work from a fork. There are two ways to get started:

### Option A: GitHub CLI (recommended)

The `gh` CLI handles fork creation, cloning, and remote setup in one step:

```shell
gh repo fork reactiveui/<repo-name> --clone
cd <repo-name>
```

This creates your fork on GitHub, clones it locally, and sets up `origin` (your fork) and `upstream` (reactiveui) remotes automatically.

### Option B: Web UI + command line

1. Click **Fork** on the repo's GitHub page
2. Clone your fork:
   ```shell
   git clone --recursive https://github.com/<your-username>/<repo-name>.git
   cd <repo-name>
   git remote add upstream https://github.com/reactiveui/<repo-name>.git
   ```

### Option C: GitHub.dev or Codespaces

For documentation or small code changes, press `.` on any repo page to open it in [github.dev](https://docs.github.com/en/codespaces/the-githubdev-web-based-editor), or use [GitHub Codespaces](https://docs.github.com/en/codespaces) for a full cloud dev environment with build support.

> [!IMPORTANT]
> Use a full recursive clone. Shallow clones can fail because build and versioning depend on git history. If you already have a shallow clone, run `git fetch --unshallow`.

## Keeping your fork up to date

```shell
git fetch upstream
git rebase upstream/main
```

Or use the **Sync fork** button on your fork's GitHub page — see [GitHub's syncing docs](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/syncing-a-fork).

## Making changes

1. Create a branch from `main`:
   ```shell
   git checkout -b my-fix-branch main
   ```

2. Make your changes:
   - Follow the [code style guide](software-style-guide/code-style.md)
   - Include tests for new behaviour or bug fixes
   - Ensure the build and all tests pass locally (`dotnet build && dotnet test` from `src/`)

3. Commit using [conventional commits](software-style-guide/commit-message-convention.md):
   ```
   fix: correct nullable handling in WhenAnyValue
   ```

4. Push to your fork and open a PR:
   ```shell
   git push origin my-fix-branch
   ```
   Then open a PR against `reactiveui/<repo-name>:main` — GitHub will prompt you when you visit your fork.

## What happens next

- CI runs automatically — build, tests, and code analysis must all pass.
- A maintainer will review your PR. We may suggest changes — push additional commits to your branch.
- Once approved, a maintainer will squash-merge your PR.

## API surface changes

If your change modifies the public API, approval test files will need updating. See [building & testing](building-and-testing.md#approval-tests) for details. Include the updated `*.approved.txt` files in your PR.
