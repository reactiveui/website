#nullable enable

using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// System.Text.Json source-generated serialiser context for the docfx
/// configuration model. All format settings (camelCase, indented output,
/// null-property suppression) live here as attributes so the call sites
/// don't need to construct or pass <see cref="JsonSerializerOptions"/>.
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(DocfxConfig))]
internal sealed partial class DocfxConfigContext : JsonSerializerContext
{
}
