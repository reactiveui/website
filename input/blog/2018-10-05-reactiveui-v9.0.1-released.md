ShowInSidebar: false
---
title: ReactiveUI v9.0.1 released
category: 
  - Release Notes
author: Glenn Watson
---

Release 9.0.1 of ReactiveUI is available!

## What's Changed

Please note the biggest change is ReactiveList, IReactiveDerivedList are being marked as deprecated. Recommend the use of DynamicData instead https://github.com/RolandPheasant/DynamicData

* Added Benchmark Harness (#1729) (#1729) @RLittlesII
* feature: Add a constructor to take the constructor to RoutingState (#1730) @glennawatson
* fix: All bench marks are now correctly running (#1731) @glennawatson
* housekeeping: Make the ReactiveList and related interfaces and components Obsolete. (#1727) @glennawatson
* Made the ReactivePagerAdapter not rely on ReactiveList. (#1736) @glennawatson
* housekeeping: update integration tests to latest nuget versions. (#1738) @glennawatson
* feature: Add app for the getting started guide (#1740) @worldbeater
* Add MasterDetailPage sample (#1741) @cabauman
* housekeeping: Make debug type use portable format and embedded. (#1742) @glennawatson
* Fix Xamarin.Forms RoutedViewHost exception (#1744) @cabauman
* housekeeping: update public api generator to allow for embedded pdbs. (#1743) @glennawatson
* samples: Add xamarin page routing and support various detail views (#1745) (#1745) @cabauman
* consolidate files (#1747) @onovotny
