# ReactiveUI Website

![Build website](https://github.com/reactiveui/website/workflows/Build%20website/badge.svg)

Source for the [ReactiveUI](https://www.reactiveui.net/) website. The site is rendered by [mkdocs Material](https://squidfunk.github.io/mkdocs-material/); the API reference is generated from the published NuGet packages by [`SourceDocParser`](https://github.com/glennawatson/SourceDocParserLib) (Roslyn + ICSharpCode.Decompiler under the hood).

The build is orchestrated by [Nuke](https://nuke.build/) — it owns NuGet fetching, the API metadata walk, source-link validation, and the mkdocs invocation. Python lives in a project-local `.venv/` that Nuke bootstraps on first run.

## Quick start

```bash
./build.sh BuildWebsite      # full build: NuGet fetch + API walk + mkdocs build → site/
./build.sh Serve             # serve at http://127.0.0.1:8000
./build.sh BuildWebsite --strict   # CI-style: fail on broken links / unresolved refs
./build.sh Clean             # drop generated API tree + site/
```

Direct mkdocs invocations also work once the venv is set up (faster iteration on prose):

```bash
.venv/bin/mkdocs serve
.venv/bin/mkdocs build --strict
```

The Nuke targets are in `build/Build.cs`. Top-level configuration lives in `mkdocs.yml`; per-folder navigation is handled by [`awesome-pages`](https://github.com/lukasgeiter/mkdocs-awesome-pages-plugin) — drop a `.pages` file in any folder to override ordering.

## Layout

```
website/
├── mkdocs.yml             site config (theme, plugins, nav)
├── overrides/             Material theme overrides
├── docs/                  all rendered content
│   ├── index.md           Material landing
│   ├── stylesheets/       brand palette + hero CSS
│   ├── docs/              docs handbook, getting-started, guidelines, …
│   ├── articles/          curated articles (rendered as a blog)
│   ├── Announcements/     chronological announcements (rendered as a blog)
│   ├── contribute/        contributor guide
│   ├── api/               GENERATED — Zensical-emitter output, gitignored
│   └── _redirects         Netlify catchall for legacy docfx .html URLs
├── build/                 Nuke build (Build.cs, _build.csproj)
├── nuget-packages.json    package list driven by SourceDocParser.NuGet
├── requirements.txt       mkdocs + plugins
└── .venv/                 bootstrapped on first ./build.sh run
```

## Contributing

1. Fork and clone the repo.
2. Create a branch.
3. `./build.sh Serve` (or `mkdocs serve` once the venv exists) — open `http://127.0.0.1:8000` and iterate on the docs.
4. PRs against `main` build under CI (`actions/setup-python@v6` + `actions/setup-dotnet@v5`) and deploy to Netlify on merge.

For the API extraction pipeline see [`SourceDocParserLib`](https://github.com/glennawatson/SourceDocParserLib). The `SourceDocParser.Zensical` package emits the mkdocs Material markdown that Build.cs hands to mkdocs.
