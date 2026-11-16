# Website agent instructions

## Build and scope

- This repository builds reactiveui.net with NuStreamDocs. Target .NET 11 only; do not add `global.json`.
- `build/Program.cs` configures rendering. `Directory.Packages.props` pins build packages and `NuStreamDocsVersion`.
- `nuget-packages.json` selects published packages for API documentation. Its framework selections are independent of the build framework. Legacy targets are `net47;net471;net472;net48;net481`.
- Run `./build.sh build` from the root before pushing. `build.ps1` and `build.cmd` provide equivalent entry points. No verb defaults to `build`.
- `serve [--port 9000]` watches and serves locally; `clean` removes generated output and caches. Strict link checking defaults to true. Use `--strict false` only when explicitly requested.
- `.api-cache/`, `build/_intermediate/`, `docs/api/`, and `site/` are generated. Never commit or hand-edit generated API pages; add reference packages through the manifest.
- Build and deploy workflows use .NET 11. Deployment goes to Cloudflare Pages.
- Use StyleSharp, PerformanceSharp, and SecuritySharp with `.editorconfig`. Console output is allowed. Do not add StyleCop, Blazor.Common.Analyzers, or public API tracking.
- Use dedicated editing tools or C# file-based apps for edits and scripts. No Python, Node, shell rewriting, or output redirection. Keep scratch work under a task-specific `~/.cache` directory.
- Work in the main thread unless the user requests delegation. Preserve existing changes. Commit, push, and deploy only when authorised.

## Content

- `docs/` contains Markdown pages. Navigation follows folders; each content folder needs `index.md`. Front-matter `Order` sorts siblings ascending.
- Blog posts belong in `Announcements/` or `articles/`, with date-prefixed filenames and `Title`, `Published`, `Author`, `Tags`, `IsBlog: true`, and `NoTitle: true`.
- Write for junior to mid-level C# developers at a grade 8 reading level. Explain library concepts and advanced threading terms on first use; do not redefine C# basics.
- Lead with the problem and a short first-use walkthrough, then reference details. Use short, active sentences and consistent terms. Avoid repetition, em dashes, and change-history prose.
- Keep changes within the requested pages. Recommend good practice: static lambdas without captures, disposed subscriptions, no blocking on UI threads. Link to explanations rather than repeating them.
- Show one- and two-argument examples for overload families, then state the supported arity range. Never enumerate every overload.
- Keep comparisons with System.Reactive, R3, or R3Async on comparison or migration pages.
- Use Mermaid only when it clarifies behavior, usually at most two diagrams per page and about eight nodes each. Prefer rounded nodes and `flowchart LR`; use sequence/state diagrams for their respective subjects.
- Copy the established Material 3 Mermaid init and class definitions from existing pages. Palette: view `#DCE9FF/#6C8EC4/#0B2447`, vm `#E3F2E8/#7FA88C/#12301C`, model `#F3E5F5/#A98BB0/#2E1437`, warn `#FDE7E4/#C98A82/#410E0B`, neutral `#F1F3F8/#A7AEBB/#1B1F27` (fill/border/text). Introduce each diagram and follow it with one sentence explaining the point.

## Primitives reference pages

- Use the sibling `Primitives` checkout and its instructions. Public API baselines at `src/<Project>/PublicAPI/<tfm>/PublicAPI.txt` are authoritative; verify names and constructors there.
- Provide one page per operator family: creation, transformation, filtering, combination, time, error handling, aggregation/terminal, utility. Repeat for async, then cover subjects/state, sequencers/scheduling, disposables, and advanced delivery types. Include every operator without duplicating the package README.
- Use LINQ names where `System.Linq.Enumerable` has the operator, including `Select`, `Where`, `SelectMany`, `Aggregate`, `Concat`, `Zip`, `Take`, `Distinct`, `OfType`, `Cast`, `Prepend`, `Append`, `ToList`, `Range`, `Repeat`, and `Empty`.
- Otherwise use Primitives names: `Calm`, `Unique`, `Fold`, `Tap`, `Blend`, `Race`, `SyncLatest`, `Latch`, `Spark`, `Shift`, `Expire`, `Recover`, `Emit`, `Fail`, `Lazy`, `After`, `Every`. Use a second name only when the required overload exists solely under it. List second names once in the at-a-glance table, never in prose. Report missing non-reflection operators upstream.
- Keep BCL type names `IObservable<T>` and `IObserver<T>`. Name their variables signals and witnesses. Use library names `Signal`, `ISignal<T>`, `ToSignal`, `Witness`, `Sequencer`, `Spark`, `Moment`, and `RxVoid`; creation uses `Signal.Emit`.
- `Fold = Scan` accumulates continuously; `Reduce = Aggregate` emits one final result. `Map = Select`, `Keep = Where`, `Spark = Materialize`.
- Public operator types live in `ReactiveUI.Primitives.Advanced` and take the source in their constructors. Present operators first, construction of operator types for custom operators second.
- Mutable delivery structs (`DeliveryGateState`, `SerializedDelivery<T>`, `SerializedBroadcaster<T>`, `CurrentValueDelivery<T>`, `WitnessAsyncState`, `DisposableSet`, `DispatchSequencerState`) must be non-readonly fields called in place. Copies have independent state.
- Mention once per page that the same types also ship under `ReactiveUI.Primitives.Reactive.*`. `ReactiveUI.Disposables` uses namespace `ReactiveUI.Primitives.Disposables`.
- Synchronous operators belong to `ReactiveUI.Primitives.LinqExtensions`; partial file names are not types. `ReactiveUI.Primitives.Extensions.Core` is globbed into the Core project and baseline.
- Async factories use `SignalAsync`; operators/terminals use `SignalAsyncExtensions`; RxVoid/AsyncContext uses `SignalAsyncReactiveExtensions`. Async subject factories live on `Signal` in `ReactiveUI.Primitives.Async.Signals`. Async baselines match across frameworks.
- Platform sequencers cover WPF, WinForms, WinUI, Avalonia, MAUI, Blazor, Android (`HandlerSequencer`), and Apple (`NSRunloopSequencer`). Check platform-specific baselines for Android and Apple.
- Primitives, Disposables, and Extensions are excluded from generated API docs to fit hosting limits. Their reference is under `docs/documentation/primitives/`; never link their types into `/api/`.
