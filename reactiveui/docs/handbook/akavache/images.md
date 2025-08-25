# Images

Akavache.Drawing helpers:
```csharp
var bmp = await ak.LocalMachine.LoadImageFromUrl("https://example.com/p.jpg");
await ak.LocalMachine.SaveImage("user:photo", bmp);
var result = await ak.LocalMachine.GetImage("user:photo");
```

Each cache has its own HttpService instance used by these helpers (initialized automatically).
