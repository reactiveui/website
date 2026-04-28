#nullable enable

using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SourceDocParser.Zensical.Navigation;

namespace ReactiveUI.Web;

/// <summary>
/// Renders an explicit YAML <c>nav:</c> block by walking the docs tree
/// for hand-authored content and splicing the API tree from the captured
/// <see cref="NavigationGraph"/>. Top-level entries follow a fixed order
/// so tabs read left-to-right the same way every build; everything else
/// falls back to ordinal sort, which keeps generated configs diff-stable.
/// </summary>
internal static class NavBuilder
{
    private static readonly string[] _topLevelOrder =
    {
        "index.md",
        "docs",
        "api",
        "contribute",
        "Announcements",
        "Book.md",
    };

    private static readonly HashSet<string> _topLevelExcludes = new(StringComparer.OrdinalIgnoreCase)
    {
        "Slack.md",
        "Extensions.md",
        "articles",
        "vs",
        "images",
        "stylesheets",
        "_redirects",
        ".pages",
    };

    public static string Build(string docsRoot, NavigationGraph? api)
    {
        var sb = new StringBuilder();
        sb.AppendLine("nav:");

        var ordered = OrderTopLevel(docsRoot);
        foreach (var entry in ordered)
        {
            if (entry.IsApiPlaceholder)
            {
                EmitApi(sb, docsRoot, api);
            }
            else if (entry.IsDirectory)
            {
                EmitDirectory(sb, entry.Path, docsRoot, indent: 1);
            }
            else
            {
                EmitFile(sb, entry.Path, docsRoot, indent: 1);
            }
        }

        return sb.ToString();
    }

    private readonly record struct Entry(string Path, bool IsDirectory, bool IsApiPlaceholder);

