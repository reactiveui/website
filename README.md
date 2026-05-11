# ReactiveUI Website

![Build website](https://github.com/reactiveui/website/workflows/Build%20website/badge.svg)

Source for the [ReactiveUI](https://www.reactiveui.net/) website. Pages are
rendered by [NuStreamDocs](https://github.com/glennawatson/NuSourceDocs) — a
.NET-native static-site generator that reads markdown + a NuGet manifest and
emits a Material 3-themed site, including the API reference (extracted from the
published packages via Roslyn + ICSharpCode.Decompiler).

The build is a single .NET 10 program (`build/Program.cs`) wired up with
`System.CommandLine`. The .NET 10 SDK is the only prerequisite.

## Quick start

```bash
./build.sh build              # full build → site/
./build.sh build --strict     # CI-style: fail on broken links / unresolved refs
./build.sh serve              # watch + serve at http://127.0.0.1:8000
./build.sh serve --port 9000  # serve on a different port
./build.sh clean              # wipe .api-cache/, build/_intermediate/, site/
```

`build.ps1` (PowerShell) and `build.cmd` (Windows shim) are equivalent. With no
verb, `./build.sh` defaults to `build`.

## Layout

```
website/
├── build/
│   ├── Program.cs              System.CommandLine entry point + DocBuilder wiring
│   ├── WebsiteLogging.cs       source-generated ILogger messages
│   ├── TerseConsoleFormatter.cs minimal log formatter
│   └── _build.csproj           PackageReference set against NuStreamDocs
├── docs/                       all source content
│   ├── index.md                landing page
│   ├── documentation/          handbook + getting-started + guidelines
│   ├── articles/               curated articles
│   ├── Announcements/          chronological announcements
│   ├── contribute/             contributor guide
│   ├── stylesheets/            brand palette + hero CSS
│   └── images/, vs/            assets + Visual Studio screenshots
├── nuget-packages.json         API-reference package list (consumed by NuStreamDocs)
├── Directory.Packages.props    central NuStreamDocs version pin (one var)
├── .api-cache/                 GENERATED — fetched NuGet assemblies + lib/refs trees
├── build/_intermediate/        GENERATED — extracted API markdown / yml / metadata
└── site/                       GENERATED — rendered output (deployed to Netlify)
```

The three `GENERATED` paths are gitignored. `clean` wipes them.

## Bumping NuStreamDocs

One line in `Directory.Packages.props`:

```xml
<NuStreamDocsVersion>1.3.0</NuStreamDocsVersion>
```

Every `NuStreamDocs.*` `PackageVersion` in the file references that property, so
the version moves in lockstep across every plugin.

## Local-iteration against an unpublished NuStreamDocs

`build/_build.csproj` contains a commented-out `<ProjectReference>` block that
points at a sibling `glennawatson/NuSourceDocs` checkout. Uncomment that block
(and comment out the `<PackageReference>` block above it) to build against live
source instead of the published NuGet packages — useful when iterating on the
renderer at the same time as the site.

## Contributing

1. Fork and clone the repo.
2. Create a branch.
3. `./build.sh serve` — open `http://127.0.0.1:8000` and iterate on the docs.
4. PRs against `main` build under CI (`actions/setup-dotnet@v5`) and deploy to
   Netlify on merge.

For the renderer + API extraction internals see
[`NuSourceDocs`](https://github.com/glennawatson/NuSourceDocs).
