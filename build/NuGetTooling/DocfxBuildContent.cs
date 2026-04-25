#nullable enable

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// One entry in the docfx <c>build.content</c> array. Most entries simply
/// list the files docfx should consume; some carry additional settings
/// (such as <c>exclude</c> or <c>src</c>) that are preserved verbatim via
/// <see cref="Extra"/>.
/// </summary>
/// <param name="Files">Optional list of file globs the entry consumes. <see langword="null"/> for entries that only carry extension data.</param>
internal sealed record DocfxBuildContent(List<string>? Files = null)
{
    /// <summary>
    /// Catch-all for additional properties present on the entry in the
    /// template (for example <c>exclude</c>). Round-tripped unchanged so we
    /// don't have to model the full docfx schema.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra { get; init; }
}
