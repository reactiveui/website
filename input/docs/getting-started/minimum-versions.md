
## Visual Studio Minimums

Visual Studio 2019 and beyond.

## Platform Minimums

ReactiveUI has the following minimum platform requirements:

| Platform | Minimum |
|----------|---------|
| .Net Framework | 4.6.1 |
| .Net Standard | 2.0 |
| .Net Core | 3.1 |
| .Net | 5.0 |
| UWP | 10.0.16299.0 |
| Xamarin Android | 10.0 |
| Xamarin iOS | 1.0 |
| Xamarin Mac | 2.0 |
| Xamarin TVOS | 1.0 |
| Tizen | 4.0 |

## Android Minimums

Android 10.0 is the minimum, with SDK 29.

Make sure your `AndroidManifest.xml` has SDK 29 as the target SDK.

```diff
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.giusepe.SextantSample" android:installLocation="auto">
+	<uses-sdk android:minSdkVersion="19" android:targetSdkVersion="29" />
 	<application android:label="SextantSample.Android"></application>
</manifest> 
```

Also that your `CSPROJ` references Android 10.0.
