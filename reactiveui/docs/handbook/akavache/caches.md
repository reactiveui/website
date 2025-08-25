# Caches

Akavache exposes logical caches through a single instance:
- LocalMachine: persistent general-purpose cache
- Secure: encrypted persistent cache
- InMemory: fast process-local cache

```csharp
var ak = Locator.Current.GetService<IAkavacheInstance>(contract: "MyApp");
await ak.LocalMachine.InsertObject("user:42", user);
var u = await ak.LocalMachine.GetObject<User>("user:42");

await ak.Secure.InsertObject("token", token);
var t = await ak.Secure.GetObject<Token>("token");
```

Expiration and bulk ops:
```csharp
await ak.LocalMachine.InsertObject("weather", data, TimeSpan.FromMinutes(10));
await ak.LocalMachine.InsertObjects(items.Select(i => (i.Key, i.Value)));
var many = await ak.LocalMachine.GetObjects<string>(keys);
```
