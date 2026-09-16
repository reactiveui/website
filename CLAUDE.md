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

Write prose a grade 8 reader can follow, for a reader with junior to mid-level C# skills. They know the
language: types such as `int`, `bool`, `string`, `DateTimeOffset` and `TimeSpan`; classes, interfaces, generics,
properties and events; lambdas and delegates; exceptions; `IDisposable` and `using`; `Task`, `async` and
`await`; and `CancellationToken`. They know what a thread and a `lock` are, but not much more about
concurrency. They do not know this library, and they may not know reactive programming.

- **Use C# terms as they are.** Write "give it an `int`", not "give it a number". Write "method" or "lambda",
  not "function". Never paraphrase a language basic into everyday words; it reads as talking down and is less
  precise.
- **Explain the library and its ideas, not the language.** Define reactive and library concepts the first time
  they appear: stream, subscribe, observer, complete, fail, operator, signal, sequencer, hot and cold, and any
  operator name. Do not define `Task`, lambda, `IEqualityComparer<T>` or other things a C# developer already
  knows.
- **Explain threading past the basics.** Thread and `lock` need no definition. Deadlocks, race conditions,
  thread-pool starvation, `SynchronizationContext` and semaphores do, briefly, where a page relies on them.
- **Recommend good practice.** Say what to do, not only what is possible: mark a lambda `static` when it
  captures nothing, dispose every subscription, keep blocking calls off the UI thread. Link to the best
  practice page for the reasoning rather than repeating it.

### Sentences

- Put the main point first.
- Give each sentence one subject. Use two only when they are tightly coupled.
- Keep sentences short. Split a sentence that needs a dash, a semicolon or a "which" to hold together.
- Use the active voice. Say who does what: "the operator delivers the value", not "the value is delivered".
- Use verbs, not nouns made from verbs. Write "decide", not "make a decision".
- Say what is true. Avoid double negatives.
- Cut words that add nothing. Do not restate a point in the next sentence.

### Words

- Use everyday words for everything that is not a C# term. When you need a library or reactive term, define it
  the first time you use it.
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

- **The audience is C# developers, not Rx users.** Examples use the LINQ name when `System.Linq.Enumerable` has
  the operator (`Select`, `Where`, `SelectMany`, `Aggregate`, `Concat`, `Zip`, `Take`, `Distinct`, `OfType`,
  `Cast`, `Prepend`, `Append`, `ToList`, `Range`, `Repeat`, `Empty`). For every other operator they use the
  Primitives name (`Calm`, `Unique`, `Fold`, `Tap`, `Blend`, `Race`, `SyncLatest`, `Latch`, `Probe`, `Shift`,
  `Expire`, `Recover`, `Emit`, `Fail`, `Lazy`, `After`, `Every`), never the Rx name. Where an overload exists only
  under the Rx name, such as `Delay(DateTimeOffset)` or `CombineLatest` on a collection, use that name for that
  overload. A non-reflection Rx operator with no Primitives equivalent is likely a gap; report it to the
  Primitives repository.
- **Types keep BCL names where the API uses a BCL type**: write `IObservable<T>` and `IObserver<T>`, since the
  operators return them. Name the variables with Primitives words: a stream is a signal (or a descriptive name),
  an `IObserver<T>` is a `witness`. Never `observable` or `observer`.
- **The library's own types and containers keep the Primitives names**, to set the library apart from Rx:
  `Signal` and its factories (`Signal.Emit`, never `Observable.Return`), `ISignal<T>`, `ToSignal`, `Witness`,
  `Sequencer`, `Spark`, `Moment`, `RxVoid`.
- Both names are supported and neither is wrong; the second names match other reactive libraries such as RxJS.
  Pages never tell readers which to use. The rule above is only this site's convention.
- Second names never appear in page prose. Each page lists them once, in the Second name column of its
  at-a-glance table at the bottom.
- Pages describe ReactiveUI as built on ReactiveUI.Primitives. Change operator names only under the rule above.
- `Fold` and `Scan` both build a running accumulation, so **`Fold` = `Scan`**. `Reduce` and `Aggregate` both
  emit a single final value, so **`Reduce` = `Aggregate`**. `Map` = `Select`, `Keep` = `Where`,
  `Spark` = `Materialize`.
- The operator types are public, in `ReactiveUI.Primitives.Advanced`: `KeepSignal<T>`, `UniqueSignal<T>`,
  `FoldSignal<TSource, TAccumulate>`, `ZipSignal<TLeft, TRight, TResult>`, `LeadSignal<T>` and the rest. Each
  takes its source through the constructor. Present the operator as the normal path, and the type as what you
  construct when writing an operator of your own. Check a constructor against the baseline before showing it.
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
- `nuget-packages.json` excludes the `ReactiveUI.Primitives*`, `ReactiveUI.Disposables` and `ReactiveUI.Extensions`
  packages from the API reference, to keep the site under the hosting page limit. The pages under
  `docs/documentation/primitives/` are the reference for them, so never link into `/api/` for a Primitives type.
