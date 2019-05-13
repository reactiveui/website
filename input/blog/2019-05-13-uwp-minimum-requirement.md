---
title: UWP minimum version is now 10.0.17763.0
category: 
  - Release Notes
author: Glenn Watson
---

Due to a ongoing issue with [Azure DevOps](https://github.com/microsoft/azure-pipelines-image-generation/pull/770) we had to change our minimum version from 10.0.16299 to 10.0.17763.0 for the UWP platform.

Without this set as your minimum version ReactiveUI controls will be seen as missing.

To fix the issue change within your .csproj file the following line to be 10.0.17763.0 or above.

```xml
<TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
```