#nullable enable

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// The docfx <c>build</c> section of the configuration file. Holds the
/// content array we patch with platform-specific entries, plus extension
/// data for everything else (resources, templates, output settings) that we
/// preserve verbatim from the template.
/// </summary>
/// <param name="Content">Ordered list of build content entries. Patched in place: previously-injected platform entries are removed and a fresh set is appended.</param>
internal sealed record DocfxBuildSection(List<DocfxBuildContent> Content)
{
    /// <summary>
    /// Catch-all for non-<c>content</c> properties of the build section
    /// (resources, dest, template, globalMetadata, etc.). Round-tripped
    /// unchanged.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra { get; init; }
}
