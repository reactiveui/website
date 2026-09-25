---
Order: 13
---
# Testing with TUnit

[Run the complete TUnit project](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/TestingFrameworks/TUnit).

This page shows how to test a Refit client with TUnit and `Refit.Testing`. Each test solves one problem,
named in the comment above it.
The same tests exist for [xUnit](xunit.md), [NUnit](nunit.md) and [MSTest](mstest.md). Only the test attributes and assert calls change.
[Testing a Refit client](index.md) explains how `Refit.Testing` works.

## Run the tests

Run this from the `src` folder of the Refit repository:

```bash
dotnet test examples/Documentation/TestingFrameworks/TUnit/TUnitTests.csproj
```

## The setup every test shares

`Person` is the model and `IPeopleApi` is the Refit interface under test. `PersonJsonContext` holds the
source-generated JSON metadata for `Person`. `TestSettings.Create()` returns new settings for each client,
because `StubHttp` changes the settings you pass it.

[`Person.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/TestingFrameworks/TUnit/Person.cs):

```csharp
namespace Refit.Documentation.TestingFrameworks;

/// <summary>A person, as returned by the little pretend API these tests call.</summary>
/// <param name="Id">The person's id.</param>
/// <param name="Name">The person's name.</param>
public sealed record Person(int Id, string Name);
```

[`IPeopleApi.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/TestingFrameworks/TUnit/IPeopleApi.cs):

```csharp
namespace Refit.Documentation.TestingFrameworks;

/// <summary>
/// The API these tests pretend to call. Refit turns this interface into real HTTP calls; in these tests,
/// a stub handler intercepts every call instead of sending it over the network.
/// </summary>
public interface IPeopleApi
{
    /// <summary>Gets one person by id.</summary>
    /// <param name="id">The person's id.</param>
    /// <returns>The person the server sent back.</returns>
    [Get("/people/{id}")]
    Task<Person> GetPersonAsync(int id);

    /// <summary>Creates a person.</summary>
    /// <param name="person">The person to create.</param>
    /// <returns>The person the server saved.</returns>
    [Post("/people")]
    Task<Person> CreatePersonAsync([Body] Person person);

    /// <summary>Watches a live feed of people, one at a time, as they arrive.</summary>
    /// <param name="cancellationToken">Stops watching when cancelled.</param>
    /// <returns>Each person, as soon as the server sends them.</returns>
    [Get("/people/live")]
    IAsyncEnumerable<Person> WatchPeopleAsync(CancellationToken cancellationToken);

    /// <summary>Uploads a list of people, one per line, as JSON Lines.</summary>
    /// <param name="people">The people to upload.</param>
    /// <returns>A task that completes once the server has answered.</returns>
    [Post("/people/import")]
    Task ImportPeopleAsync([Body(BodySerializationMethod.JsonLines)] IEnumerable<Person> people);
}
```

