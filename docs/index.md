---
title: Home
hide:
  - navigation
  - toc
---

<div class="rxui-landing" markdown>

<div class="rxui-hero" markdown>

# ReactiveUI

<p class="tagline">An advanced, composable, functional reactive model-view-viewmodel framework for all .NET platforms.</p>

[:material-rocket-launch: Get started](documentation/getting-started/index.md){ .md-button .md-button--primary }
[:fontawesome-brands-github: Star on GitHub](https://github.com/reactiveui/ReactiveUI){ .md-button }

</div>

<div class="rxui-sample" markdown>

=== "C#"

    ```csharp
    this.WhenAnyValue(x => x.SearchQuery)
        .Calm(TimeSpan.FromSeconds(0.8), RxSchedulers.TaskpoolScheduler)
        .Select(query => query?.Trim())
        .Unique()
        .Where(query => !string.IsNullOrWhiteSpace(query))
        .WitnessOn(RxSchedulers.MainThreadScheduler)
        .InvokeCommand(this, x => x.ExecuteSearch);
    ```

=== "F#"

    ```fsharp
    this.WhenAnyValue(fun x -> x.SearchQuery)
        .Calm(TimeSpan.FromSeconds(0.8), RxSchedulers.TaskpoolScheduler)
        .Select(fun (query: string) -> if isNull query then null else query.Trim())
        .Unique()
        .Where(fun query -> not (String.IsNullOrWhiteSpace(query)))
        .WitnessOn(RxSchedulers.MainThreadScheduler)
        .InvokeCommand(this.ExecuteSearch)
    ```

</div>

</div>

<div class="grid cards" markdown>

-   :material-script-text-outline:{ .lg .middle } **Declarative**

    ---

    Describe what you want, not how to do it. Code is communication between people that also happens to run on a computer; optimising for human readability pays off over a project's lifetime.

-   :material-puzzle-outline:{ .lg .middle } **Composable**

    ---

    Build re-usable chunks of functionality that slot into your reactive pipelines. Write and [test code](documentation/handbook/testing.md) once, leverage it many times.

-   :material-monitor-cellphone:{ .lg .middle } **Cross-platform**

    ---

    Share business logic between mobile and desktop. First-class support for [.NET (WPF, WinForms, WinUI), MAUI, Avalonia, and Uno](documentation/getting-started/installation/index.md). Xamarin remains supported for legacy apps.

-   :material-test-tube:{ .lg .middle } **Scalable & Testable**

    ---

    ReactiveUI [copes gracefully as your application grows](articles/2026-05-07-why-reactiveui-earns-its-keep.md). Control time in tests — no more 3-second waits.

-   :material-book-open-page-variant-outline:{ .lg .middle } **Built on ReactiveUI.Primitives**

    ---

    Streams are LINQ for events. [ReactiveUI.Primitives](documentation/primitives/why-primitives.md) makes them fast, small and ready for Native AOT, and [works alongside System.Reactive](documentation/primitives/system-reactive.md) when a library still needs it.

-   :material-lightning-bolt-outline:{ .lg .middle } **Async-aware commands**

    ---

    [`ReactiveCommand`](documentation/handbook/commands/index.md) runs your async work, surfaces `CanExecute`, funnels errors to one handler, and cancels on demand — bind it to a button and forget the plumbing.

-   :material-auto-fix:{ .lg .middle } **Less boilerplate**

    ---

    Decorate fields and methods with `[Reactive]`, `[ObservableAsProperty]`, and `[ReactiveCommand]`; [source generators](documentation/handbook/view-models/boilerplate-code.md) write the property and command code for you.

-   :material-account-group-outline:{ .lg .middle } **Open-source community**

    ---

    [Contribute](contribute/index.md) under an OSI-approved licence. Free for commercial use. Maintained by the ReactiveUI Association and Contributors.

</div>
