---
title: ReactiveUI v5.4.0 released
category: Release Notes
author: Anaïs Betts
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/5.3.0...5.4.0)

Since some of these fixes change behavior (such as the NLog fix), this is a minor release - however, most of these changes _shouldn't_ affect existing programs

### Bug Fixes
- Allow binding to 3rd party control libraries in WinForms (#405, thanks @rikbosch)
- Fix F# friendly ObservableForProperty (#407, thanks @marklam)
- Clean up NuSpec files for Xamarin on Visual Studio (#410, thanks @onovotny)
- Fixes to deserializing ReactiveObjects using certain serializers (#412, thanks @meteficha)
- Small improvements to derived collections (#417, thanks @meteficha)
- Change NLog to use the full class name so it is easier to filter on (thanks @npnelson)
- Race condition and reentrancy fixes to Reactive(Table/Collection)ViewSource (#425 + #426 + #433, thanks @meteficha)
- Remove Pex from the list of test runners because it conflicts with WriteableBitmapEx (#428, thanks @tiagomargalho)
- Fix up some of the collection interfaces (#430, thanks @Haacked)
- Add Count\* observables to list interfaces (#436, thanks @onovotny)
- Disable setting up ViewHosts in design mode
