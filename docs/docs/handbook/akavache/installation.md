# Installation

Akavache v11 uses modular packages and a builder-based initialization for flexible setup and DI.

Packages:
- Akavache (core abstractions/in-memory)
- Akavache.Sqlite3 (persistence)
- Akavache.SystemTextJson (serializer, recommended) or Akavache.NewtonsoftJson
- Akavache.Settings (typed settings)
- Akavache.Drawing (image helpers)

Quick start (Splat builder):
```csharp
AppBuilder.CreateSplatBuilder()
    .WithAkavache<SystemJsonSerializer>(
        "MyApp",
        b => b.WithSqliteDefaults()
              .WithForceDateTimeKind(DateTimeKind.Utc),
        (splat, instance) =>
        {
            splat.RegisterLazySingleton<IBlobCache>(() => instance.LocalMachine, contract: nameof(instance.LocalMachine));
            splat.RegisterLazySingleton<IAkavacheInstance>(() => instance, contract: instance.ApplicationName);
        });
```

MAUI: call the above in MauiProgram.CreateMauiApp.
