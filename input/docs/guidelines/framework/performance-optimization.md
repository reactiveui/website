NoTitle: true
---
# Performance optimization

## Splat

It's recommended to override the `ModeDetector` in your application to improve startup performance. In order to do that the first thing has to be when the application starts, is to register the built-in `Mode.Run` mode.

```csharp
public App()
{
    Splat.ModeDetector.OverrideModeDetector(Mode.Run);
    
    //... the rest of the startup code.
}
```
