---
Order: 6
---
# Settings

Typed settings via Akavache.Settings:
```csharp
public class AppSettings : SettingsBase
{
    public AppSettings() : base(nameof(AppSettings)) {}
    public bool EnableNotifications { get => GetOrCreate(true); set => SetOrCreate(value); }
    public string Theme { get => GetOrCreate("Light"); set => SetOrCreate(value); }
}

AppSettings? settings = null;

AppBuilder.CreateSplatBuilder()
    .WithAkavache<SystemJsonSerializer>("MyApp",
        b => b.WithSqliteDefaults()
              .WithSettingsStore<AppSettings>(s => settings = s));
              // Or, for an encrypted store:
              // .WithSecureSettingsStore<AppSettings>("password", s => settings = s));

var enabled = settings!.EnableNotifications;
settings.Theme = "Dark";
```
