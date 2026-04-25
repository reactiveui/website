#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Nuke.Common.IO;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// Generates the docfx configuration consumed by the build, dynamically
/// patching <c>metadata</c> and <c>build.content</c> based on the TFM
/// directories that <see cref="NuGetFetcher"/> has materialised under
/// <c>api/lib</c> and <c>api/refs</c>.
/// </summary>
internal static class DocfxConfigWriter
{
    /// <summary>
    /// Reads the template <c>reactiveui/docfx.json</c>, rewrites the
    /// <c>metadata</c> and <c>build.content</c> sections to reflect the
    /// discovered TFMs, and writes the result to <paramref
    /// name="outputPath"/>. One metadata entry is produced per lib TFM that
    /// has matching reference assemblies; platform TFMs are bucketed into
    /// per-platform output directories so the docfx site can switch between
    /// them.
    /// </summary>
    /// <param name="rootDirectory">Repository root.</param>
    /// <param name="outputPath">File to write the generated docfx configuration to.</param>
    /// <returns>The same <paramref name="outputPath"/>, for fluent use by the caller.</returns>
    public static AbsolutePath Write(AbsolutePath rootDirectory, AbsolutePath outputPath)
    {
        var apiDir = rootDirectory / "reactiveui" / "api";
        var libDir = apiDir / "lib";
        var refsDir = apiDir / "refs";
        var docfxTemplatePath = rootDirectory / "reactiveui" / "docfx.json";

        if (!Directory.Exists(libDir))
        {
            throw new DirectoryNotFoundException($"No lib/ directory found at {libDir}");
        }

        List<string> libTfms =
        [
            .. Directory.GetDirectories(libDir)
                .Where(d => Directory.GetFiles(d, "*.dll").Length > 0)
                .Select(d => Path.GetFileName(d)!)
                .OrderBy(t => t),
        ];

        if (libTfms.Count == 0)
        {
            throw new InvalidOperationException("No TFM directories with DLLs found in lib/");
        }

        List<string> refsTfms = Directory.Exists(refsDir)
            ?
            [
                .. Directory.GetDirectories(refsDir)
                    .Where(d => Directory.GetFiles(d, "*.dll").Length > 0)
                    .Select(d => Path.GetFileName(d)!),
            ]
            : [];

        Log.Info($"Discovered lib/ TFMs: {string.Join(", ", libTfms)}");
        Log.Info($"Discovered refs/ TFMs: {string.Join(", ", refsTfms)}");

        var template = ReadTemplate(docfxTemplatePath);
        var sharedExtra = ExtractSharedMetadataExtras(template.Metadata.FirstOrDefault());

        var metadataEntries = new List<DocfxMetadataEntry>();
        var platformLabels = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var tfm in libTfms)
        {
            var bestRef = TfmResolver.FindBestRefsTfm(tfm, refsTfms);
            if (bestRef == null)
            {
                Log.Info($"No matching refs for lib/{tfm}, skipping metadata entry");
                continue;
            }

            var platformLabel = TfmResolver.GetPlatformLabel(tfm);
            var dest = platformLabel != null ? $"api-{platformLabel}" : "api";
            if (platformLabel != null)
            {
                platformLabels.Add(platformLabel);
            }

            var refDir = Path.Combine(refsDir, bestRef);
            var refDllNames = Directory.Exists(refDir)
                ? new HashSet<string>(
                    Directory.GetFiles(refDir, "*.dll").Select(f => Path.GetFileName(f)),
                    StringComparer.OrdinalIgnoreCase)
                : new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var libTfmDir = Path.Combine(libDir, tfm);
            List<string> packageDlls =
            [
                .. Directory.GetFiles(libTfmDir, "*.dll")
                    .Select(f => Path.GetFileName(f))
                    .Where(f => !refDllNames.Contains(f))
                    .OrderBy(f => f, StringComparer.Ordinal),
            ];

            if (packageDlls.Count == 0)
            {
                Log.Info($"No package DLLs in lib/{tfm} (only refs), skipping");
                continue;
            }

            var entry = new DocfxMetadataEntry(
                Src: [new DocfxMetadataSource($"api/lib/{tfm}", packageDlls)],
                Dest: dest)
            {
                Extra = sharedExtra,
            };

            metadataEntries.Add(entry);
            Log.Info($"Metadata entry: lib/{tfm} ({packageDlls.Count} DLLs, refs from {bestRef}) -> {dest}");
        }

