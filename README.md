[![Build website](https://github.com/reactiveui/website/workflows/Build%20website/badge.svg)](https://github.com/reactiveui/website/actions/workflows/main.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
<br>
<a href="https://reactiveui.net/slack">
<img src="https://img.shields.io/badge/chat-slack-blue.svg">
</a>
<a href="https://github.com/reactiveui/website/labels/good%20first%20issue">
<img src="https://img.shields.io/badge/first--timers--only-friendly-blue.svg">
</a>

<img src="docs/images/logo.png" width="200">

# ReactiveUI Website

This repository holds the source of [reactiveui.net](https://www.reactiveui.net/). You write a markdown
file, run one command, and read the finished page in your browser before you send it to anyone.

> [!NOTE]
> This page covers building and changing the site. It does not teach ReactiveUI. The
> [handbook](https://www.reactiveui.net/documentation/handbook/) teaches the framework. The
> [contributor guide](https://www.reactiveui.net/contribute/) covers how to work on the code.

## Table of Contents

- [The problem it solves](#the-problem-it-solves)
- [Your first change](#your-first-change)
- [Commands](#commands)
- [Where content lives](#where-content-lives)
- [Adding a page](#adding-a-page)
- [Adding an announcement or an article](#adding-an-announcement-or-an-article)
- [The API reference](#the-api-reference)
- [Generated folders](#generated-folders)
- [Bumping NuStreamDocs](#bumping-nustreamdocs)
- [Building against an unpublished NuStreamDocs](#building-against-an-unpublished-nustreamdocs)
- [How the site ships](#how-the-site-ships)
- [Contribute](#contribute)

## The problem it solves

A docs site is more than its markdown. This one also carries an API reference for every ReactiveUI
package, a search index, a navigation tree, two blogs and a set of redirects. Building those by hand,
or checking them in, would go stale the day a package shipped.

So the site is generated. [NuStreamDocs](https://github.com/glennawatson/NuSourceDocs) reads the markdown in
`docs/` and the package list in `nuget-packages.json`. It downloads each package, reads the types out of the
assemblies with Roslyn, and writes the API pages. It then renders every page into `site/`.

The whole build is a single .NET program, `build/Program.cs`. The .NET 10 SDK is the only thing you need
installed.

## Your first change

**1. Clone the repository.**

```bash
git clone https://github.com/reactiveui/website.git
cd website
```

**2. Start the preview server.** The first run downloads every package in the API reference, so it takes a
while. Later runs read the cache.

```bash
./build.sh serve
```

**3. Open the site.** It is at `http://127.0.0.1:8000`.

**4. Edit a markdown file under `docs/`.** Save it. The server rebuilds the page and the browser reloads.

**5. Build once before you open a pull request.**

```bash
./build.sh build
```

A broken internal link fails the build. So does a cross-reference that resolves to nothing.

> [!TIP]
> `build.ps1` and `build.cmd` take the same verbs and options. Run `./build.sh` with no verb and you get
> `build`.

## Commands

| Command | What it does |
|---------|--------------|
| `./build.sh build` | Builds the whole site into `site/`. |
| `./build.sh build --strict false` | Builds, and reports a broken link as a warning instead of failing. |
| `./build.sh serve` | Watches `docs/`, rebuilds on save, and serves the site. |
| `./build.sh serve --port 9000` | Serves on a port you choose. |
| `./build.sh clean` | Deletes every generated folder, so the next build starts cold. |

Link checking is on by default for both `build` and `serve`. Pass `--strict false` to turn it off while you
work on a page that links somewhere you have not written yet.

## Where content lives

```
website/
├── build/
│   ├── Program.cs               The build program, and the plugins it turns on
│   ├── WebsiteLogging.cs        Log messages
│   ├── TerseConsoleFormatter.cs Console output format
│   └── _build.csproj            The NuStreamDocs package references
├── docs/                        Every page you write
│   ├── index.md                 The landing page
│   ├── documentation/           Handbook, getting started, and guidelines
│   ├── articles/                Articles, as a blog
│   ├── Announcements/           Announcements, as a blog
│   ├── contribute/              The contributor guide
│   ├── stylesheets/extra.css    Colours and the hero panel
│   ├── images/, vs/             Pictures and screenshots
│   ├── _redirects               Old URLs, pointed at their new page
│   └── _headers                 Response headers, such as the ones for security
├── nuget-packages.json          Which packages the API reference covers
└── Directory.Packages.props     One line that pins the NuStreamDocs version
```

## Adding a page

Put a markdown file in the folder it belongs to. The navigation tree follows the folder tree, so you do not
register the page anywhere.

Add an `Order` to the front matter to place the page among its siblings. A lower number comes first.

```markdown
---
Order: 4
---
# Commands
```

A folder needs an `index.md`. That file is the page a reader lands on when they pick the folder in the
navigation.

## Adding an announcement or an article

`Announcements/` and `articles/` are blogs. A post there needs more front matter, and its file name starts
with the date.

```markdown
---
NoTitle: true
IsBlog: true
Title: You, I, and ReactiveUI
Tags: Announcement
Author: Kent Boogaart
Published: 2018-04-22
---
```

Each blog also publishes a feed. You get that for free.

## The API reference

`nuget-packages.json` decides which packages the API reference covers.

| Key | What it does |
|-----|--------------|
| `nugetPackageOwners` | Fetches every package these NuGet owners publish. |
| `additionalPackages` | Adds a package from another owner, such as `System.Reactive`. |
| `excludePackages`, `excludePackagePrefixes` | Leaves a package out, such as a sample or a source generator. |
| `tfmPreference` | The order to pick a target framework in, when a package ships several. |
| `tfmOverrides` | Forces one package onto one target framework. |
| `referencePackages` | The reference assemblies each target framework is read against. |

A package you add appears in the reference on the next build. You write nothing by hand.

The API pages stay out of the search index. There are about thirteen thousand of them, and indexing them
would push the deploy past Cloudflare's file limit. Search covers the pages you write.

## Generated folders

Three folders are generated, and all three are gitignored. `clean` deletes them.

| Folder | What is in it |
|--------|---------------|
| `.api-cache/` | The downloaded packages and their extracted assemblies. |
| `build/_intermediate/` | The API markdown the generator writes before rendering. |
| `site/` | The rendered site. |

Delete `.api-cache/` and the next build downloads every package again. Keep it, and the build reuses what it
already has.

## Bumping NuStreamDocs

Every `NuStreamDocs.*` package reads one property, so one line moves them all.

```xml
<NuStreamDocsVersion>1.6.1</NuStreamDocsVersion>
```

## Building against an unpublished NuStreamDocs

`build/_build.csproj` carries a commented-out `<ProjectReference>` block. It points at a sibling
[`NuSourceDocs`](https://github.com/glennawatson/NuSourceDocs) checkout. Uncomment that block, comment out the
`<PackageReference>` block above it, and the site builds against that source. Use this when you change the
renderer and the site at the same time.

## How the site ships

Two workflows run.

| Workflow | When it runs | What it does |
|----------|--------------|--------------|
| `main.yml` | Every push to `main`, and every pull request | Builds the site. It holds no credentials, so a pull request from a fork cannot reach any. |
| `deploy.yml` | Every push to `main`, nightly at 03:00 UTC, and on request | Builds the site and uploads `site/` to Cloudflare Pages. |

The nightly run matters. It rebuilds the API reference against whatever the packages published that day, so
the reference keeps up without anyone committing to this repository.

Cloudflare Pages reads `_redirects` and `_headers` from the site root. Both files are copied there from
`docs/`.

## Contribute

The ReactiveUI website uses an OSI-approved open source license. You can use and share it freely, including
for commercial work. We value everyone who takes part. We would love to have you, even if you have never
contributed to open source before.

Ways to help:

- Fix a page that is wrong, unclear or out of date.
- Write a page that is missing.
- [Answer questions on GitHub Discussions](https://github.com/reactiveui/ReactiveUI/discussions)
- [Share what you know and teach other developers](https://ericsink.com/entries/dont_use_rxui.html)

To make a change: fork the repository, create a branch, run `./build.sh serve` while you write, and open a
pull request against `main`.

## Code of Conduct

We are dedicated to providing a welcoming and inclusive community. Please read and follow our
[Code of Conduct](https://github.com/reactiveui/ReactiveUI/blob/main/CODE_OF_CONDUCT.md).

## License

The ReactiveUI website is licensed under the [MIT License](LICENSE).
