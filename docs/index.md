---
title: Home
hide:
  - navigation
  - toc
---

<div class="rxui-landing" markdown>

<div class="rxui-hero" markdown>

# ReactiveUI

<p class="tagline">A family of open-source .NET libraries for building apps. Use them together, or pick only the one you need.</p>

[:material-rocket-launch: Get started](documentation/getting-started/index.md){ .md-button .md-button--primary }
[:fontawesome-brands-github: Browse on GitHub](https://github.com/reactiveui){ .md-button }

</div>

</div>

## Build your app

These libraries shape how your app fits together. Several of them use streams. A **stream** is a series of
values that arrive over time, such as each new value of a property. You **subscribe** to a stream to receive
its values.

<div class="grid cards rxui-projects" markdown>

-   [![](https://raw.githubusercontent.com/reactiveui/ReactiveUI/main/images/logo.png){ .rxui-logo } **ReactiveUI**](documentation/getting-started/index.md){ .rxui-project-link }

    ---

    A model-view-viewmodel (MVVM) framework for WPF, WinForms, WinUI, MAUI, Avalonia and Uno. MVVM keeps
    screen logic in a view model class that you can test without a UI. Here the Save button turns off while
    the name is empty.

    ```csharp
    IObservable<bool> canSave = this.WhenAnyValue(
        x => x.Name, name => !string.IsNullOrWhiteSpace(name));

    Save = ReactiveCommand.CreateFromTask(SaveAsync, canSave);
    ```

    [:material-arrow-right: Docs](documentation/getting-started/index.md) ·
    [:fontawesome-brands-github: GitHub](https://github.com/reactiveui/ReactiveUI)

-   [![](https://raw.githubusercontent.com/reactiveui/ReactiveUI.Binding.SourceGenerators/main/images/logo.png){ .rxui-logo } **ReactiveUI.Binding**](documentation/binding/index.md){ .rxui-project-link }

    ---

    Keeps a view and a view model in step. A source generator writes the binding code when you build,
    so bindings are safe to trim and to publish with Native AOT. Dispose a binding to stop it.

    ```csharp
    // The text box shows Name, and typing updates Name.
    using var binding = view.Bind(
        viewModel, vm => vm.Name, v => v.NameBox.Text);
    ```

    [:material-arrow-right: Docs](documentation/binding/index.md) ·
    [:fontawesome-brands-github: GitHub](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators)

-   [![](https://raw.githubusercontent.com/reactiveui/ReactiveUI.Validation/main/media/logo.png){ .rxui-logo } **ReactiveUI.Validation**](documentation/handbook/user-input-validation.md){ .rxui-project-link }

    ---

    Adds validation rules to a view model. Each rule names a property, a check and the message to show
    when the check fails.

    ```csharp
    this.ValidationRule(
        vm => vm.Email,
        email => email?.Contains('@') == true,
        "Enter a valid email address.");
    ```

    [:material-arrow-right: Docs](documentation/handbook/user-input-validation.md) ·
    [:fontawesome-brands-github: GitHub](https://github.com/reactiveui/ReactiveUI.Validation)

-   [![](https://raw.githubusercontent.com/reactiveui/styleguide/master/logo_sextant/vertical.png){ .rxui-logo } **Sextant**](documentation/handbook/sextant/index.md){ .rxui-project-link }

    ---

    Navigation that starts from the view model. You open and close pages by naming view models, so you
    can test navigation without a UI.

    ```csharp
    // Show the page for DetailsViewModel, then go back.
    await viewStack.PushPage<DetailsViewModel>();
    await viewStack.PopPage();
    ```

    [:material-arrow-right: Docs](documentation/handbook/sextant/index.md) ·
    [:fontawesome-brands-github: GitHub](https://github.com/reactiveui/Sextant)

</div>

## Foundations

Every library on this page uses ReactiveUI.Primitives for its streams. ReactiveUI, ReactiveUI.Binding, Akavache and
Fusillade also use Splat. You can use either one on its own.

<div class="grid cards rxui-projects" markdown>

-   [![](https://raw.githubusercontent.com/reactiveui/splat/main/images/logo.png){ .rxui-logo } **Splat**](documentation/handbook/dependency-inversion/index.md){ .rxui-project-link }

    ---

    A service locator and logging for every .NET platform. A **service locator** is one shared place that
    hands out services. Register a service when your app starts, then ask for it anywhere.

    ```csharp
    AppLocator.CurrentMutable.RegisterLazySingleton<IWeatherService>(
        () => new WeatherService());

    var weather = AppLocator.Current.GetService<IWeatherService>();
    ```

    [:material-arrow-right: Docs](documentation/handbook/dependency-inversion/index.md) ·
    [:fontawesome-brands-github: GitHub](https://github.com/reactiveui/splat)

-   [![](https://raw.githubusercontent.com/reactiveui/Primitives/main/images/logo.png){ .rxui-logo } **ReactiveUI.Primitives**](documentation/primitives/index.md){ .rxui-project-link }

    ---

    Small, fast streams, ready for Native AOT. Turn events, timers and tasks into streams. Then shape them
    with **operators**, methods that take a stream and return a new one. `Calm` waits for 300 ms of quiet.

    ```csharp
    using var search = Signal.FromEventPattern(
            h => box.TextChanged += h, h => box.TextChanged -= h)
        .Select(_ => box.Text)
        .Calm(TimeSpan.FromMilliseconds(300))
        .Subscribe(RunSearch);
    ```

    [:material-arrow-right: Docs](documentation/primitives/index.md) ·
    [:fontawesome-brands-github: GitHub](https://github.com/reactiveui/Primitives)

</div>

## Data and networking

These libraries call web services and keep data on the device. None of them needs ReactiveUI.

<div class="grid cards rxui-projects" markdown>

-   [![](https://raw.githubusercontent.com/reactiveui/refit/main/images/logo.png){ .rxui-logo } **Refit**](documentation/refit/index.md){ .rxui-project-link }

    ---

    Turns a C# interface into a REST client. An attribute on each method describes the request, and Refit
    writes the code that sends it. Refit takes advantage of System.Text.Json
    [source generation](https://learn.microsoft.com/dotnet/standard/serialization/system-text-json/source-generation)
    to read JSON without reflection. `AppJsonContext` is only this example's name for your context class.

    ```csharp
    public interface IGitHubApi
    {
        [Get("/users/{user}")]
        Task<User> GetUserAsync(string user, CancellationToken token);
    }

    var api = RestService.ForGenerated<IGitHubApi>(
        httpClient, AppJsonContext.Default);
    ```

    [:material-arrow-right: Docs](documentation/refit/index.md) ·
    [:fontawesome-brands-github: GitHub](https://github.com/reactiveui/refit)

-   [![](https://raw.githubusercontent.com/reactiveui/Akavache/main/Images/logo.png){ .rxui-logo } **Akavache**](documentation/handbook/akavache/index.md){ .rxui-project-link }

    ---

    Stores objects on the device: a cache, user settings and encrypted secrets. Ask for a key, and
    Akavache returns the saved copy, or fetches a new one when the copy is missing or has expired.

    ```csharp
    var news = await CacheDatabase.LocalMachine.GetOrFetchObject(
        "news",
        () => api.GetNewsAsync(),
        DateTimeOffset.Now.AddHours(1));
    ```

    [:material-arrow-right: Docs](documentation/handbook/akavache/index.md) ·
    [:fontawesome-brands-github: GitHub](https://github.com/reactiveui/Akavache)

-   [![](https://raw.githubusercontent.com/reactiveui/punchclock/main/images/logo.png){ .rxui-logo } **Punchclock**](https://github.com/reactiveui/punchclock#readme){ .rxui-project-link }

    ---

    A queue that limits how many tasks run at once. When a slot frees up, the task with the highest
    priority runs next.

    ```csharp
    using var queue = new OperationQueue(maximumConcurrent: 2);

    var page = queue.Enqueue(1, () => http.GetStringAsync(pageUrl));
    var urgent = queue.Enqueue(10, () => http.GetStringAsync(urgentUrl));
    await Task.WhenAll(page, urgent);
    ```

    [:fontawesome-brands-github: Read the guide on GitHub](https://github.com/reactiveui/punchclock#readme)

-   [![](https://raw.githubusercontent.com/reactiveui/styleguide/master/logo_fusillade/main.png){ .rxui-logo } **Fusillade**](https://github.com/reactiveui/Fusillade#readme){ .rxui-project-link }

    ---

    An `HttpClient` handler that sends requests in order of importance. Requests the user waits on go
    first, background work waits, and identical requests share one download.

    ```csharp
    using var client = new HttpClient(
        NetCache.UserInitiated, disposeHandler: false);

    string json = await client.GetStringAsync(url);
    ```

    [:fontawesome-brands-github: Read the guide on GitHub](https://github.com/reactiveui/Fusillade#readme)

</div>

## Open source

Every library here is free for commercial use under an OSI-approved licence. The ReactiveUI Association and
its contributors maintain them. [Contribute](contribute/index.md) or [sponsor the work](sponsors.md).
