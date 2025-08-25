---
NoTitle: true
IsBlog: true
Title: Akavache Refreshed 
Tags: Article
Author: Chris Pulman
Published: 2025-08-25
---

# Akavache Refreshed: A Deep Dive into V11 vs V10.2.41

Author: Chris Pulman  
Published: August 25, 2025  

---

# Akavache V11 — The Caching Evolution for the Modern .NET Era 🚀

If you’ve ever wrestled with persistent data storage, tried to optimise offline scenarios, or wanted caching that felt **native**, **fast**, and **future-proof**, Akavache has probably been on your radar.  

With **V11**, the venerable caching library doesn’t just keep up with modern .NET — it leapfrogs into a new era of modularity, performance, and developer experience.

---

## Why This Release Matters

This is more than a “bump the version number” kind of release.  
**Akavache V11** represents a **philosophical shift** — from *being a convenient caching library* to *being the foundation for robust, offline‑capable, multi‑platform apps*.

It’s designed for:

- **.NET 8/9+ and .NET Standard 2.0 compatibility**
- .NET MAUI, WPF, WinForms, Avalonia
- Scenarios that demand **speed**, **security**, and **portability**
- Developers who care about **clean architecture** and **future maintainability**

---

## ✨ What's New in Akavache V11 — In Depth

Let’s pull back the curtain and explore each of the marquee improvements.

Akavache V11 introduces a host of new features and improvements that make it a compelling choice for modern .NET applications. 

Here’s a detailed look at what lead to this release and the key enhancements:

Developed with feedback from the community, V11 focuses on flexibility, performance, and ease of use through a incubator phase that saw multiple release versions in ReactiveMarbles CacheDatabase.

This lead to a more stable and feature-rich base for the release in V11.

---

### 1️⃣ Builder Pattern for Initialization — Fluent, Flexible, Future-Proof

**The old way:** V10 relied on static singletons for setup. It worked… until you needed dependency injection, multiple caches, or platform‑specific config.

**The V11 way:** A fluent **Builder Pattern** that plays perfectly with DI containers and modular app composition.

This is aimed to mirror of the V10 static BlobCache, but with far more flexibility, renamed to CacheDatabase to better reflect its purpose and to avoid confusion with the IBlobCache interface.

```csharp
AppBuilder.CreateSplatBuilder()
    .WithAkavacheCacheDatabase<SystemJsonSerializer>(builder =>
        builder.WithApplicationName("MyApp")
               .WithSqliteDefaults());
```

💡 **Why it’s better**  
You get *readable*, *declarative*, and *scalable* configuration. In a single glance, you can see your database, serialization strategy, encryption choices, and settings store setup. It’s also far easier to tweak for testing, staging, or production environments.

---

### 2️⃣ Multiple Serializers — Finally, the Freedom of Choice

Serialization can be a silent performance killer, or a compatibility hero. Akavache V11 hands you the controls:

- **System.Text.Json** → Fast, modern, built-in, has source generation support.
- **Newtonsoft.Json** → Rich features and edge‑case handling.
- **BSON** Options → Binary JSON for legacy compatibility.

```csharp
var serializer = new SystemJsonSerializer()
{
    Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
};

AppBuilder.CreateSplatBuilder()
    .WithAkavache<SystemJsonSerializer>(() => serializer, builder =>
        builder.WithApplicationName("MyApp")
               .WithSqliteDefaults());
```

💡 **Why it’s better**  
Pick the right tool for your data. 

Optimised for speed, storage size, some backward compatibility.

Migration of old caches without rewriting them would be nice, some testing has shown it works well, but not guaranteed.
Given the possibility of breaking changes in serializers, it’s wise to test thoroughly when switching and ensure your data integrity remains intact.
Make sure to back up existing caches before attempting a migration.

---

### 3️⃣ Cross-Serializer Compatibility — Read Legacy Data, Today

Switching serializers used to mean losing old cache data. V11 introduces **cross‑serializer compatibility**, meaning you can read JSON created by another serializer, or even BSON‑encoded data from earlier Akavache versions.

💡 **Scenario:**  
Migrating an enterprise app from Newtonsoft.Json to System.Text.Json without forcing every user to lose offline data.

We recommend testing this feature thoroughly in your specific use case to ensure data integrity and compatibility. While the cross-serializer compatibility aims to facilitate smoother transitions, edge cases may arise depending on the complexity of your data structures and serialization settings. Always back up your existing cache before attempting to read legacy data with a new serializer.
Let us know if you encounter any issues or have feedback on how this works in practice!
Is there a specific scenario you have in mind where cross-serializer compatibility would be particularly beneficial? We’d love to hear about it!

---

### 4️⃣ Modular NuGet Packages — Only What You Need

In V10, Akavache was “all or nothing.” In V11, it’s **Lego bricks**:

- `Akavache` for the base in-memory cache
- `Akavache.Sqlite3` for Sqlite persistence
- `Akavache.SystemTextJson` or `Akavache.NewtonsoftJson`
- `Akavache.Drawing` for image caching
- `Akavache.Settings` for typed settings

💡 **Why it’s better**  
Trim your app size, reduce dependency overhead, and comply with AOT/linker constraints by avoiding unused features.

---

### 5️⃣ First-Class .NET MAUI Support

