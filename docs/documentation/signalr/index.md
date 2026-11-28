---
Order: 16
---
# Receive SignalR updates

An order screen can read the current status once, then listen for changes while it is open. A SignalR hub exposes server methods that a client calls by name. The client library also exposes server callbacks and streams as `IObservable<T>`, so the screen can dispose its subscriptions when it closes.

Install `ReactiveUI.AspNetCore.SignalR.Client.Rx` and import its namespace. If your app uses System.Reactive, use the package and namespace ending in `.Rx.Reactive`.

## Read an order and subscribe to updates

Build a `HubConnection` with the hub URL and start it. The example uses a single start attempt. Pass the caller's cancellation token to the operation and the wait.

```csharp
await using var connection = new HubConnectionBuilder()
    .WithUrl(hubUrl)
    .WithAutomaticReconnect()
    .Build();
await connection.StartObservable(1, token).ToTask(token);

var statusMethod = new HubMethod<string>("Status");
string status = await connection.InvokeObservable(statusMethod, token, 42).ToTask(token);
// "42 shipped"
```

Register the callback before asking the server to send it. `On` takes the callback name and its argument types. Use `On("Ping")` for a callback without arguments, `HubMethod<T>` for one argument, or a tuple for two to eight arguments.

```csharp
var changed = new HubMethod<(int Number, string Status)>("OrderChanged");
var next = connection.On(changed).Take(1).ToTask(token);
await connection.SendObservable("Notify", token, 42).ToTask(token);
var update = await next;
// update.Number is 42 and update.Status is "shipped".
```

`SendObservable` completes when the client sends the invocation. To wait for the server to finish, call `InvokeObservable`. Its string overload waits for a method that returns no value. A typed `HubMethod<T>` supplies the expected result type.

## Read a stream

A hub can send a series of results from one invocation. Pass the element type and any server arguments to `StreamObservable`. A subscription can display each update as it arrives. For a finite history, collect the results in order.

```csharp
var history = new HubMethod<string>("History");
var entries = await connection.StreamObservable(history, token, 42)
    .ToList().ToTask(token);
// "42 received", then "42 shipped".
```

Disposing a subscription cancels that stream. A server error reaches the subscriber through `OnError`.

## Manage connection lifetime

| Operation | When to use it |
| --- | --- |
| `EnsureStarted(token)` | The caller may receive a connection that has already started. |
| `StateChanges()` | Read the current state, then follow close and reconnect events. |
| `IsReconnecting()`, `HasReconnected()` | Report a reconnect attempt and the connection ID returned after reconnection. Configure automatic reconnection on the SignalR builder. |
| `WaitForState(state)` | Complete when the state stream reports the requested value. |
| `HasClosed()` | Receive the close error, or `null` after a clean stop. |
| `StopObservable(retryCount, token)` | Stop the connection and wait for completion. The retry count is the total number of attempts. |

`HubBuilder.Create` gives each subscriber its own connection and a disposable collection. Keep the subscription alive for the lifetime of the screen. If a stream supplies connections or a UI event should start one, the `Start` extensions start the connection when the stream emits.

Subscribe to both reconnect events before the interruption. The order example returns HTTP 503 for one poll. The client observes the failure and then receives the replacement connection ID.

```csharp
var interruption = connection.IsReconnecting().Take(1).ToTask(token);
var recovery = connection.HasReconnected().Take(1).ToTask(token);
var cause = await interruption;
var activeConnectionId = await recovery;
```

The complete example checks that the ID matches the active connection and that the client can make another call. [Run the in-memory order example](https://github.com/reactiveui/AspNetCore.SignalR.Client.Rx/tree/main/src/examples).
