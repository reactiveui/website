---
Order: 14
---
# Draw and resize a parcel label

Use `Splat.SkiaSharp` when your app needs to draw a bitmap and send a smaller copy to a preview. Register the loader once. The same loader can create an empty bitmap and read the encoded image back into memory.

```csharp
using SkiaSharp;
using Splat;
using Splat.SkiaSharp;

using var resolver = new ModernDependencyResolver();
resolver.UseSkiaSharpBitmapLoader();
var loader = (SkiaBitmapLoader)resolver.GetService<IBitmapLoader>()!;
using var label = loader.Create(32, 32);
var color = SplatColor.SteelBlue;
label.ToNative().Erase(new SKColor(color.R, color.G, color.B, color.A));

await using var encoded = new MemoryStream();
await label.Save(CompressedBitmapFormat.Png, 1, encoded).WaitAsync(token);
using var preview = loader.LoadFromMemory(encoded.ToArray(), 8, 8, token);
```

Here `token` is the cancellation token for the preview request. `WaitAsync` lets the caller cancel its wait. The loader accepts the token directly when decoding. Check the returned preview before reading its pixels. Dispose both bitmaps when their views release them.

To keep a barcode in the same place when scaling the label, split the label rectangle with `Divide`, take the barcode region's `Center()` and scale that point by the preview ratio. The example caches the center for each label size with `MemoizingMRUCache`. It checks both the scaled position and the decoded pixels. [Parcel label example](https://github.com/reactiveui/splat/blob/main/src/examples/DispatchAdapters/ParcelLabel.cs)
