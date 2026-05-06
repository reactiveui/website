// Copyright (c) 2019-2026 ReactiveUI Association Incorporated and contributors. All rights reserved.
// ReactiveUI Association Incorporated and contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NuStreamDocs.Autorefs;
using NuStreamDocs.Blog;
using NuStreamDocs.Building;
using NuStreamDocs.Common;
using NuStreamDocs.CSharpApiGenerator;
using NuStreamDocs.Emoji;
using NuStreamDocs.Highlight;
using NuStreamDocs.Icons.FontAwesome;
using NuStreamDocs.Icons.Material;
using NuStreamDocs.Icons.MaterialDesign;
using NuStreamDocs.Lightbox;
using NuStreamDocs.Links;
using NuStreamDocs.MagicLink;
using NuStreamDocs.MarkdownExtensions;
using NuStreamDocs.Nav;
using NuStreamDocs.Optimize;
using NuStreamDocs.Search;
using NuStreamDocs.Serve;
using NuStreamDocs.Sitemap;
using NuStreamDocs.SuperFences;
using NuStreamDocs.Theme.Material3;
using NuStreamDocs.Toc;

namespace ReactiveUI.Web;

/// <summary>ReactiveUI website builder entry point — exposes <c>build</c>, <c>serve</c>, and <c>clean</c> verbs via <see cref="System.CommandLine"/>.</summary>
internal static class Program
{
    /// <summary>Source folder containing author markdown.</summary>
    private const string DocsFolder = "docs";

    /// <summary>Subfolder under <see cref="DocsFolder"/> that holds the generated API tree.</summary>
    private const string ApiFolder = "api";

    /// <summary>Output folder the rendered site is written to.</summary>
    private const string SiteFolder = "site";

    /// <summary>Out-of-tree cache for the NuGet fetcher (lib/, refs/, cache/).</summary>
    private const string ApiCacheFolder = ".api-cache";

    /// <summary>Default port for the preview server.</summary>
    private const int DefaultPort = 8000;

    /// <summary>Sort order assigned to the API section's nav index.</summary>
    private const int ApiSectionNavOrder = 2;

    /// <summary>Process-wide logging shell.</summary>
    private static readonly WebsiteLogging _logging = new();

    /// <summary>Gets the long-form intro shown on the API reference landing page.</summary>
    private static ReadOnlySpan<byte> ApiIndexIntroduction =>
        "Browse the public surface of the ReactiveUI ecosystem — ReactiveUI itself, ReactiveUI.Validation, Akavache, Splat, "u8 +
        "Fusillade, Punchclock, Sextant, Refit, and the platform integrations. Pages are generated from the published NuGet "u8 +
        "packages on every site deploy; pick a namespace to dive in."u8;

