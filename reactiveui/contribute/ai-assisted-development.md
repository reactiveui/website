# AI-Assisted Development

We actively use AI coding tools across ReactiveUI and encourage contributors to do the same. Claude Code and GitHub Copilot are both used by maintainers day-to-day.

## Repo-level AI guidance

Most ReactiveUI repositories include agent instruction files that give AI tools context about the project:

- **`agent.md`** — the single source of truth for AI assistance in each repo. Contains build commands, architecture context, coding standards, and AOT guidance.
- **`CLAUDE.md`** — points Claude Code to `agent.md`.
- **`.github/copilot-instructions.md`** — points GitHub Copilot to `agent.md`.

If you use Claude Code or Copilot, these files are picked up automatically. They help the AI follow our conventions, use the right build commands, and understand the project structure.

## When AI tools help

- **Boilerplate and repetitive code** — reactive property declarations, command wiring, test scaffolding
- **Understanding unfamiliar code** — asking the AI to explain a binding pipeline or scheduler interaction
- **Refactoring** — renaming, modernising C# syntax, applying patterns consistently across files
- **Writing tests** — generating test cases from existing patterns
- **Commit messages and PR descriptions** — following our conventional commit format

## When to be careful

- **Public API design** — AI tools can generate plausible APIs that don't fit the framework's conventions. API changes should always be discussed with maintainers first.
- **AOT/trimming** — the AI may introduce reflection patterns that break AOT compatibility. Review generated code for `DynamicallyAccessedMembers`, `UnconditionalSuppressMessage`, and similar attributes.
- **Platform-specific code** — generated code may not account for multi-targeting constraints.
- **Complex Rx pipelines** — AI-generated observable chains can have subtle subscription lifecycle issues. Test thoroughly.

## Disclosure

If your contribution was substantially AI-generated or AI-assisted, please mention it in the PR description. This is not a gate or a judgement — it helps reviewers understand the context and focus their review appropriately. A one-liner like "AI-assisted with Claude Code" or "Copilot helped scaffold the tests" is plenty.

## Writing good prompts for ReactiveUI work

Point the AI at the `agent.md` file in the repo you are working on. Beyond that:

- Ask it to follow the `.editorconfig` and StyleCop rules
- Specify TUnit (not xUnit/NUnit) for new tests
- Mention AOT compatibility requirements when working on core libraries
- Reference existing patterns — "follow the same style as `ReactiveCommand.CreateFromTask`" works well
