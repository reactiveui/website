#nullable enable

using System.Collections.Generic;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// One entry in a docfx metadata <c>src</c> array. Identifies the source
/// directory and the explicit list of assemblies inside it that docfx should
/// process for this metadata block.
/// </summary>
/// <param name="Src">Repository-relative path to the source directory (for example <c>api/lib/net10.0</c>).</param>
/// <param name="Files">Explicit list of file names within <see cref="Src"/> that docfx should consume. Filenames only; the path comes from <see cref="Src"/>.</param>
internal sealed record DocfxMetadataSource(string Src, List<string> Files);