With MAUI becoming the unified story for .NET cross‑platform apps, Akavache V11 is ready out‑of‑the‑box.
No additional setup beyond the builder pattern.
Integrates seamlessly with MAUI’s lifecycle and DI via ReactiveUI's `Splat` and `Microsoft.Extensions.DependencyInjection` with the Splat adapter.

Akavache comes with the Splat.Builder base package which provides extension methods for the Builder and to register Akavache with Splat's dependency injection system.

```csharp
AppBuilder.CreateSplatBuilder()
    .WithAkavache<SystemJsonSerializer>(
        "MyMauiApp",
        builder =>
        builder.WithForceDateTimeKind(DateTimeKind.Utc)
               .WithSqliteDefaults(),
        (splat, instance) =>
        {
            // Register the IBlobCache instances with Splat for DI
            splat.RegisterLazySingleton<IBlobCache>(() => instance.LocalMachine, contract: nameof(instance.LocalMachine));
            splat.RegisterLazySingleton<IAkavacheInstance>(() => instance, contract: instance.ApplicationName);
        });
```

💡 **Why it’s better**  
Native feel across Android, iOS, macOS, and Windows — without writing custom caching layers per platform.

---

### 6️⃣ Improved Encrypted Cache — Security Without Compromise

Sensitive data storage is no longer an afterthought. The **Secure** cache type in V11 uses robust, cross‑platform encryption.
Based on Sqlite Cipher with encryption extensions, it ensures your tokens, credentials, and PII are safe.
The only requirement you have is to provide a secure key management strategy in your app.

💡 **Scenario:**  
Store OAuth tokens or API keys securely without rolling your own crypto.

```csharp
await CacheDatabase.Secure.InsertObject("apiToken", tokenObject);
```

---

### 7️⃣ Settings Database — Type-Safe, Persistent, Optional Encryption

Your app’s persistent settings deserve **type safety** and **structure**, not magic strings.

```csharp
public class AppSettings : SettingsBase
{
    public AppSettings() : base(nameof(AppSettings)) {}
    public bool EnableNotifications { get => GetOrCreate(true); set => SetOrCreate(value); }
    public string Theme { get => GetOrCreate("Light"); set => SetOrCreate(value); }
}
```

💡 **Why it’s better**  
No more `Get("SomeKey", defaultValue)`. Strong typing = compiler checks = fewer bugs.

This is available with optional encryption, so sensitive settings can be protected as needed.

```csharp
    YourSettings? settings;
    var instance = CacheDatabase.CreateBuilder()
        .WithApplicationName("MyApp")
        .WithSecureSettingsStore<YourSettings>(s => settings = s)
        .Build();

   // Use settings - note there is a small delay before settings are loaded due to async initialization
   // Consider awaiting 100 ms before accessing settings to ensure they are loaded
    var enableFeature = settings?.EnableFeature ?? false;
---

### 8️⃣ Image Caching with Akavache.Drawing

Perfect for profile pictures, offline product images, or galleries.

```csharp
var image = await CacheDatabase.LocalMachine.LoadImageFromUrl("https://example.com/photo.jpg");
await CacheDatabase.LocalMachine.SaveImage("user_photo", imageBitmap);
```

💡 **Why it’s better**  
Eliminates repeated network calls, speeds up UI rendering, and supports manipulation (resize, crop, thumbnail).

Each cache has its own HttpService instance for efficient downloading and caching of images.
This is automatically created when you use the Drawing extension methods otherwise its uninitialized.

This removes the need to rely on Static HttpService which was a common source of issues in V10.

---

### 9️⃣ Advanced Features You’ll Actually Use

- **Bulk Operations** → Insert/fetch multiple objects in one hit for massive performance gains.
- **Expiration Policies** → Control freshness for API data.
- **Mixed Object Storage** → Different data types in the same cache.
- **Cache Inspection** → Debug and monitor cache usage.

---

## 📊 Performance Insights

Benchmarks show **10x faster bulk writes** compared to one‑by‑one inserts.  
System.Text.Json is now the fastest supported serializer — perfect for high‑volume caching scenarios.

For numbers, see the [Benchmark Report](https://github.com/reactiveui/Akavache#performance-reports).

---

## 🚦 Migration from V10

The shift to the builder pattern means **more control with minimal pain**.  

**Basic upgrade path:**

1. Swap static init for the builder.
2. Pick your serializer.
3. Split your dependencies into modular packages.
1. Add any custom Builder configuration you need.
4. Test thoroughly.

Migration helpers are included to preserve data during the transition.

---

## 💡 Real-World Scenarios

- **Offline-first MAUI app** caching API data, credentials, and product images.
- **WPF desktop tool** preserving UI preferences and restoring them instantly.
- **Hybrid cloud client** with local caching to survive intermittent connectivity.
- **Secure mobile app** storing sensitive tokens in an encrypted cache.
- **Cross-platform game** caching assets and player progress seamlessly.
- **IoT device** logging sensor data locally with periodic sync to the cloud.

---

## 🎯 Conclusion

Akavache V11 is **more than just a cache** — it’s a **platform** for building offline‑capable, high‑performance .NET apps with elegance and confidence.

Whether you need **blazing‑fast in‑memory storage**, **robust encrypted persistence**, or **cross‑platform simplicity**, V11 lets you do it without boilerplate or compromise.

🔗 **Get started today:** [Akavache on GitHub](https://github.com/reactiveui/Akavache)

---
