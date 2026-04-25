#nullable enable

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// One entry in the docfx <c>metadata</c> array. Each entry produces an
/// independent compilation, which is essential for our setup because the
/// .NET Framework and modern .NET TFMs cannot share a single compilation
/// without type-system collisions (mscorlib vs. System.Runtime).
/// </summary>
/// <param name="Src">The set of source directories and files this metadata entry consumes.</param>
/// <param name="Dest">Output directory (relative to the docfx working directory) for the generated YAML.</param>
internal sealed record DocfxMetadataEntry(
    List<DocfxMetadataSource> Src,
    string Dest)
{
    /// <summary>
    /// Captures every metadata property other than <c>src</c> and <c>dest</c>
    /// when reading from the template, and re-emits them verbatim on each
    /// generated entry. This preserves user-controlled settings such as
    /// filters, references and properties without having to model them.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra { get; init; }
}
