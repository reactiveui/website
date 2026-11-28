---
Order: 7
---
# Cache images and thumbnails

Use `Akavache.Drawing` to keep a photo for offline viewing. Apps using System.Reactive can use `Akavache.Drawing.Reactive`. Register your platform's Splat bitmap loader before saving or loading images.

## Keep the original and a list preview

Give the thumbnail its own key so a list can read the smaller image without requesting the full-size photo. This example takes an `IBlobCache cache`, a Splat `IBitmap photo` and a cancellation token.

```csharp
var expires = cache.Scheduler.Now.AddDays(1);
await cache.SaveImage("delivery/42/photo", photo, expires).ToTask(token);
await cache.CreateAndCacheThumbnail(
    "delivery/42/photo", "delivery/42/photo/preview", 96, 96, expires).ToTask(token);
using var preview = await cache.LoadImage("delivery/42/photo/preview").ToTask(token);
```

Keep the returned bitmap alive while the view displays it. Dispose it when the view releases it. The example shows how to use the same code with either package family. [Delivery photo example](https://github.com/reactiveui/Akavache/blob/main/src/examples/OrderCache/DeliveryPhoto.cs)

## Download a photo or use a fallback

Call `LoadImageFromUrl` with a URI to download a photo through the cache's HTTP service. Pass `fetchAlways: true` when the user requests a fresh copy. Use `LoadImageFromUrlWithFallback` with your placeholder image bytes when the view needs an image even if the download fails.

For a gallery, `PreloadImagesFromUrls` caches downloads ahead of time. `LoadImages` returns key and bitmap pairs for entries it can read. To remove an order's images, pass a key predicate such as `key => key.StartsWith("delivery/42/", StringComparison.Ordinal)` to `ClearImageCache`.
