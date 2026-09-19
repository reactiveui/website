---
Order: 5
---
# Custom return adapters

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/results-adapters/results-adapters.csproj).

Your app may already use a wrapper for service calls, perhaps one that lets the caller decide
when to start the request. A return adapter connects that wrapper to Refit. Your interface
can return the type your app expects while Refit still builds the request and reads the reply.

This page starts with a wrapper that delays the call until you run it. It then explains how
Refit finds adapters and matches their generic types.

## Wrap a deferred call

Implement `IReturnTypeAdapter<TReturn, TResult>`. `TReturn` is the caller's wrapper;
`TResult` is the body Refit reads. Its `Adapt` method receives the invocation that starts the HTTP call.

This wrapper retains the invocation and exposes a method that accepts a cancellation token.


```csharp
public sealed class PersonCall<T>
{
    private readonly Func<CancellationToken, Task<T>> _invoke;

    internal PersonCall(Func<CancellationToken, Task<T>> invoke) => _invoke = invoke;

    public Task<T> InvokeAsync(CancellationToken cancellationToken) => _invoke(cancellationToken);
}
```

The adapter implements the closed relationship `PersonCall<T>` to `T`.
It has an implicit public parameterless constructor, which lets generated code instantiate it.


```csharp
public sealed class PersonCallAdapter<T> : IReturnTypeAdapter<PersonCall<T>, T>
{
    public PersonCall<T> Adapt(Func<CancellationToken, Task<T>> invoke) => new(invoke);
}
```

Declare the custom return shape on the annotated API method.


```csharp
internal interface IAdapterApi
{
    [Get("/content/person")]
    PersonCall<Person> Read();
}
```

Create the generated client and invoke the wrapper once.


```csharp
IAdapterApi api = RestService.ForGenerated<IAdapterApi>(host.Client, host.Settings);
PersonCall<Person> pending = api.Read();
Person result = await pending.InvokeAsync(CancellationToken.None);
Console.WriteLine(result.Name); // Ada
```

The source generator discovers adapter implementations declared in the current compilation and
emits a direct `new ...().Adapt(...)` call. No entry in `RefitSettings.ReturnTypeAdapters` is needed.
The example uses a local handler and generated System.Text.Json metadata, and is suitable for native AOT.
A class in a referenced assembly alone is not discovered by this source-declaration scan.

## Invocation and matching limits

A generated method builds its request before passing the invocation to the adapter.
That invocation captures the request and is single-use. Calling the same wrapper twice can try to
send an already-sent request. Call the API method again to obtain a new wrapper and request.
The sample wrapper does not add replay or retry behavior.
Keep the underlying `HttpClient` alive until the deferred call finishes, and forward cancellation
through the token received by the invocation.

Adapter matching uses the declared return shape. A closed adapter must expose exactly that
`TReturn`. For an open generic adapter, the number of wrapper arguments must match the number of
adapter type parameters. Each adapter parameter must receive a consistent binding.
Reordered wrapper parameters are supported. A concrete wrapper argument must match exactly,
and repeated occurrences of a parameter must agree. An unbound adapter parameter prevents a match.
Avoid several adapters for the same return shape; matching selects the first match.
Adapters need a public parameterless constructor accessible to the generated implementation.
They are not constructed by dependency injection or disposed by Refit.

## Reflection registration

The opt-in reflection request builder resolves adapter types from
`RefitSettings.ReturnTypeAdapters`. Add a closed adapter type or a supported open generic definition
before creating the builder. This runtime list does not control source-generator discovery.
Reflection adapter matching and activation require runtime metadata and dynamic code, so this route
is separate from the generated example and is unsuitable for native AOT.
The reflection builder builds a fresh request for every invocation, allowing a deferred wrapper
that is designed to run more than once. See [request builders](../clients/request-builders.md) for
creating that builder explicitly.

The [reflection example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Clients/Reflection/ReflectionClients.cs)
registers the same deferred wrapper used above. `Content` is the example's namespace;
`host` supplies the local client and generated JSON serializer.

```csharp
RefitSettings settings = new(host.Settings.ContentSerializer);
settings.ReturnTypeAdapters.Add(typeof(Content.PersonCallAdapter<>));
Content.IAdapterApi api = RestService.For<Content.IAdapterApi>(host.Client, settings);
int before = host.Http.Requests.Count;
Content.PersonCall<Person> pending = api.Read();
SampleCheck.Equal(before, host.Http.Requests.Count);
SampleCheck.Equal(expected, await pending.InvokeAsync(CancellationToken.None));
SampleCheck.Equal(before + 1, host.Http.Requests.Count);
```

The contracts are in [IReturnTypeAdapter.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/IReturnTypeAdapter.cs).
Discovery and generic matching are in [Parser.Adapters.cs](https://github.com/reactiveui/refit/blob/main/src/InterfaceStubGenerator.Shared/Parser.Adapters.cs);
runtime matching is in [ReturnTypeAdapterResolver.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Reflection/ReturnTypeAdapterResolver.cs).
