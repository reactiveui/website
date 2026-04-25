#nullable enable

using System.Collections.Generic;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// Strongly-typed model of <c>nuget-packages.json</c>. Drives every aspect of
/// the docfx package fetch: which owners to discover, which packages to
/// include or exclude, which target frameworks to prefer, and which
/// reference-only packages to extract for assembly resolution.
/// </summary>
/// <param name="NugetPackageOwners">NuGet owner accounts whose published packages should be discovered automatically via the search service.</param>
/// <param name="TfmPreference">Ordered list of preferred target frameworks. The first TFM in the package's <c>lib/</c> folder that matches an entry is selected. Earlier entries take precedence.</param>
/// <param name="AdditionalPackages">Packages to fetch in addition to those discovered through <see cref="NugetPackageOwners"/>. Useful for transitively required packages owned by third parties.</param>
/// <param name="ExcludePackages">Exact package identifiers to exclude from fetching, regardless of how they were discovered.</param>
/// <param name="ExcludePackagePrefixes">Package identifier prefixes to exclude from fetching. Matches case-insensitively.</param>
/// <param name="ReferencePackages">Reference-only packages whose assemblies are extracted for the docfx assembly resolver but not themselves documented.</param>
/// <param name="TfmOverrides">Per-package TFM overrides. Keyed by package identifier, the value forces the named TFM (with prefix-match fallback) instead of consulting <see cref="TfmPreference"/>.</param>
internal sealed record PackageConfig(
    string[] NugetPackageOwners,
    string[] TfmPreference,
    AdditionalPackage[] AdditionalPackages,
    string[] ExcludePackages,
    string[] ExcludePackagePrefixes,
    ReferencePackage[] ReferencePackages,
    Dictionary<string, string> TfmOverrides);
