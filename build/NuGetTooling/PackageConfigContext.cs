#nullable enable

using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// System.Text.Json source-generated serialiser context for <see
/// cref="PackageConfig"/> and its nested record types. Using the source
/// generator keeps the build project free of reflection-based deserialisation
/// (which would otherwise trigger CA1812 on the records since the analyser
/// cannot see the reflective construction).
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(PackageConfig))]
internal sealed partial class PackageConfigContext : JsonSerializerContext
{
}
