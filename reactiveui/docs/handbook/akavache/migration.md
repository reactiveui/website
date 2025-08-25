# Migration (v10 ? v11)

Key changes:
- Static BlobCache ? builder + IAkavacheInstance
- Modular packages
- Pluggable serializers

Example:
```csharp
// v10
BlobCache.LocalMachine.InsertObject("user", user);

// v11
var ak = Locator.Current.GetService<IAkavacheInstance>(contract: "MyApp");
ak.LocalMachine.InsertObject("user", user);
```

Test serializer migration paths and back up caches.
