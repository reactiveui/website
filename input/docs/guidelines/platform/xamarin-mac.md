# Xamarin Mac

Please review [https://developer.xamarin.com/guides/mac/advanced\_topics/target-framework/](https://developer.xamarin.com/guides/mac/advanced_topics/target-framework/)

This platform has two different base class libraries:

* Mobile which is a custom, super lean and restrictive subset of NET45 that excludes many Windowisms. 

> /Library/Frameworks/Xamarin.Mac.framework/Versions/Current/lib/mono/Xamarin.Mac/

* XM 4.5 which looks, acts and barks like a normal .NET program but doesn't include things like System.Drawing and System.Windows.Forms on OSX. This allows you to easily consume standard "desktop" libraries that target NET45 via NuGet as long as the library does not depend on assemblies not available in XM45.

> /Library/Frameworks/Xamarin.Mac.framework/Versions/Current/lib/mono/4.5/

_ReactiveUI-Events_ and _ReactiveUI_ use Xamarin.Mac Mobile but can be consumed into a Xamarin Mac XM 4.5 application. We would love to ship XM45 assemblies with the project but [it is currently impossible to do so](https://github.com/NuGet/Home/issues/2662).