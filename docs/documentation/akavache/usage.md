---
Order: 4
---
# Usage

Common operations:
```csharp
// strings/bytes
await ak.LocalMachine.Insert("msg", Encoding.UTF8.GetBytes("hello"));
var bytes = await ak.LocalMachine.Get("msg");

// POCO
await ak.LocalMachine.InsertObject("user", user);
var user2 = await ak.LocalMachine.GetObject<User>("user");

// GetOrFetch with expiration (throttles dup requests)
var posts = await ak.LocalMachine.GetOrFetchObject("posts",
    () => api.GetPostsAsync(), DateTimeOffset.Now.AddMinutes(5));

// invalidate
await ak.LocalMachine.Invalidate("user");
await ak.LocalMachine.InvalidateAll();
```
