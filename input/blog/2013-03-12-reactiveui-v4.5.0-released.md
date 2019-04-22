---
title: ReactiveUI v4.5.0 released
category: Release Notes
author: Anaïs Betts
---

# [What's New](https://github.com/reactiveui/reactiveui/compare/4.4.3...4.5.0)

## Xamarin.iOS and Xamarin.Mac support

This release brings initial support for the latest Xamarin.Mac and Xamarin.iOS. Using ReactiveUI and Xamarin Studio, you can write cross-platform ViewModels and Model classes and bind them to Views using the same RxUI 4.x binding syntax.
- ReactiveUI knows how to bind to Cocoa objects using Key-Value Observing
- DeferredScheduler automatically runs code on the Cocoa main runloop
- Routing and ViewModelViewHost is supported via a class that attaches to NSView
- Command binding understands Cocoa controls via Cocoa target/action framework, including automatically disabling the control when CanExecute is false
- Many common controls have implicit binding support

## Experimental Xamarin.Android support

This release also includes very basic Xamarin.Android support, including a scheduler that will run code on the Activity's main thread.

## What do I need to run this??

You need to run [Xamarin Studio](http://xamarin.com/) and make sure to have the latest updates installed from the **Alpha Channel**. 

## What else is New?
- Bug fixes in UserError Handling
- Extra documentation (thanks @ArturPhilibin!)
- Add Debugger support for ReactiveCollection (#204, thanks @distantcam!)
