---
title: Assemblies are now code signed with the .NET Foundation  certificate
category: Announcement
author: Geoffrey Huntley
---

Thanks to a legendary contribution from Oren Novotny, all ReactiveUI assemblies from `v8.0.0-alpha73` and onwards are code signed with the .NET Foundation certificate. Identity signing of the assemblies means that you can [verify the authenticity of the binaries](/docs/security/)  used in your application and ensure they have not been tampered with as only assemblies compiled via the .NET Foundation CI infrastructure are signed with this certificate.
