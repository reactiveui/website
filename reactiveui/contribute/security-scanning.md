# Security & Code Scanning

ReactiveUI uses GitHub's built-in security features across the organisation to catch vulnerabilities in code and dependencies. This is relatively new work — not all repos have been onboarded yet, but the pattern below is what we are rolling out.

## CodeQL

[CodeQL](https://docs.github.com/en/code-security/code-scanning/introduction-to-code-scanning/about-code-scanning-with-codeql) performs static analysis to find security vulnerabilities and coding errors. We use a shared reusable workflow from [actions-common](https://github.com/reactiveui/actions-common) so all repos run the same analysis configuration.

### What gets scanned

| Language | Runner | Notes |
|---|---|---|
| C# | Windows | Manual build mode — the workflow builds the solution first, then analyses the compiled output |
| GitHub Actions | Linux | Scans workflow YAML files for injection, permission, and secret exposure issues |
| JavaScript/TypeScript | Linux | Opt-in per repo, for repos that include JS/TS content |

All analyses use the **`security-extended`** query suite, which includes more rules than the default set.

### When it runs

- On every **push to `main`**
- On every **pull request targeting `main`**
- **Weekly on a schedule** — catches issues from newly discovered vulnerability patterns even when code has not changed

### Onboarding a repo

Add a workflow file like this (using ReactiveUI as the example):

```yaml
name: "CodeQL Advanced"

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]
  schedule:
    - cron: '0 6 * * 1'

permissions:
  security-events: write
  contents: read
  packages: read
  actions: read

jobs:
  codeql:
    uses: reactiveui/actions-common/.github/workflows/workflow-common-codeql.yml@main
    with:
      srcFolder: src
      solutionFile: reactiveui.slnx
      installWorkloads: true
      analyzeCSharp: true
      analyzeActions: true
```

The shared workflow handles .NET SDK setup, workload restore, building, and running the CodeQL analysis. You just pass in your solution details.

### Viewing results

CodeQL findings appear in the **Security** tab of each repo under **Code scanning alerts**. They are also surfaced as PR annotations when a pull request introduces a new issue.

## Dependabot security alerts

[Dependabot](https://docs.github.com/en/code-security/dependabot/dependabot-alerts/about-dependabot-alerts) is enabled across the organisation to monitor for known vulnerabilities in dependencies:

- **Dependabot alerts** — flags NuGet packages and other dependencies with known CVEs
- **Dependabot security updates** — automatically opens PRs to update vulnerable dependencies to a patched version

This works alongside [Renovate](renovate.md), which handles routine version updates. Dependabot focuses specifically on security — if a vulnerability is disclosed in a package you depend on, Dependabot will alert and can propose a fix independently of Renovate's schedule.

## Secret scanning

GitHub [secret scanning](https://docs.github.com/en/code-security/secret-scanning/introduction/about-secret-scanning) is enabled with **push protection** across the organisation:

- **Secret scanning** — detects accidentally committed secrets (API keys, tokens, connection strings) in the repository history
- **Push protection** — blocks pushes that contain detected secrets *before* they reach the remote. If you hit a push protection block, remove the secret from your commit before pushing again

## Branch protection

Repositories are being onboarded to [GitHub rulesets](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-rulesets/about-rulesets) to enforce:

- Required pull request reviews before merging to `main`
- Required CI status checks to pass
- Protection against force pushes and branch deletion

The specifics vary per repo — check the repo's **Settings > Rules** if you have access, or ask a maintainer.

## For contributors

You do not need to configure any of this yourself. As a contributor, what this means in practice:

- **CodeQL may comment on your PR** if it detects an issue. Treat these like review comments — address them before merge.
- **Dependabot or Renovate PRs labelled `security`** should be prioritised for review and merge.
- **If your push is blocked by secret scanning**, you have likely committed a secret. Remove it from your commit history (not just the latest commit) and rotate the exposed credential.
