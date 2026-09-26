---
Order: 4
---
# Visual Studio Minimums

Visual Studio 2022 and beyond. Currently, Visual Studio 2026 is the latest version and recommended for the best experience.

> [!WARNING]
> ReactiveUI drops a target framework after Microsoft ends support for it. .NET 8 and .NET 9 reach
> [end of support](https://learn.microsoft.com/dotnet/core/releases-and-support) on November 10, 2026, and .NET
> Framework 4.6.2 reaches [end of support](https://learn.microsoft.com/lifecycle/end-of-support/end-of-support-2027)
> on January 12, 2027. Move to a newer target before then to keep receiving ReactiveUI updates.

## Platform Minimums

ReactiveUI has the following minimum platform requirements:

| Platform | Minimum |
|----------|---------|
| .NET Framework | 4.6.2 (any 4.6.2 – 4.8.1; binaries published as `net462`/`net472`/`net481`) |
| .NET | 8.0 (also 9.0, 10.0, 11.0) |
| Windows | 10.0.19041.0 |
| Android (core `ReactiveUI`) | API 35 (Android 15), on `net10.0-android`/`net11.0-android` only |
| Android (`ReactiveUI.Maui`) | API 24 (Android 7.0) |
| Android (`ReactiveUI.AndroidX`) | API 34 (Android 14) |
| iOS / Mac Catalyst | 15.0 |

No package ships a Tizen target. [Tizen](installation/tizen.md) explains what to use instead.

## Android Minimums

Three packages ship Android support, each with its own minimum SDK. The core `ReactiveUI` package targets only
`net10.0-android` and `net11.0-android`, and requires API 35 (Android 15). `ReactiveUI.Maui` requires API 24
(Android 7.0). `ReactiveUI.AndroidX` requires API 34 (Android 14).

Make sure your `AndroidManifest.xml` targets a compatible SDK. This example is for a `ReactiveUI.Maui` app, whose
minimum is API 24:

```diff
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.giusepe.SextantSample" android:installLocation="auto">
+	<uses-sdk android:minSdkVersion="24" android:targetSdkVersion="34" />
 	<application android:label="SextantSample.Android"></application>
</manifest> 
```

Also ensure your `csproj` targets `net10.0-android` or `net11.0-android`, the only Android targets ReactiveUI
ships, with a `SupportedOSPlatformVersion` of at least 35 for core `ReactiveUI`, 24 for `ReactiveUI.Maui`, or 34
for `ReactiveUI.AndroidX`.
