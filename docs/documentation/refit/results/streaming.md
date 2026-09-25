---
Order: 2
---
# Streaming replies

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/results-streaming/results-streaming.csproj).

A large reply can take time to arrive. A live feed may keep sending new items without ending
the response at all. Streaming lets your app start processing those items as the service sends them.

Refit exposes this through `IAsyncEnumerable<T>`, which you read with `await foreach`.
Each item comes from the same HTTP reply; the loop does not send a new request for every item.
Refit sends that request when the loop starts enumerating the sequence.

## Read your first stream

**1. Declare a streaming method.** Use the item type as `T`, rather than a list type.
`Person` is the type from [the first request](../index.md#your-first-request).

```csharp
internal interface IStreamingApi
{
    [Get("/people")]
    IAsyncEnumerable<Person> ReadPeopleAsync(CancellationToken cancellationToken);
}
```

**2. Create the client.** Pass your HTTP client and the generated JSON context from
[the first request](../index.md#your-first-request) to `RestService.ForGenerated<IStreamingApi>`.
The default `SystemTextJsonContentSerializer` supports streamed replies.
The runnable example uses the sample host's client with a base address of `https://people.example`.

```csharp
IStreamingApi api = RestService.ForGenerated<IStreamingApi>(host.Client, SampleJsonContext.Default);
```

To pass settings that you built yourself, hand them to the same call. The sample host exposes its shared settings as `host.Settings`.

```csharp
IStreamingApi withSettings = RestService.ForGenerated<IStreamingApi>(host.Client, host.Settings);
```

**3. Read the items.** The example sets `DeadlineSeconds` to `10` and starts `count` at `0`.
In your app, pass the caller's cancellation token when the caller controls the request lifetime.

```csharp
using CancellationTokenSource cancellation = new(TimeSpan.FromSeconds(DeadlineSeconds));
await foreach (Person person in api.ReadPeopleAsync(cancellation.Token))
{
    Console.WriteLine(person.Name); // Ada, then Grace
    count++;
}
```

Breaking out of `await foreach` closes the response and its body stream.
Refit also disposes the request when enumeration ends or fails.
Call the API method again when you need a new request.

The [streaming example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/results-streaming/results-streaming.csproj)
checks all three formats below through the same generated method.

## Reply formats

Refit chooses the format from the response's `Content-Type` header.
`StreamingContentFormat` names the formats that a streaming serializer can read.

| Format | Content type | Body shape |
| --- | --- | --- |
| `JsonArray` | `application/json`, or another type not listed below | A JSON array such as `[{"id":1,"name":"Ada"}]`. |
| `JsonLines` | `application/jsonl`, `application/x-ndjson`, or `application/x-jsonlines` | Each line holds a separate JSON value. |
| `ServerSentEvents` | `text/event-stream` | Each event's `data:` field holds a JSON value. |

**JSON Lines** separates items with newlines instead of enclosing them in one array.
**Server-sent events**, often shortened to SSE, let a server send named events through a reply that stays open.
Refit's default serializer reads the JSON from each event's data field.
It gives you the deserialized value. It does not expose the event name, ID or retry field.
It does not reconnect when the connection ends.

The server must send each event's data as JSON that the serializer can deserialize as `T`.
For example, an unquoted text value cannot be read as `Person`.

## Read the items with explicit metadata

A streaming method can take a `JsonTypeInfo<T>` parameter for the item type. `T` is the type of one item, not of the
sequence. Refit reads every item with that metadata and does not send the parameter.
It works with all three formats above.

```csharp
[Get("/orders")]
IAsyncEnumerable<Order> StreamOrdersAsync(JsonTypeInfo<Order> orderInfo, CancellationToken cancellationToken);
```

```csharp
await foreach (Order streamed in api.StreamOrdersAsync(OrdersJsonContext.Default.Order, CancellationToken.None))
{
    Console.WriteLine(streamed.Customer); // Ada
}
```

See [pass metadata to a method](../serialization/json.md#pass-metadata-to-a-method) for the rules.

## Cancellation and errors

The method's cancellation token stops the request and body reading.
You can also supply an enumeration token with `WithCancellation`.
Refit combines both tokens when both can cancel.

An unsuccessful HTTP status throws when `RefitSettings.ExceptionFactory` returns an exception.
A bad item throws while the loop reads it. Handle errors around the whole `await foreach` loop.
Use cancellation to end an endless reply when the screen or operation no longer needs it.

To test these cases without a server, release items one at a time from a test response.
See [test streams, uploads and time](../testing/streaming.md).

## Custom serializers

The serializer must implement `IStreamingContentSerializer`.
Its `DeserializeStreamAsync<T>` method receives the body stream, selected format and cancellation token.
It returns the items to enumerate.
The default `SystemTextJsonContentSerializer.DeserializeStreamAsync<T>` also lets you read a stream directly.

The Newtonsoft.Json and XML serializers do not implement this streaming interface.
Using them with a Refit `IAsyncEnumerable<T>` method throws `NotSupportedException` when enumeration starts.