[`PersonJsonContext.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/TestingFrameworks/TUnit/PersonJsonContext.cs):

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Refit.Documentation.TestingFrameworks;

/// <summary>The JSON metadata for <see cref="Person"/>, generated at compile time instead of by reflection.</summary>
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Person))]
public sealed partial class PersonJsonContext : JsonSerializerContext;
```

[`TestSettings.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/TestingFrameworks/TUnit/TestSettings.cs):

```csharp
using System.Text.Json;

namespace Refit.Documentation.TestingFrameworks;

/// <summary>Builds the <see cref="RefitSettings"/> every test uses, so JSON matches the generated <see cref="Person"/> metadata.</summary>
public static class TestSettings
{
    /// <summary>The JSON options every test shares, built once from the source-generated <see cref="Person"/> metadata.</summary>
    private static readonly JsonSerializerOptions Options = new(PersonJsonContext.Default.Options) { TypeInfoResolver = PersonJsonContext.Default };

    /// <summary>Creates settings that read and write <see cref="Person"/> using the source-generated metadata.</summary>
    /// <returns>Settings ready to pass to a generated client.</returns>
    public static RefitSettings Create() => new(new SystemTextJsonContentSerializer(Options));
}
```

## Read a reply and check what you sent

These two tests are the starting point for testing any Refit client.
The tests are in [`GettingStartedTests.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/TestingFrameworks/TUnit/GettingStartedTests.cs).

### `GetPerson_ReturnsTheStubbedPerson`

`StubHttp` stands in for the network. `Route.Get("/people/{id}")` matches the request, and `Reply.With` sends the person back as JSON.
`CreateGeneratedClient<T>` builds your Refit client on top of the stub, so the test calls the same interface your app calls.

```csharp
// Problem: does my code correctly read back the person my API returns?
[Test]
public async Task GetPerson_ReturnsTheStubbedPerson()
{
    using StubHttp http = new()
    {
        { Route.Get("/people/{id}"), Reply.With(new Person(1, "Ada")) },
    };
    IPeopleApi api = http.CreateGeneratedClient<IPeopleApi>("https://api.example.com", TestSettings.Create());

    Person person = await api.GetPersonAsync(1);

    await Assert.That(person.Name).IsEqualTo("Ada");
}
```

### `CreatePerson_SendsTheRightJsonBody`

`StubHttp` records every request it receives. `LastRequestBodyAsync<T>()` reads the last request body back as a model,
so you can check the data your app sent.

```csharp
// Problem: did my app actually send the data I expected to the server?
[Test]
public async Task CreatePerson_SendsTheRightJsonBody()
{
    using StubHttp http = new()
    {
        { Route.Post("/people"), Reply.With(new Person(2, "Grace"), HttpStatusCode.Created) },
    };
    IPeopleApi api = http.CreateGeneratedClient<IPeopleApi>("https://api.example.com", TestSettings.Create());

    await api.CreatePersonAsync(new Person(2, "Grace"));

    Person? sentPerson = await http.LastRequestBodyAsync<Person>();
    await Assert.That(sentPerson?.Name).IsEqualTo("Grace");
}
```

## Handle errors

A good test also covers the times the server says no.
The tests are in [`ErrorHandlingTests.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/TestingFrameworks/TUnit/ErrorHandlingTests.cs).

### `GetMissingPerson_ReturnsNotFound`

`Reply.Status` answers with a status code and no body. A Refit method that returns `Task<Person>` throws `ApiException`
for an error status, and its `StatusCode` tells you which one. See [errors](../results/errors.md) for the other ways to handle a failed call.

```csharp
// Problem: my API can return 404 for a person who doesn't exist. Does my code notice?
[Test]
public async Task GetMissingPerson_ReturnsNotFound()
{
    using StubHttp http = new()
    {
        { Route.Get("/people/{id}"), Reply.Status(HttpStatusCode.NotFound) },
    };
    IPeopleApi api = http.CreateGeneratedClient<IPeopleApi>("https://api.example.com", TestSettings.Create());

    ApiException? error = await Assert.That(async () => await api.GetPersonAsync(99))
        .ThrowsExactly<ApiException>();

    await Assert.That(error?.StatusCode).IsEqualTo(HttpStatusCode.NotFound);
}
```

### `ForgottenApiCall_FailsVerification`

`VerifyAllCalled()` throws `InvalidOperationException` when a route you set up received no request. The message names the route.
Call it at the end of a test, so a call your code forgot to make fails the test. [Verification](verification.md) covers the other checks.

```csharp
// Problem: how do I make sure a test actually called the API it set up?
[Test]
public async Task ForgottenApiCall_FailsVerification()
{
    using StubHttp http = new()
    {
        { Route.Get("/people/1"), Reply.Status(HttpStatusCode.OK) },
    };

    InvalidOperationException? error = await Assert.That(http.VerifyAllCalled).ThrowsExactly<InvalidOperationException>();

    await Assert.That(error?.Message).Contains("GET /people/1");
}
```

## Simulate a slow or broken network

`NetworkBehavior` makes `StubHttp` act like a real network. By default it adds a 2-second delay, random jitter
(`Variance` 0.4) and a small chance of failure (`FailurePercent` 0.03, a probability from 0 to 1).
Set the values a test needs, and turn the random parts off so the test gives the same result every run.
The tests are in [`NetworkConditionsTests.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/TestingFrameworks/TUnit/NetworkConditionsTests.cs).

### `SlowServer_DelaysTheReplyWithoutWaiting`

Set `TimeProvider` to a `FakeTimeProvider` from the `Microsoft.Extensions.TimeProvider.Testing` package, and the delay follows its clock.
`Advance` moves time forward at once, so the test checks a 2-second delay without waiting 2 seconds.

```csharp
// Problem: what happens when the server is slow? We can test that without really waiting.
[Test]
public async Task SlowServer_DelaysTheReplyWithoutWaiting()
{
    FakeTimeProvider clock = new();
    NetworkBehavior behavior = new()
    {
        Delay = TimeSpan.FromSeconds(2),
        Variance = 0,
        FailurePercent = 0, // turn off the default random jitter and random failures so the test is repeatable
    };
    using StubHttp http = new(behavior)
    {
        { Route.Get("/people/1"), Reply.Json("""{"id":1,"name":"Ada"}""") },
    };
    http.TimeProvider = clock;
    IPeopleApi api = http.CreateGeneratedClient<IPeopleApi>("https://api.example.com", TestSettings.Create());

    Task<Person> pendingPerson = api.GetPersonAsync(1);
    clock.Advance(TimeSpan.FromSeconds(1));
    await Assert.That(pendingPerson.IsCompleted).IsFalse(); // Only one of the two simulated seconds has passed.

    clock.Advance(TimeSpan.FromSeconds(1));
    Person person = await pendingPerson;
    await Assert.That(person.Name).IsEqualTo("Ada");
}
```

### `BrokenNetwork_ThrowsAConnectionError`

`FailurePercent = 1` makes every request fail, and `FailureFactory` chooses the exception. Refit wraps a transport failure in
`ApiRequestException`. Its `Message` is the original message, and `InnerException` holds the original `HttpRequestException`.
`RefitSettings.TransportExceptionFactory` controls that wrapping. See [faults](faults.md) for more failure types.

```csharp
// Problem: what happens when the network just fails, not just returns an error status?
[Test]
public async Task BrokenNetwork_ThrowsAConnectionError()
{
    NetworkBehavior behavior = new()
    {
        Delay = TimeSpan.Zero,
        FailurePercent = 1, // 1.0 = every request fails (the value is a probability from 0 to 1)
        FailureFactory = static () => new HttpRequestException("Connection reset."),
    };
    using StubHttp http = new(behavior)
    {
        { Route.Get("/people/1"), Reply.Json("""{"id":1,"name":"Ada"}""") },
    };
    IPeopleApi api = http.CreateGeneratedClient<IPeopleApi>("https://api.example.com", TestSettings.Create());

    // Refit never lets a transport failure escape as the raw HttpRequestException: it wraps it in an
    // ApiRequestException, copying the original exception's message across.
    ApiRequestException? error = await Assert.That(async () => await api.GetPersonAsync(1))
        .ThrowsExactly<ApiRequestException>();

    await Assert.That(error?.Message).IsEqualTo("Connection reset.");
}
```

## Stream data in and out

Some calls send or receive a stream of items instead of one reply. These tests control the timing of each item.
The tests are in [`StreamingTests.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/TestingFrameworks/TUnit/StreamingTests.cs).

### `WatchPeople_SeesEachOneAsTheyArrive`

`StreamSource` lets the test release one item at a time, and `Reply.Stream` sends the items as JSON Lines.
`Release` sends one person and `Complete` ends the stream. The test shows your code sees Ada before Grace is sent.
[Streaming](streaming.md) covers the other stream formats.

```csharp
// Problem: can my code react to each item the moment it streams in, instead of waiting for everything?
[Test]
public async Task WatchPeople_SeesEachOneAsTheyArrive()
{
    StreamSource source = new(StreamingContentFormat.JsonLines);
    using StubHttp http = new() { { Route.Get("/people/live"), Reply.Stream(source) } };
    IPeopleApi api = http.CreateGeneratedClient<IPeopleApi>("https://api.example.com", TestSettings.Create());
    await using IAsyncEnumerator<Person> people = api.WatchPeopleAsync(CancellationToken.None).GetAsyncEnumerator();

    source.Release(new Person(1, "Ada"));
    await Assert.That(await people.MoveNextAsync()).IsTrue();
    await Assert.That(people.Current.Name).IsEqualTo("Ada");

    ValueTask<bool> nextPerson = people.MoveNextAsync();
    await Assert.That(nextPerson.IsCompleted).IsFalse(); // Grace has not arrived yet.

    source.Release(new Person(2, "Grace"));
    await Assert.That(await nextPerson).IsTrue();
    await Assert.That(people.Current.Name).IsEqualTo("Grace");

    source.Complete();
}
```

### `UploadPeople_ServerDoesNotWaitForTheWholeUpload`

`Reply.From((request, cancellationToken) => ...)` answers with your own code. This overload receives the send's
cancellation token, so the stub reads the body the way a real network handler does.
By default `StubHttp` reads and stores every request body before it matches a route. `RequestCapture.None` turns that off,
so the responder starts reading while your code is still producing the upload.
`RequestCapture.Bounded(maxBytes)` is the middle ground: it records at most `maxBytes` of each body.

```csharp
// Problem: can the server start handling my upload before I've finished sending it?
[Test]
public async Task UploadPeople_ServerDoesNotWaitForTheWholeUpload()
{
    int peopleProducedSoFar = 0;

    IEnumerable<Person> ProducePeople()
    {
        peopleProducedSoFar++;
        yield return new(1, "Ada");
        peopleProducedSoFar++;
        yield return new(2, "Grace");
    }

    int producedWhenServerStartedReading = -1;
    string? uploadedJson = null;
    using StubHttp http = new()
    {
        {
            Route.Post("/people/import"),
            Reply.From(async (request, cancellationToken) =>
            {
                // RequestCapture.None (set below) means StubHttp never buffered the body ahead of
                // time, so this line genuinely runs before ProducePeople() has finished.
                producedWhenServerStartedReading = peopleProducedSoFar;
                uploadedJson = await request.Content!.ReadAsStringAsync(cancellationToken);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            })
        },
    };
    http.RequestCapture = RequestCapture.None;
    IPeopleApi api = http.CreateGeneratedClient<IPeopleApi>("https://api.example.com", TestSettings.Create());

    await api.ImportPeopleAsync(ProducePeople());

    await Assert.That(producedWhenServerStartedReading).IsEqualTo(0);
    await Assert.That(uploadedJson).Contains("Grace");
}
```
