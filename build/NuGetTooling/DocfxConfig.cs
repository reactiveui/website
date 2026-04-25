#nullable enable

using System.Collections.Generic;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// Strongly-typed model of the subset of <c>docfx.json</c> that the build
/// generates and rewrites. Top-level docfx properties we don't touch are
/// not modelled because the generator produces a complete file from the
/// template each run.
/// </summary>
/// <param name="Metadata">Ordered list of metadata entries — one per lib TFM that has matching reference assemblies.</param>
/// <param name="Build">The build section, copied from the template with the content array patched to include platform-specific outputs.</param>
internal sealed record DocfxConfig(
    List<DocfxMetadataEntry> Metadata,
    DocfxBuildSection Build);
