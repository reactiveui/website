---
title: System.Reactive has a new home on GitHub
category: Announcement
author: Geoffrey Huntley
---

At MVP Summit, [the community met with Microsoft](https://github.com/Reactive-Extensions/Rx.NET/issues/466#issuecomment-370496523) and with their support and encouragement the community forked control of System.Reactive. Over the last couple weeks some of the smartest brains from multiple ecosystems have united in [our Slack instance](https://reactiveui.net/slack) to plan for the future.

One of the items that everyone agreed upon almost instantaniously was that reactive programming on dotnet needed to have a stronger branding/marketing presense. We need to signal that `System.Reactive` is part of .NET and that living under the existing Reactive-Extensions GitHub organization did not convey that message.

Today the community, in conjunction with Microsoft shipped the first milestone on [our initial roadmap](https://reactiveui.net/blog/2018/04/the-future-of-system-reactive).

<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr"><a href="https://twitter.com/hashtag/Rx?src=hash&amp;ref_src=twsrc%5Etfw">#Rx</a>.NET is moving under <a href="https://t.co/4piNpJ5uOx">https://t.co/4piNpJ5uOx</a><br>Huge milestone, amazing achievement<br>A huge round of applause for <a href="https://twitter.com/onovotny?ref_src=twsrc%5Etfw">@onovotny</a> and <a href="https://twitter.com/GeoffreyHuntley?ref_src=twsrc%5Etfw">@GeoffreyHuntley</a></p>&mdash; Emil Petro (@petroemil) <a href="https://twitter.com/petroemil/status/992142750509355008?ref_src=twsrc%5Etfw">May 3, 2018</a></blockquote>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>


This is big news because System.Reactive for .NET is the OG and original implementation of the Reactive Extensions. Sadly it's potential was artificially limited over the years while other ecosystems saw massive growth. It's time to put that narrative to bed though. It's time to move on. Thank you Jon Galloway, Phil Carter, Bart de Smet, Immo Landwerth for your help with advancing the community past this historical narrative.

![image](https://user-images.githubusercontent.com/127353/39624525-f7ad51fc-4fdc-11e8-9546-4e56eae31033.png)

Our new home under the .NET organization on GitHub provides better discoverability and visibility. It provides CI systems that keep up with the rapid evolution that's happening in .NET. The new location is closer to .NET development itself and components from System.Reactive which are logical base class library extensions now have a possible shorter integration path into corefxlabs. A great demonstration of this is how System.Reactive's (Ix) `IAsyncEnumerable` is graduating into a first-class citizen in C# v8.

This is a new era. Join the conversation over at https://reactiveui.net/slack and ask how you can help.
