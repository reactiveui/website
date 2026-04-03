# Commit Message Convention

We use [Conventional Commits](https://www.conventionalcommits.org/). Commit messages are parsed by our release note generator to produce changelogs automatically, so consistent formatting matters.

## Format

```
<type>: <subject>

<body>

<footer>
```

An optional **scope** can be added in parentheses: `fix(bindings): resolve nullable handling`.

## Types

| Type | Description | Release notes category |
|---|---|---|
| `break` | Breaking change to the public API | Breaking Changes |
| `feat` | New feature | Features |
| `fix` | Bug fix | Fixes |
| `refactor` | Code change that neither fixes a bug nor adds a feature | Refactoring |
| `perf` | Performance improvement | Performance |
| `chore` | Build process, tooling, or maintenance | General Changes |
| `test` | Adding or updating tests | Tests |
| `doc` | Documentation only changes | Documentation |
| `style` | Formatting, whitespace — no code behaviour change | Style Changes |
| `dep` | Dependency updates | Dependencies |
| `ci` | CI/CD pipeline changes | General Changes |

> [!NOTE]
> Dependency bot commits (Renovate, Dependabot) are automatically categorised as dependency updates regardless of prefix.

## Subject

- Use imperative, present tense: "change" not "changed" nor "changes"
- Don't capitalise the first letter
- No full stop at the end

## Body

Optional. Use it to explain **why** the change was made and how it differs from previous behaviour. Same tense rules as the subject.

## Footer

Use the footer for:

- **Breaking changes** — start with `BREAKING CHANGE:` followed by a description
- **Issue references** — e.g. `Closes #123`

## Examples

```
feat: add WhenAnyValue overload for six properties

Closes #4200
```

```
break: seal ValidationState as record

BREAKING CHANGE: ValidationState is now a sealed record. Subclassing is no longer supported.
```

```
chore(deps): update dependency DynamicData to 9.4.31
```
