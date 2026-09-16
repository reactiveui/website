# CLAUDE.md

This file is the single source of truth for AI/agent assistance in this repository.

If another agent entrypoint file exists, it defers to this file.

## Repository Orientation

This repository holds the source of [reactiveui.net](https://www.reactiveui.net/).

- **Content:** `docs/` — every page, as markdown
- **Build program:** `build/Program.cs` — a single .NET 10 program wired up with `System.CommandLine`
- **API reference package list:** `nuget-packages.json`
- **Renderer version:** the `NuStreamDocsVersion` property in `Directory.Packages.props`

The .NET 10 SDK is the only prerequisite. `README.md` covers the layout and the workflows.

## Build Commands

Run from the repository root.

```bash
./build.sh build              # build the whole site into site/
./build.sh build --strict false  # downgrade broken-link failures to warnings
./build.sh serve              # watch docs/, rebuild on save, serve on 127.0.0.1:8000
./build.sh serve --port 9000  # serve on another port
./build.sh clean              # delete .api-cache/, build/_intermediate/ and site/
```

`build.ps1` and `build.cmd` take the same verbs. `./build.sh` with no verb runs `build`.

Link checking is **on by default** for both `build` and `serve`. A broken internal link or an unresolved
cross-reference fails the build. `--strict false` turns that off.

`build` is the gate before any push. The first run downloads every package in the API reference and is slow;
later runs read `.api-cache/`.

## Repository Conventions

- The navigation tree follows the folder tree. A new page needs no registration.
- `Order` in a page's front matter places it among its siblings. A lower number comes first.
- Every content folder needs an `index.md`.
- `Announcements/` and `articles/` are blogs. A post needs `Title`, `Published`, `Author`, `Tags`,
  `IsBlog: true` and `NoTitle: true`, and its file name starts with the date.
- `.api-cache/`, `build/_intermediate/` and `site/` are generated and gitignored. Never commit them.
- The API reference is generated from the published NuGet packages. Never hand-write a page under `docs/api/`.
- Adding a package to the reference means adding it to `nuget-packages.json`, not writing pages.

## Writing Docs

These rules cover `README.md`, `CLAUDE.md` and every page under `docs/`.

**Apply them to new work and to work you modify.** Rewriting a page you were not asked to touch is out of
scope. A page you do rewrite follows these rules in full.

### Who you write for

Write for a reader at a grade 8 level who knows basic C#. They know what a class, a property and an event
are. They do not know this library.

### Sentences

- Put the main point first.
- Give each sentence one subject. Use two only when they are tightly coupled.
- Keep sentences short. Split a sentence that needs a dash, a semicolon or a "which" to hold together.
- Use the active voice. Say who does what: "the operator delivers the value", not "the value is delivered".
- Use verbs, not nouns made from verbs. Write "decide", not "make a decision".
- Say what is true. Avoid double negatives.
- Cut words that add nothing. Do not restate a point in the next sentence.

### Words

- Use everyday words. When you need a technical term, define it the first time you use it.
- Define each term once. After that, use it without explaining it again.
- Use the same word for the same thing every time. Do not swap in a synonym for variety.
- Use "you" for the reader.

### Structure

- Use headings so a reader can find a topic.
- Use a list for steps or for separate items. Use a table to compare items across the same columns.
- Show a short code example when it explains faster than words.

### Scope

- Each page says what the thing does, on its own terms.
- A comparison with System.Reactive, R3 or R3Async goes only on a page whose subject is that comparison, or
  in a migration guide. Do not compare with them anywhere else.
- Describe the code as it is. Do not describe what it used to do.

### Page shape

The model is [`ReactiveUI.Binding.SourceGenerators/README.md`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/README.md):
the problem first, then a numbered walkthrough of the first use, then reference sections. A reader who stops
after the walkthrough can already do the thing.

## In Flight: Per-Operator Pages For ReactiveUI.Primitives

The site is to carry one page per operator family for `ReactiveUI.Primitives`, with an example for every
operator. The package README lists operators in tables and samples only the obvious cases, so the exhaustive
per-operator treatment lives here. The two must not duplicate each other.

### Source of truth

The Primitives repository is `reactiveui/Primitives`. **An agent doing this work needs that repository
checked out alongside this one.** The authoritative list of the public surface is the PublicAPI baselines at
`src/<Project>/PublicAPI/<tfm>/PublicAPI.txt`. Read those rather than searching the source; they are complete
and cheap. The repository's own `CLAUDE.md` carries its writing rules, and they match the rules above.

### Page grouping

One page per family: creation factories, transformation, filtering, combination, time, error handling,
aggregation and terminal, utility. Then the same eight again for the async surface. Then separate pages for
subjects and stateful signals, sequencers and scheduling, disposables, and the advanced delivery types.

### Facts that will bite

- `Fold` and `Scan` both build a running accumulation, so **`Fold` = `Scan`**. `Reduce` and `Aggregate` both
  emit a single final value, so **`Reduce` = `Aggregate`**. `Map` = `Select`, `Keep` = `Where`,
  `Spark` = `Materialize`.
- Most operator types are internal: `PrependSignal<T>`, `StartWithEnumerableSignal<T>`, `FoldSignal`,
  `ReduceSignal`, `UniqueSignal`, `ZipSignal`, `CombineLatestSignal`, `CalmSignal`, `ShiftSignal`,
  `ProbeSignal`, `LatchSignal`, `KeepNotNullSignal`, `KeepTypeSignal`, `ReattemptSignal` and
  `AbsoluteExpireSignal`. Never tell a reader to construct one. `LeadSignal<T>` is the public type for one
  leading value.
- `DeliveryGateState`, `SerializedDelivery<T>`, `SerializedBroadcaster<T>`, `CurrentValueDelivery<T>`,
  `WitnessAsyncState`, `DisposableSet` and `DispatchSequencerState` are record structs meant to be held as a
  mutable field and called in place. A copy is a separate gate, queue or set. Every sample using one shows a
  non-readonly field, never a local copy.
- Every `ReactiveUI.Primitives.*` type also exists as `ReactiveUI.Primitives.Reactive.*`, compiled from the
  same source. Cover that in one sentence per page. Do not write the page twice.
- The `ReactiveUI.Disposables` package ships its types under the namespace
  `ReactiveUI.Primitives.Disposables`.
- Almost the whole synchronous operator surface is one type, `ReactiveUI.Primitives.LinqExtensions`, split
  across about 20 files in `src/Primitives.Shared`. There is no `SignalOperatorMixins` type; those file names
  are partial-class shards.
- `src/ReactiveUI.Primitives.Extensions.Core` has no `.csproj`. It is globbed into
  `ReactiveUI.Primitives.Core.csproj`, so its public surface appears in the Core baseline.
- The async surface is three static classes: `SignalAsync` for creation factories, `SignalAsyncExtensions`
  for every operator and terminal, and `SignalAsyncReactiveExtensions` for the `RxVoid` and `AsyncContext`
  surface. Async subject factories live on `Signal` in `ReactiveUI.Primitives.Async.Signals`.
- All eight async TFM baselines are identical, so the async pages need no per-framework notes.
- Eight platform sequencers ship: WPF, WinForms, WinUI, Avalonia, MAUI, Blazor, Android (`HandlerSequencer`)
  and Apple (`NSRunloopSequencer`). The Android and Apple ones appear only in their own TFM baselines.
- `ReactiveUI.Primitives` is published to NuGet and `nuget-packages.json` already fetches it, so the API
  reference covers it. Link into `/api/` rather than restating a signature list.
