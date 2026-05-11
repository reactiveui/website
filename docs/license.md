---
title: Licenses & Credits
---

# Licenses & Credits

## ReactiveUI

ReactiveUI itself is licensed under the [MIT License](https://github.com/reactiveui/ReactiveUI/blob/main/LICENSE),
copyright the ReactiveUI Association and Contributors.

## Site generation

Every page on this site — the Markdown rendering, the navigation, the search
index, the syntax highlighting, the theme shell, the sitemap and feeds — is
produced end-to-end by **[NuStreamDocs](https://github.com/glennawatson/NuStreamDocs)**
(MIT), a zero-dependency .NET static-site generator. No Python, no Node, no
external Markdown processor runs at build time.

API reference pages under [`/api`](/api/) are produced by NuStreamDocs in
combination with **[SourceDocParser](https://github.com/glennawatson/SourceDocParserLib)**
(MIT), a sibling library that walks the published NuGet `.dll` + `.xml`
triples and emits the portable doc model NuStreamDocs renders.

## Inspirations & upstream credits

NuStreamDocs is config-compatible with — and visually / behaviourally
inspired by — a number of foundational projects in the MkDocs, Pygments,
and docfx ecosystems. None of those projects ship code into this site, but
their configuration shapes, theme designs, lexer grammars, and feature sets
shaped what NuStreamDocs implements.

The full third-party notices, with per-project license texts and an
itemised list of what each upstream contributed (theme assets, lexer
grammars, icon catalogues, configuration shape, …), live in the
**[NuStreamDocs LICENSE file on GitHub](https://github.com/glennawatson/NuStreamDocs/blob/main/LICENSE)**.

The headline projects credited there:

- **[MkDocs](https://www.mkdocs.org/)** — the original `mkdocs.yml`
  configuration shape and navigation model.
- **[mkdocs-material](https://squidfunk.github.io/mkdocs-material/)** by
  Martin Donath — the theme whose visual language, navigation, palette
  toggles, and admonition / tab / annotation patterns NuStreamDocs's
  Material theme reproduces.
- **[Zensical](https://zensical.org/)** — the modern re-imagining of
  mkdocs-material whose theming refinements and pipeline ideas informed
  parts of NuStreamDocs.
- **[Python-Markdown](https://python-markdown.github.io/)** and
  **[PyMdown Extensions](https://facelessuser.github.io/pymdown-extensions/)**
  — the surface and behaviour of every supported `markdown_extensions:`
  entry tracks the upstream Python implementations.
- **[Pygments](https://pygments.org/)** — the lexer grammars (Bash, C#, F#,
  Python, PowerShell, JSON, YAML, Diff, HTML, XML, Razor, TypeScript,
  JavaScript) are pragmatic ports of the corresponding Pygments lexers,
  and the emitted CSS class taxonomy matches Pygments' short-form names so
  existing Pygments themes light up against the output.
- **[Mermaid](https://mermaid.js.org/)** — diagram rendering shipped as a
  client-side asset and wired in through the superfences custom-fence shape.
- **[GLightbox](https://github.com/biati-digital/glightbox)** — the
  click-to-zoom image viewer surfaced through the `glightbox` extension.
- **[dotnet/docfx](https://github.com/dotnet/docfx)** — the metadata
  extraction pipeline in SourceDocParser is inspired by, and lifts
  patterns from, docfx.

## Icons & emoji

- **[Material Design Icons](https://pictogrammers.com/library/mdi/)** —
  Apache-2.0; used throughout the navigation and admonition headings.
- **[FontAwesome Free](https://fontawesome.com/)** brand icons for the
  social links in the footer — [FontAwesome Free License](https://fontawesome.com/license/free).
- **[Twemoji](https://github.com/jdecked/twemoji)** SVG emoji set rendered
  through the emoji extension — graphics CC-BY 4.0, code MIT.

## Fonts

- **[Source Sans 3](https://fonts.google.com/specimen/Source+Sans+3)** —
  body font, [SIL Open Font License 1.1](https://fonts.google.com/specimen/Source+Sans+3/license).
- **[JetBrains Mono](https://www.jetbrains.com/lp/mono/)** — monospace /
  code font, SIL Open Font License 1.1.
