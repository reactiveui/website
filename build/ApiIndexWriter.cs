#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nuke.Common.IO;

namespace ReactiveUI.Web;

/// <summary>
/// Writes <c>docs/api/index.md</c> from whatever per-namespace dirs the
/// Zensical emitter just wrote. Kept separate from the Nuke build target
/// so the IO + Markdown shape lives in one place and can be unit-tested
/// in isolation if/when we add coverage.
/// </summary>
internal static class ApiIndexWriter
{
    private static readonly HashSet<string> _skip = new(StringComparer.OrdinalIgnoreCase)
    {
        "lib",
        "refs",
        "cache",
        "_global",
    };

    public static int Write(AbsolutePath apiPath)
    {
        var namespaces = apiPath.GlobDirectories("*")
            .Select(p => p.Name)
            .Where(name => !_skip.Contains(name))
            .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var sb = new StringBuilder();
        sb.AppendLine("# API Reference");
        sb.AppendLine();
        sb.AppendLine("The API reference is generated from the latest stable NuGet packages — types, members, signatures, XML docs, and source-link locations are walked directly out of the published `.dll` + `.xml` triples by `SourceDocParser`.");
        sb.AppendLine();
        sb.AppendLine("Use the navigation on the left to browse by namespace. Cross-references between members resolve through `mkdocs-autorefs`, so you can jump from any signature into the type it returns.");
        sb.AppendLine();
        sb.AppendLine("## Namespaces");
        sb.AppendLine();
        foreach (var ns in namespaces)
        {
            sb.AppendLine($"- [`{ns}`]({ns}/)");
        }

        sb.AppendLine();
        sb.AppendLine("!!! info \"Tracked packages\"");
        sb.AppendLine();
        sb.AppendLine("    The exact list and resolved versions are pinned in [`nuget-packages.json`](https://github.com/reactiveui/website/blob/main/nuget-packages.json) and rebuilt on every site deploy.");

        File.WriteAllText(apiPath / "index.md", sb.ToString());
        return namespaces.Count;
    }
}
