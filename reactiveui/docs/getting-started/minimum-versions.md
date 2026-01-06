# Visual Studio Minimums

Visual Studio 2022 and beyond. Currently, Visual Studio 2026 is the latest version and recommended for the best experience.

## Platform Minimums

ReactiveUI has the following minimum platform requirements:

| Platform | Minimum |
|----------|---------|
| .Net Framework | 4.6.2 |
| .Net Standard | 2.0 |
| .Net | 8.0 |
| Windows | 10.0.19041.0 |
| Xamarin Android | 15.0 |
| Tizen | 4.0 |

## Android Minimums

Android 15.0 is the minimum, with SDK 34.

Make sure your `AndroidManifest.xml` has SDK 34 as the target SDK.

```diff
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.giusepe.SextantSample" android:installLocation="auto">
+	<uses-sdk android:minSdkVersion="19" android:targetSdkVersion="34" />
 	<application android:label="SextantSample.Android"></application>
</manifest> 
```

Also that your `CSPROJ` references Android 15.0.
