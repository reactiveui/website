---
Order: 4
---
# Visual Studio Minimums

Visual Studio 2022 and beyond. Currently, Visual Studio 2026 is the latest version and recommended for the best experience.

## Platform Minimums

ReactiveUI has the following minimum platform requirements:

| Platform | Minimum |
|----------|---------|
| .NET Framework | 4.6.2 (any 4.6.2 – 4.8.1; binaries published as `net462`/`net472`/`net481`) |
| .NET | 8.0 (also 9.0, 10.0) |
| Windows | 10.0.19041.0 |
| Android (core / MAUI) | API 24 (Android 7.0) |
| Android (AndroidX) | API 34 (Android 14) |
| iOS / Mac Catalyst | 15.0 |
| Tizen | 6.5 |

## Android Minimums

The core `ReactiveUI` and `ReactiveUI.Maui` Android targets compile against API 24 (Android 7.0). `ReactiveUI.AndroidX` requires API 34 (Android 14).

Make sure your `AndroidManifest.xml` targets a compatible SDK:

```diff
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.giusepe.SextantSample" android:installLocation="auto">
+	<uses-sdk android:minSdkVersion="24" android:targetSdkVersion="34" />
 	<application android:label="SextantSample.Android"></application>
</manifest> 
```

Also ensure your `csproj` targets `net10.0-android` (or `net8.0`/`net9.0`-android) with a `SupportedOSPlatformVersion` of at least 24 (or 34 for AndroidX).