    /// <summary>Gets the absolute repository root.</summary>
    private static DirectoryPath RootDirectory =>
        (DirectoryPath)Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));

    /// <summary>Gets the absolute docs root.</summary>
    private static DirectoryPath WebRootPath => RootDirectory / DocsFolder;

    /// <summary>Gets the absolute API tree root under <see cref="WebRootPath"/>.</summary>
    private static DirectoryPath ApiPath => WebRootPath / ApiFolder;

    /// <summary>Gets the absolute API-fetch cache directory.</summary>
    private static DirectoryPath ApiCachePath => RootDirectory / ApiCacheFolder;

    /// <summary>Gets the absolute site output directory.</summary>
    private static DirectoryPath SiteOutputPath => RootDirectory / SiteFolder;

    /// <summary>Entry point: parses verbs and dispatches to the matching async handler.</summary>
    /// <param name="args">Process arguments.</param>
    /// <returns>Process exit code.</returns>
    public static Task<int> Main(string[] args)
    {
        var portOption = new Option<int>("--port", "-p")
        {
            Description = "Port for the preview server.",
            DefaultValueFactory = _ => DefaultPort,
        };

        var build = new Command("build", "Build the full site (api walk + render + emit) into site/.");
        build.SetAction((_, ct) => RunBuildAsync(ct));

        var serve = new Command("serve", "Run the watch + serve loop for local development.")
        {
            portOption,
        };
        serve.SetAction((parseResult, ct) => RunServeAsync(parseResult.GetValue(portOption), ct));

        var clean = new Command("clean", "Wipe the API cache, generated namespace dirs, and prior site output.");
        clean.SetAction(_ => RunClean());

        var root = new RootCommand("ReactiveUI website builder.")
        {
            build,
            serve,
            clean,
        };

        return root.Parse(args).InvokeAsync();
    }

    /// <summary>Runs the full site build.</summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Process exit code.</returns>
    private static async Task<int> RunBuildAsync(CancellationToken cancellationToken)
    {
        var builder = ConfigureBuilder();
        await builder.BuildAsync(cancellationToken).ConfigureAwait(false);
        _logging.SiteBuilt(SiteOutputPath.Value);
        return 0;
    }

    /// <summary>Runs the watch + serve loop for local development.</summary>
    /// <param name="port">Port the dev server listens on.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Process exit code.</returns>
    private static async Task<int> RunServeAsync(int port, CancellationToken cancellationToken)
    {
        _logging.Serving(port);
        var builder = ConfigureBuilder();
        await builder
            .WatchAndServeAsync(
                opts => opts with { Port = port },
                _logging.For<DocBuilder>(),
                cancellationToken)
            .ConfigureAwait(false);
        return 0;
    }

    /// <summary>Wipes the API cache, generated namespace dirs, and prior site output.</summary>
    /// <returns>Process exit code.</returns>
    private static int RunClean()
    {
        if (Directory.Exists(ApiCachePath.Value))
        {
            Directory.Delete(ApiCachePath.Value, recursive: true);
        }

        if (Directory.Exists(ApiPath.Value))
        {
            foreach (var dir in Directory.EnumerateDirectories(ApiPath.Value))
            {
                Directory.Delete(dir, recursive: true);
            }
        }

        if (Directory.Exists(SiteOutputPath.Value))
        {
            Directory.Delete(SiteOutputPath.Value, recursive: true);
        }

        return 0;
    }

    /// <summary>Builds the configured <see cref="DocBuilder"/> shared between the build and serve verbs.</summary>
    /// <returns>The configured builder.</returns>
    private static DocBuilder ConfigureBuilder() =>
        new DocBuilder()
            .WithInput(WebRootPath.Value)
            .WithOutput(SiteOutputPath.Value)
            .AddExtraCss((FilePath)(WebRootPath / "stylesheets" / "extra.css").Value)
            .WithLogger(_logging.For<DocBuilder>())
            .UseDirectoryUrls()
            .UseCSharpApiGenerator(
                CSharpApiGeneratorOptions.FromManifest(RootDirectory.Value, ApiCachePath.Value) with
                {
                    IndexTitle = "ReactiveUI API Reference"u8.ToArray(),
                    IndexIntroduction = ApiIndexIntroduction.ToArray(),
                    IndexOrder = ApiSectionNavOrder,
                },
                _logging.For<CSharpApiGeneratorPlugin>())
            .UseMaterial3Theme(
                opts => opts
                    .WithSiteName("ReactiveUI"u8)
                    .WithLogo("images/logo.png"u8)
                    .WithSiteUrl("https://reactiveui.net"u8)
                    .WithCopyright("Copyright © ReactiveUI Association and Contributors"u8)
                    .WithRepoUrl("https://github.com/reactiveui/ReactiveUI"u8)
                    .WithEditUri("https://github.com/reactiveui/website/edit/main/docs/"u8)
                    .WithSectionScopedFooter(true),
                new MdiIconResolver())
            .UseFontAwesome()
            .UseMaterialIcons()
            .UseNav(static opts => (opts with { Tabs = true, Prune = true }).AddExcludes((GlobPattern)"404.md"))
            .UseAutorefs()
            .UseToc()
            .UseHighlight(HighlightOptions.Default with
            {
                AutoDetectLanguage = true,
                CopyButton = true,
                DetectionLanguages =
                [
                    [.. "csharp"u8],
                    [.. "xml"u8],
                    [.. "bash"u8],
                    [.. "powershell"u8],
                    [.. "fsharp"u8],
                    [.. "json"u8],
                ],
            })
            .UseSuperFences()
            .UseEmoji()
            .UseLightbox()
            .UseMagicLink(new MagicLinkOptions
            {
                DefaultRepo = "reactiveui/ReactiveUI"u8.ToArray(),
                ExpandUserMentions = true,
            })
            .UseCommonMarkdownExtensions()
            .UseMarkdownLinks()
            .UseSearch()
            .UseSitemap()
            .UseWyamBlog(new WyamBlogOptions((PathSegment)"Announcements", "Announcements"u8.ToArray()))
            .UseWyamBlog(new WyamBlogOptions((PathSegment)"articles", "Release Notes"u8.ToArray()));
            ////UseOptimize();
}