        List<string> orderedPlatforms = [.. platformLabels.OrderBy(l => l, StringComparer.Ordinal)];
        var patchedBuild = PatchBuildSection(template.Build, orderedPlatforms);
        var generated = new DocfxConfig(metadataEntries, patchedBuild);

        Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
        var json = JsonSerializer.Serialize(generated, DocfxConfigContext.Default.DocfxConfig);
        File.WriteAllText(outputPath, json);

        Log.Info($"Wrote generated docfx config with {metadataEntries.Count} metadata entries to {outputPath}");
        return outputPath;
    }

    /// <summary>
    /// Reads and deserialises the template <c>docfx.json</c>. Throws when
    /// the file is missing or empty so misconfiguration surfaces as a clear
    /// error rather than a downstream null-reference.
    /// </summary>
    /// <param name="docfxTemplatePath">Path to the template file.</param>
    /// <returns>The parsed template configuration.</returns>
    private static DocfxConfig ReadTemplate(AbsolutePath docfxTemplatePath)
    {
        var docfxJson = File.ReadAllText(docfxTemplatePath);
        return JsonSerializer.Deserialize(docfxJson, DocfxConfigContext.Default.DocfxConfig)
            ?? throw new InvalidOperationException(
                $"Failed to deserialize docfx template at {docfxTemplatePath}");
    }

    /// <summary>
    /// Returns the extension data (<c>filter</c>, <c>references</c>,
    /// <c>properties</c> etc.) hanging off the template's first metadata
    /// entry, with <c>references</c> stripped because each generated entry
    /// supplies its own resolution via co-located ref DLLs.
    /// </summary>
    /// <param name="templateEntry">First metadata entry from the template, if any.</param>
    /// <returns>A copy of the extension data (or <see langword="null"/> when there are no shared extras to preserve).</returns>
    private static Dictionary<string, JsonElement>? ExtractSharedMetadataExtras(DocfxMetadataEntry? templateEntry)
    {
        if (templateEntry?.Extra is null || templateEntry.Extra.Count == 0)
        {
            return null;
        }

        var copy = new Dictionary<string, JsonElement>(templateEntry.Extra);
        copy.Remove("references");
        return copy.Count == 0 ? null : copy;
    }

    /// <summary>
    /// Returns a copy of the template build section with previously-injected
    /// platform <c>api-*</c> content entries removed and a fresh set
    /// appended for the platforms discovered this run.
    /// </summary>
    /// <param name="template">Build section from the template.</param>
    /// <param name="platformLabels">Platform labels to inject content entries for, in deterministic order.</param>
    /// <returns>The patched build section.</returns>
    private static DocfxBuildSection PatchBuildSection(DocfxBuildSection template, List<string> platformLabels)
    {
        List<DocfxBuildContent> content = [.. template.Content.Where(item => !IsInjectedPlatformEntry(item))];

        foreach (var label in platformLabels)
        {
            content.Add(new DocfxBuildContent(
                Files: [$"api-{label}/**.yml", $"api-{label}/index.md"]));
        }

        return new DocfxBuildSection(content)
        {
            Extra = template.Extra,
        };
    }

    /// <summary>
    /// Detects content entries previously injected by this writer (whose
    /// first file path begins with <c>api-</c> but is not the canonical
    /// <c>api/</c> path) so they can be replaced rather than accumulated.
    /// </summary>
    /// <param name="entry">A build content entry from the template.</param>
    /// <returns><see langword="true"/> if the entry was previously injected by this writer.</returns>
    private static bool IsInjectedPlatformEntry(DocfxBuildContent entry)
    {
        if (entry.Files is null || entry.Files.Count == 0)
        {
            return false;
        }

        var firstFile = entry.Files[0];
        return firstFile.StartsWith("api-", StringComparison.Ordinal)
            && !firstFile.StartsWith("api/", StringComparison.Ordinal);
    }
}
