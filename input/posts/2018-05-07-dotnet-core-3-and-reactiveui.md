NoTitle: true
IsBlog: true
Title: .NET Core 3 and ReactiveUI
Tags: 
  - Announcement
Author: Geoffrey Huntley
---

At Microsoft Build Live today, Microsoft [shared a first look at their plans](
https://blogs.msdn.microsoft.com/dotnet/2018/05/07/net-core-3-and-support-for-windows-desktop-applications/
) for .NET Core 3. The highlight of .NET Core 3 is support for Windows desktop applications, specifically Windows Forms, Windows Presentation Framework (WPF), and UWP XAML. Soon you will be able to run new and existing Windows desktop applications on .NET Core and enjoy all the benefits that .NET Core has to offer.

<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr">I’m extremely excited about us bringing WPF and WinForms to .NET Core. <a href="https://t.co/eZnljtanml">https://t.co/eZnljtanml</a></p>&mdash; Immo Landwerth #msbuild (@terrajobst) <a href="https://twitter.com/terrajobst/status/993601917795221504?ref_src=twsrc%5Etfw">May 7, 2018</a></blockquote>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>

ReactiveUI had some heads up what was coming and added `netcoreapp20` as a supported platform as part of [the release of v8.0.0 last week](https://reactiveui.net/blog/2018/05/reactiveui-v8.0.0-released). When `netcoreapp30` lands ReactiveUI fully intends to support these scenarios. It's unclear at this stage if it means that WPF/WinForms will be able to run on other platforms in the future - I seriously doubt it as it would be a massive undertaking but if Microsoft does it - ReactiveUI will support it. 

If you need to ship cross-platform applications on .NET core right now, then the maintainers of ReactiveUI recommends that you check out [Avalonia](https://github.com/AvaloniaUI/Avalonia). Their community is as active, as vibrant as ours, open by default and [you can drop in for a chat](https://gitter.im/AvaloniaUI/Avalonia).

<div class="youtube-video-container"><iframe src="https://www.youtube.com/embed/wHcB3sGLVYg" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

If you want to get a head start on .NET Core 3.0 and want to make it possible sooner - send in a PR that 🚢's this:

[https://github.com/reactiveui/ReactiveUI/issues/1575](https://github.com/reactiveui/ReactiveUI/issues/1575)