    private static List<Entry> OrderTopLevel(string docsRoot)
    {
        var files = Directory.GetFiles(docsRoot, "*.md", SearchOption.TopDirectoryOnly)
            .Select(f => Path.GetFileName(f)!)
            .Where(name => !_topLevelExcludes.Contains(name))
            .ToList();
        var dirs = Directory.GetDirectories(docsRoot)
            .Select(d => Path.GetFileName(d)!)
            .Where(name => !_topLevelExcludes.Contains(name))
            .ToList();

        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var ordered = new List<Entry>();
        foreach (var name in _topLevelOrder)
        {
            if (string.Equals(name, "api", StringComparison.OrdinalIgnoreCase))
            {
                ordered.Add(new(Path.Combine(docsRoot, "api"), IsDirectory: true, IsApiPlaceholder: true));
                seen.Add(name);
                continue;
            }

            if (files.Any(f => string.Equals(f, name, StringComparison.OrdinalIgnoreCase)))
            {
                ordered.Add(new(Path.Combine(docsRoot, name), IsDirectory: false, IsApiPlaceholder: false));
                seen.Add(name);
            }
            else if (dirs.Any(d => string.Equals(d, name, StringComparison.OrdinalIgnoreCase)))
            {
                ordered.Add(new(Path.Combine(docsRoot, name), IsDirectory: true, IsApiPlaceholder: false));
                seen.Add(name);
            }
        }

        foreach (var f in files.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
        {
            if (!seen.Contains(f))
            {
                ordered.Add(new(Path.Combine(docsRoot, f), IsDirectory: false, IsApiPlaceholder: false));
            }
        }

        foreach (var d in dirs.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
        {
            if (!seen.Contains(d))
            {
                ordered.Add(new(Path.Combine(docsRoot, d), IsDirectory: true, IsApiPlaceholder: false));
            }
        }

        return ordered;
    }

    private static void EmitFile(StringBuilder sb, string filePath, string docsRoot, int indent)
    {
        var rel = ToPosix(Path.GetRelativePath(docsRoot, filePath));
        sb.Append(' ', indent * 2).Append("- ").Append(rel).AppendLine();
    }

    private static void EmitDirectory(StringBuilder sb, string dirPath, string docsRoot, int indent)
    {
        // Render the section into a scratch buffer first; if the
        // directory has no markdown content (only assets, excluded
        // subdirs, etc.) we drop the entire entry. Zensical rejects
        // a section header with no children as a YAML null.
        var name = Path.GetFileName(dirPath)!;
        var title = TitleFor(name);
        var body = new StringBuilder();

        var indexPath = Path.Combine(dirPath, "index.md");
        if (File.Exists(indexPath))
        {
            EmitFile(body, indexPath, docsRoot, indent + 1);
        }

        foreach (var child in EnumerateChildren(dirPath))
        {
            if (Directory.Exists(child))
            {
                EmitDirectory(body, child, docsRoot, indent + 1);
            }
            else
            {
                if (string.Equals(Path.GetFileName(child), "index.md", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                EmitFile(body, child, docsRoot, indent + 1);
            }
        }

        if (body.Length == 0)
        {
            return;
        }

        sb.Append(' ', indent * 2).Append("- ").Append(EscapeYamlKey(title)).AppendLine(":");
        sb.Append(body);
    }

    private static IEnumerable<string> EnumerateChildren(string dirPath)
    {
        var dirs = Directory.GetDirectories(dirPath)
            .Where(d => !_topLevelExcludes.Contains(Path.GetFileName(d)!))
            .OrderBy(d => Path.GetFileName(d), StringComparer.OrdinalIgnoreCase);
        var files = Directory.GetFiles(dirPath, "*.md", SearchOption.TopDirectoryOnly)
            .OrderBy(f => Path.GetFileName(f), StringComparer.OrdinalIgnoreCase);
        foreach (var f in files)
        {
            yield return f;
        }
        foreach (var d in dirs)
        {
            yield return d;
        }
    }

    private static void EmitApi(StringBuilder sb, string docsRoot, NavigationGraph? graph)
    {
        sb.Append("  - API:").AppendLine();
        var apiIndex = Path.Combine(docsRoot, "api", "index.md");
        if (File.Exists(apiIndex))
        {
            sb.Append("    - api/index.md").AppendLine();
        }

        if (graph is not { Packages: { Length: > 0 } packages })
        {
            return;
        }

        foreach (var package in packages)
        {
            sb.Append("    - ").Append(EscapeYamlKey(package.Name)).AppendLine(":");

            // Splice the section landing page as the first entry --
            // with navigation.indexes enabled in mkdocs.base.yml that
            // turns the section header itself into a clickable link
            // to the package landing, so deep-linked readers can
            // navigate up.
            EmitLanding(sb, package.LandingPagePath, indent: "      ");

            foreach (var ns in package.Namespaces)
            {
                sb.Append("      - ").Append(EscapeYamlKey(ns.Name)).AppendLine(":");
                EmitLanding(sb, ns.LandingPagePath, indent: "        ");

                foreach (var type in ns.Types)
                {
                    var path = $"api/{type.Path}";
                    var title = FormatTypeTitle(type);
                    sb.Append("        - ")
                        .Append(EscapeYamlKey(title))
                        .Append(": ")
                        .Append(path)
                        .AppendLine();
                }
            }
        }
    }

    /// <summary>
    /// Emits a leading <c>- api/.../index.md</c> entry inside a
    /// section. <paramref name="landingPath"/> is the
    /// <c>NavigationPackage.LandingPagePath</c> /
    /// <c>NavigationNamespace.LandingPagePath</c> field -- null
    /// when no landing page was emitted (e.g. routing rule stripped
    /// the section), in which case we just skip.
    /// </summary>
    private static void EmitLanding(StringBuilder sb, string? landingPath, string indent)
    {
        if (landingPath is null)
        {
            return;
        }

        sb.Append(indent).Append("- api/").Append(landingPath).AppendLine();
    }

    /// <summary>
    /// Renders an MS-Learn-style sidebar title: real generic parameter
    /// names when the metadata exposed them (<c>Change&lt;TKey, TValue&gt;</c>)
    /// and a kind suffix (<c>Class</c> / <c>Struct</c> / <c>Delegate</c>) so
    /// generic-arity siblings don't look like duplicate entries.
    /// </summary>
    private static string FormatTypeTitle(NavigationEntry entry)
    {
        var baseName = NameWithGenericParameters(entry);
        var suffix = KindSuffix(entry.Kind);
        return suffix is null ? baseName : baseName + " " + suffix;
    }

    private static string NameWithGenericParameters(NavigationEntry entry)
    {
        if (entry.Arity == 0
            || entry.TypeParameters is not { Length: > 0 } parameters
            || parameters.Length != entry.Arity)
        {
            return entry.Title;
        }

        var bareNameLength = entry.Title.IndexOf('<', StringComparison.Ordinal);
        var bareName = bareNameLength < 0 ? entry.Title : entry.Title[..bareNameLength];
        return bareName + "<" + string.Join(", ", parameters) + ">";
    }

    private static string? KindSuffix(NavigationTypeKind kind) => kind switch
    {
        NavigationTypeKind.Class => "Class",
        NavigationTypeKind.Struct => "Struct",
        NavigationTypeKind.Interface => "Interface",
        NavigationTypeKind.Record => "Record",
        NavigationTypeKind.RecordStruct => "Record Struct",
        NavigationTypeKind.Enum => "Enum",
        NavigationTypeKind.Delegate => "Delegate",
        NavigationTypeKind.Union => "Union",
        _ => null,
    };

    private static string ToPosix(string path) => path.Replace('\\', '/');

    private static string TitleFor(string name)
    {
        var stripped = Path.GetFileNameWithoutExtension(name);
        var spaced = stripped.Replace('-', ' ').Replace('_', ' ');
        if (spaced.Length == 0)
        {
            return stripped;
        }
        return char.ToUpperInvariant(spaced[0]) + spaced[1..];
    }

    private static readonly SearchValues<char> _yamlSpecials =
        SearchValues.Create(":#&*!|>'\"%@`");

    private static string EscapeYamlKey(string value)
    {
        if (value.AsSpan().IndexOfAny(_yamlSpecials) >= 0
            || value.Contains(" - ", StringComparison.Ordinal))
        {
            return "\"" + value.Replace("\"", "\\\"", StringComparison.Ordinal) + "\"";
        }
        return value;
    }
}
