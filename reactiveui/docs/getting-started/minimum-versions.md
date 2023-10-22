---
NoTitle: true
---
## Visual Studio Minimums

Visual Studio 2019 and beyond.

## Platform Minimums

ReactiveUI has the following minimum platform requirements:

| Platform | Minimum |
|----------|---------|
| .Net Framework | 4.6.2 |
| .Net Standard | 2.0 |
| .Net | 6.0 |
| Windows | 10.0.17763.0 |
| Xamarin Android | 12.0 |
| Xamarin iOS | 1.0 |
| Xamarin Mac | 2.0 |
| Xamarin TVOS | 1.0 |
| Tizen | 4.0 |

## Android Minimums

Android 12.0 is the minimum, with SDK 30.

Make sure your `AndroidManifest.xml` has SDK 30 as the target SDK.

```diff
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.giusepe.SextantSample" android:installLocation="auto">
+	<uses-sdk android:minSdkVersion="19" android:targetSdkVersion="30" />
 	<application android:label="SextantSample.Android"></application>
</manifest> 
```

Also that your `CSPROJ` references Android 12.0.
