---
NoTitle: true
IsBlog: true
Title: Assemblies are now code signed with the .NET Foundation  certificate
Tags: Announcement
Author: Geoffrey Huntley
Published: 2017-09-05
---

Thanks to a legendary contribution from Oren Novotny, all ReactiveUI assemblies from `v8.0.0-alpha73` and onwards are code signed with the .NET Foundation certificate. Identity signing of the assemblies means that you can [verify the authenticity of the binaries](../docs/security/index.md)  used in your application and ensure they have not been tampered with as only assemblies compiled via the .NET Foundation CI infrastructure are signed with this certificate.
