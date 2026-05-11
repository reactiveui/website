---
title: Licenses & Credits
---

# Licenses & Credits

## ReactiveUI

ReactiveUI itself is licensed under the [MIT License](https://github.com/reactiveui/ReactiveUI/blob/main/LICENSE),
copyright the ReactiveUI Association and Contributors.

## Site generation

Every page on this site — the Markdown rendering, the navigation, the syntax
highlighting, the theme shell, the sitemap and feeds — is produced end-to-end
by **[NuStreamDocs](https://github.com/glennawatson/NuStreamDocs)** (MIT), a
zero-dependency .NET static-site generator. No Python, no Node, no external
Markdown processor runs at build time.

API reference pages under [`/api`](/api/) are produced by NuStreamDocs in
combination with **[SourceDocParser](https://github.com/glennawatson/SourceDocParserLib)**
(MIT), a sibling library that walks the published NuGet `.dll` + `.xml`
triples and emits the portable doc model NuStreamDocs renders.

The complete third-party notices for NuStreamDocs itself live in its
**[LICENSE file on GitHub](https://github.com/glennawatson/NuStreamDocs/blob/main/LICENSE)**.

## Client-side libraries

These ship to the browser as part of the rendered site:

- **[Pagefind](https://pagefind.app/)** (MIT) — the static search index and
  search UI.
- **[Mermaid](https://mermaid.js.org/)** (MIT) — client-side diagram
  rendering for `mermaid` fenced code blocks.
- **[GLightbox](https://github.com/biati-digital/glightbox)** (MIT) — the
  click-to-zoom image viewer.

## Icons & emoji

- **[Material Symbols](https://fonts.google.com/icons)** — Apache-2.0; the
  icon font used throughout the theme shell, navigation, and admonition
  headings.
- **[Material Design Icons](https://pictogrammers.com/library/mdi/)** —
  Apache-2.0; the `:material-…:` icon shortcodes.
- **[FontAwesome Free](https://fontawesome.com/)** brand icons for the social
  links in the footer — [FontAwesome Free License](https://fontawesome.com/license/free).
- **[Twemoji](https://github.com/jdecked/twemoji)** SVG emoji set rendered
  through the emoji extension — graphics CC-BY 4.0, code MIT.

## Fonts

- **[Source Sans 3](https://fonts.google.com/specimen/Source+Sans+3)** —
  body font, [SIL Open Font License 1.1](https://fonts.google.com/specimen/Source+Sans+3/license).
- **[JetBrains Mono](https://www.jetbrains.com/lp/mono/)** — monospace /
  code font, SIL Open Font License 1.1.
