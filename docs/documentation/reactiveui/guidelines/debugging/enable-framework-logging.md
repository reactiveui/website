# Enable framework logging

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

ReactiveUI writes its debug messages through Splat's `ILogger`. By default nothing is registered, so those
messages go nowhere. Register a logger at your composition root to see them.

**1. Register a logger.** Splat ships `ConsoleLogger`, which writes to the console at or above the level you
give it. `PreferRegisteringALogger` registers one with `AppLocator`, Splat's service locator, at the level
`LogLevel.Info`:

```csharp
AppLocator.CurrentMutable.RegisterConstant<ILogger>(new ConsoleLogger { Level = LogLevel.Info });
```

**2. Write through it.** Any type that implements `IEnableLogger` gets a `Log()` extension method that resolves
the registered logger and writes through it. The example's `Log` operator uses the same path: it logs each
notification a stream sends, tagged with the `IEnableLogger` instance and a message.

```csharp
SchoolLog log = new();
using IDisposable subscription = Signal.Emit("Robotics Club")
    .Log(log, "Roster loaded")
    .Subscribe(static _ => { });
```

```text
SchoolLog: Roster loaded OnNext: Robotics Club
SchoolLog: Roster loaded OnCompleted
```

`SchoolLog` is a type that does nothing but implement `IEnableLogger`, so its stream can write through the
logger registered in step 1.

Register the logger once, near where your app builds its dependencies, before anything else in the app runs.
A common pattern is to register it only for debug builds, so production builds keep the null logger and pay no
logging cost.

## At a glance

| Do | What it does |
|---|---|
| `AppLocator.CurrentMutable.RegisterConstant<ILogger>(new ConsoleLogger { Level = ... })` | Registers a logger every `IEnableLogger` writes through. |
| `Log(this, "message")` | Logs each notification a stream sends, through the registered logger. |
