#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Serilog.Extensions.Logging;
using SourceDocParser;
using SourceDocParser.NuGet;
using SourceDocParser.SourceLink;
using SourceDocParser.Zensical;

namespace ReactiveUI.Web;

/// <summary>
/// Nuke build entry point. Drives the documentation pipeline: fetches
/// NuGet packages, walks their public API surface into mkdocs Material
/// markdown via the SourceDocParser.Zensical emitter, then shells out
/// to mkdocs in a project-local venv to render the final site.
/// </summary>
internal sealed class Build : NukeBuild
{
    private const string DocsFolder = "docs";
    private const string ApiFolder = "api";
    private const string SiteFolder = "site";
    private const string VenvFolder = ".venv";
    private const string RequirementsFile = "requirements.txt";
    private const int DefaultPort = 8000;

    [Parameter("Port for the preview server (default: 8000)")]
    private readonly int Port = DefaultPort;

    [Parameter("Treat broken source links as a build failure (mkdocs-style strict mode)")]
    private readonly bool FailOnBrokenSourceLinks;

    [Parameter("Pass --strict to mkdocs (CI gate); fails on any broken link or unresolved cross-ref")]
    private readonly bool Strict;

    private static readonly SerilogLoggerFactory _loggerFactory = new(Serilog.Log.Logger, dispose: false);

    private static ExtractionResult? _lastExtractionResult;

    public static int Main() => Execute<Build>(x => x.BuildWebsite);

    private static AbsolutePath WebRootPath => RootDirectory / DocsFolder;

    private static AbsolutePath ApiPath => WebRootPath / ApiFolder;

    private static AbsolutePath ApiLibDirectory => ApiPath / "lib";

    private static AbsolutePath ApiRefsDirectory => ApiPath / "refs";

    private static AbsolutePath SiteOutputPath => RootDirectory / SiteFolder;

    private static AbsolutePath VenvPath => RootDirectory / VenvFolder;

    private static AbsolutePath MkdocsExecutable => OperatingSystem.IsWindows()
        ? VenvPath / "Scripts" / "mkdocs.exe"
        : VenvPath / "bin" / "mkdocs";

    private static AbsolutePath VenvPipExecutable => OperatingSystem.IsWindows()
        ? VenvPath / "Scripts" / "pip.exe"
        : VenvPath / "bin" / "pip";

    private Target Clean => _ => _
        .Before(ExtractMetadata)
        .Executes(() =>
        {
            ApiLibDirectory.DeleteDirectory();
            ApiRefsDirectory.DeleteDirectory();
            // Delete only the per-namespace dirs the Zensical emitter
            // creates; preserve the hand-curated docs/api/index.md and
            // any sibling .pages file at the api/ root.
            foreach (var dir in ApiPath.GlobDirectories("*"))
            {
                dir.DeleteDirectory();
            }
            SiteOutputPath.DeleteDirectory();
        });

    /// <summary>
    /// Walks each NuGet package's public API into mkdocs Material markdown.
    /// NuGetAssemblySource handles the fetch + extract internally and is
    /// idempotent against the package cache, so there's no separate fetch
    /// target — invoking ExtractMetadata gets you both. Re-emit to docs/api/
    /// directly so mkdocs picks the tree up without a copy step. Also
    /// regenerates docs/api/index.md from the discovered namespace dirs
    /// so the API landing page stays in sync with whatever the emitter
    /// actually produced.
    /// </summary>
    private Target ExtractMetadata => _ => _
        .Executes(async () =>
        {
            var source = new NuGetAssemblySource(RootDirectory, ApiPath, _loggerFactory.CreateLogger(nameof(NuGetAssemblySource)));
            var emitter = new ZensicalDocumentationEmitter();
            _lastExtractionResult = await new MetadataExtractor().RunAsync(source, ApiPath, emitter, _loggerFactory.CreateLogger(nameof(MetadataExtractor)));
            WriteApiIndex();
        });

    private static void WriteApiIndex()
    {
        // Skip the well-known scaffold dirs Zensical / NuGetFetcher create
        // alongside the per-namespace API trees.
        var skip = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "lib", "refs", "cache", "_global" };
        var namespaces = ApiPath.GlobDirectories("*")
            .Select(p => p.Name)
            .Where(name => !skip.Contains(name))
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

        File.WriteAllText(ApiPath / "index.md", sb.ToString());
        Serilog.Log.Information("Wrote API index for {Count} namespace(s)", namespaces.Count);
    }

    private Target ValidateSourceLinks => _ => _
        .DependsOn(ExtractMetadata)
        .Executes(async () =>
        {
            if (_lastExtractionResult is not { SourceLinks: { } links })
            {
                Serilog.Log.Information("No extraction result available for validation.");
                return;
            }

            var broken = await new SourceLinkValidator().ValidateAsync(links, FailOnBrokenSourceLinks, _loggerFactory.CreateLogger(nameof(SourceLinkValidator)));
            if (broken == 0)
            {
                Serilog.Log.Information("Source link validation passed.");
            }
        });

    /// <summary>
    /// Bootstraps the project-local Python venv if missing, then makes
    /// sure requirements.txt is installed. Idempotent and cheap once the
    /// venv exists. Cross-platform: picks the right interpreter / script
    /// dir for Windows vs Linux/macOS.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance",
        "CA1822:Mark members as static",
        Justification = "Nuke Target properties are bound to the build instance by convention and must remain instance members.")]
    private Target EnsurePythonEnv => _ => _
        .Executes(() =>
        {
            var freshlyCreated = false;
            if (!File.Exists(MkdocsExecutable))
            {
                Serilog.Log.Information("Bootstrapping Python venv at {VenvPath}", VenvPath);
                var python = OperatingSystem.IsWindows() ? "python" : "python3";
                ProcessTasks.StartProcess(python, $"-m venv \"{VenvPath}\"", workingDirectory: RootDirectory)
                    .AssertZeroExitCode();
                freshlyCreated = true;
            }

            // Only pip-install when bootstrapping the venv or when requirements.txt
            // is newer than the last install. Avoids a network round trip on every
            // local build.
            var requirements = RootDirectory / RequirementsFile;
            var stamp = VenvPath / ".requirements.stamp";
            if (freshlyCreated || !File.Exists(stamp) || File.GetLastWriteTimeUtc(requirements) > File.GetLastWriteTimeUtc(stamp))
            {
                ProcessTasks.StartProcess(VenvPipExecutable, $"install --quiet --upgrade --requirement \"{requirements}\"", workingDirectory: RootDirectory)
                    .AssertZeroExitCode();
                File.WriteAllText(stamp, DateTime.UtcNow.ToString("o"));
            }
        });

    private Target BuildWebsite => _ => _
        .DependsOn(ExtractMetadata)
        .DependsOn(EnsurePythonEnv)
        .Produces(SiteOutputPath)
        .Executes(() =>
        {
            var strictFlag = Strict ? "--strict " : string.Empty;
            ProcessTasks.StartProcess(MkdocsExecutable, $"build {strictFlag}--site-dir \"{SiteOutputPath}\"", workingDirectory: RootDirectory)
                .AssertZeroExitCode();
            Serilog.Log.Information("Site built at {SiteOutputPath}", SiteOutputPath);
        });

    private Target Serve => _ => _
        .DependsOn(EnsurePythonEnv)
        .Executes(() =>
        {
            Serilog.Log.Information("Serving site at http://127.0.0.1:{Port}", Port);
            ProcessTasks.StartProcess(MkdocsExecutable, $"serve --dev-addr 127.0.0.1:{Port}", workingDirectory: RootDirectory)
                .AssertZeroExitCode();
        });
}
