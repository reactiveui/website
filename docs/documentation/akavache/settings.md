---
Order: 6
---
# Observe and save settings

Use `Akavache.Settings` when a UI needs both the current preference and later changes. In an app that uses System.Reactive, use `Akavache.Settings.Reactive`.

## Make a setting observable

Derive from `SettingsStorage` to supply a cache. In the example below, `Notifications` starts at `true`. A subscriber receives the saved value when it loads, then each choice the dispatcher makes.

```csharp
internal sealed class DeliveryPreferences(IBlobCache cache)
    : SettingsStorage(nameof(DeliveryPreferences), cache)
{
    internal IObservable<bool> Notifications => GetOrCreateObservable(true);

    internal async Task SetNotificationsAsync(bool enabled, CancellationToken token) =>
        _ = await SetObservable(enabled, nameof(Notifications)).ToTask(token);
}
```

Pass the getter's name to `SetObservable` so the read and write use the same key. Await the write when you need to know the cache accepted it. Disposing the settings also disposes the supplied cache. These helpers use runtime serialization. For a trimmed app, store the preference with generated JSON metadata as shown in [serialization](serialization.md). [Delivery preferences example](https://github.com/reactiveui/Akavache/blob/main/src/examples/OrderCache/DeliveryPreferences.cs)

## Bind a plain property

For a view that binds to `Value`, expose a `SettingsPropertyHelper<T>` from your settings class. Create it with `CreateProperty(defaultValue, nameof(YourProperty))`. The helper raises `PropertyChanged` when the value changes. Save a choice with `await helper.Set(value).ToTask(token)` and dispose the helper when its owner closes.

## Open a named settings store

Use a settings class with a public parameterless constructor and the builder's `WithSettingsStore<T>` to open it at startup. Use `WithSecureSettingsStore<T>` with an encryption key supplied by the app for a secure store. You can also obtain a store from a built instance with `GetSettingsStore<T>()` or `GetSecureSettingsStore<T>(key)`.
