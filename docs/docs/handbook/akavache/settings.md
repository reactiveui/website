# Settings

Typed settings via Akavache.Settings:
```csharp
public class AppSettings : SettingsBase
{
    public AppSettings() : base(nameof(AppSettings)) {}
    public bool EnableNotifications { get => GetOrCreate(true); set => SetOrCreate(value); }
    public string Theme { get => GetOrCreate("Light"); set => SetOrCreate(value); }
}

AppBuilder.CreateSplatBuilder()
    .WithAkavache<SystemJsonSerializer>("MyApp",
        b => b.WithSqliteDefaults()
              .WithSecureSettingsStore<AppSettings>(out var settings));

var enabled = settings.EnableNotifications;
settings.Theme = "Dark";
```
