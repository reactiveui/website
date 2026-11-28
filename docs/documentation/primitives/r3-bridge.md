---
Order: 27
---
# Use an R3 observable with Primitives

An app can receive stock updates from a library that uses R3 and display them through Primitives. Reference `R3`, `ReactiveUI.Primitives` and `ReactiveUI.Primitives.R3Bridge.Generator` in the project that needs the conversion. The generator adds the extension methods at build time.

## Read an R3 stream

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.R3Bridge;

static IDisposable Listen(R3.Observable<string> barcodes, Action<string> showBarcode)
    => barcodes.AsPrimitivesSignal().Subscribe(showBarcode);
```

The caller owns the returned subscription. Dispose it when the display closes to detach from the scanner. To pass a BCL `IObservable<string>` to an R3 consumer, call `AsR3Observable()` on it.

The stock example starts with a .NET event, passes the generated observable to R3 and converts it back. It checks that the first barcode arrives and the next scan after disposal does not. [Runnable barcode scanner](https://github.com/reactiveui/Primitives/blob/main/src/examples/StockLevels/BarcodeScanner.cs)

## Completion and errors

An R3 success completes the BCL stream. A failed R3 completion or an R3 resumable error calls the BCL observer's `OnError`. That notification ends a BCL subscription. In the other direction, a BCL error becomes a failed R3 completion.

## Async conversions

Add `ReactiveUI.Primitives.Async` for `IObservableAsync<T>`. Which adapter you call depends on the stream supplied by your dependency.

| Input | Convert to Primitives | Convert back |
| --- | --- | --- |
| `R3.Observable<T>` | `AsPrimitivesAsyncObservable()` | `AsR3Observable()` |
| `R3Async.AsyncObservable<T>` | `AsPrimitivesAsyncObservable()` | `AsR3AsyncObservable()` |

The R3Async adapter is generated when the compiler sees the R3Async observable, observer and result types. It forwards cancellation through async subscriptions and sends resumable errors to `OnErrorResumeAsync`. See [async error handling](async/error-handling.md) for the difference between a resumable error and a failed completion.
