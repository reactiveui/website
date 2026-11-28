// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NuStreamDocs.Autorefs;
using NuStreamDocs.Blog;
using NuStreamDocs.Building;
using NuStreamDocs.CSharpApiGenerator;
using NuStreamDocs.Common;
using NuStreamDocs.Emoji;
using NuStreamDocs.Feed;
using NuStreamDocs.GitInfo;
using NuStreamDocs.Glossary;
using NuStreamDocs.Highlight;
using NuStreamDocs.Icons.FontAwesome;
using NuStreamDocs.Icons.Material;
using NuStreamDocs.Icons.MaterialDesign;
using NuStreamDocs.Images;
using NuStreamDocs.Keys;
using NuStreamDocs.Lightbox;
using NuStreamDocs.LinkValidator;
using NuStreamDocs.Links;
using NuStreamDocs.Llms;
using NuStreamDocs.MagicLink;
using NuStreamDocs.MarkdownExtensions;
using NuStreamDocs.Mermaid;
using NuStreamDocs.Nav;
using NuStreamDocs.Search.Pagefind;
using NuStreamDocs.Serve;
using NuStreamDocs.Sitemap;
using NuStreamDocs.SmartSymbols;
using NuStreamDocs.Snippets;
using NuStreamDocs.Social;
using NuStreamDocs.SuperFences;
using NuStreamDocs.Tags;
using NuStreamDocs.Theme.Material3;
using NuStreamDocs.Toc;
using NuStreamDocs.Xrefs;

namespace ReactiveUI.Web;

/// <summary>Runs the website build, preview server, and cleanup commands.</summary>
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

    /// <summary>Folder for generated API metadata and Markdown.</summary>
    private const string IntermediateFolder = "build/_intermediate";

    /// <summary>Folder containing announcement posts.</summary>
    private const string AnnouncementsFolder = "Announcements";

    /// <summary>Default port for the preview server.</summary>
    private const int DefaultPort = 8000;

    /// <summary>Sort order assigned to the API section's nav index.</summary>
    private const int ApiSectionNavOrder = 2;

    /// <summary>Process-wide logging shell.</summary>
    private static readonly WebsiteLogging _logging = new();

    /// <summary>Gets the public website URL.</summary>
    private static ReadOnlySpan<byte> SiteUrl => "https://www.reactiveui.net"u8;

    /// <summary>Gets the long-form intro shown on the API reference landing page.</summary>
    private static ReadOnlySpan<byte> ApiIndexIntroduction =>
        "Browse the public surface of the ReactiveUI ecosystem: ReactiveUI itself, ReactiveUI.Validation, Akavache, Splat, "u8
        + "Fusillade, Punchclock, Sextant, Refit, and the platform integrations. Pages are generated from the published NuGet "u8
        + "packages on every site deploy; pick a namespace to dive in."u8;

    /// <summary>Gets the one-line site summary written under the title in <c>llms.txt</c>.</summary>
    private static ReadOnlySpan<byte> LlmsSiteSummary =>
        "ReactiveUI is a composable, cross-platform MVVM framework for .NET built on Reactive Extensions. "u8
        + "These pages cover ReactiveUI, Splat, Akavache, Refit, Fusillade, Punchclock, Sextant and the platform integrations."u8;

    /// <summary>Gets the absolute repository root.</summary>
    private static DirectoryPath RootDirectory { get; } = ResolveRoot();

    /// <summary>Gets the absolute docs root.</summary>
    private static DirectoryPath WebRootPath => RootDirectory / DocsFolder;

    /// <summary>Gets the absolute API tree root under <see cref="WebRootPath"/>.</summary>
    private static DirectoryPath ApiPath => WebRootPath / ApiFolder;

    /// <summary>Gets the absolute API-fetch cache directory.</summary>
    private static DirectoryPath ApiCachePath => RootDirectory / ApiCacheFolder;

    /// <summary>Gets the absolute site output directory.</summary>
    private static DirectoryPath SiteOutputPath => RootDirectory / SiteFolder;

    /// <summary>Gets the absolute build-intermediate directory.</summary>
    private static DirectoryPath IntermediatePath => RootDirectory / IntermediateFolder;

    /// <summary>Entry point: parses verbs and dispatches to the matching async handler.</summary>
    /// <param name="args">Process arguments.</param>
    /// <returns>Process exit code.</returns>
    internal static Task<int> Main(string[] args)
    {
        var portOption = new Option<int>("--port", "-p") { Description = "Port for the preview server.", DefaultValueFactory = static _ => DefaultPort, };

        var strictOption = new Option<bool>("--strict")
        {
            Description = "Fail the build on broken internal links and other validation diagnostics. Pass --strict false to downgrade to warnings.",
            DefaultValueFactory = static _ => true,
        };

        var build = new Command("build", "Build the full site (api walk + render + emit) into site/.") { strictOption, };
        build.SetAction((parseResult, ct) => RunBuildAsync(parseResult.GetValue(strictOption), ct));

        var serve = new Command("serve", "Run the watch + serve loop for local development.") { portOption, strictOption, };
        serve.SetAction((parseResult, ct) => RunServeAsync(parseResult.GetValue(portOption), parseResult.GetValue(strictOption), ct));

        var clean = new Command("clean", "Wipe the API cache, generated namespace dirs, and prior site output.");
        clean.SetAction(static _ => RunClean());

        var root = new RootCommand("ReactiveUI website builder.") { build, serve, clean, };

        return root.Parse(args).InvokeAsync();
    }

    /// <summary>Resolves the repository root from <c>build/Program.cs</c> by walking one directory up.</summary>
    /// <param name="programPath">Compile-time-injected absolute path of this source file; do not pass.</param>
    /// <returns>Absolute path of the repository root.</returns>
    private static DirectoryPath ResolveRoot([CallerFilePath] string programPath = "") =>
        (DirectoryPath)Path.GetFullPath("..", Path.GetDirectoryName(programPath)!);

    /// <summary>Runs the full site build.</summary>
    /// <param name="strict">When true, internal-link validation diagnostics fail the build.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Process exit code.</returns>
    private static async Task<int> RunBuildAsync(bool strict, CancellationToken cancellationToken)
    {
        var builder = ConfigureBuilder(strict);
        await builder.BuildAsync(cancellationToken).ConfigureAwait(false);
        _logging.SiteBuilt(SiteOutputPath.Value);
        return 0;
    }

    /// <summary>Runs the watch + serve loop for local development.</summary>
    /// <param name="port">Port the dev server listens on.</param>
    /// <param name="strict">When true, internal-link validation diagnostics fail each rebuild.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Process exit code.</returns>
    private static async Task<int> RunServeAsync(int port, bool strict, CancellationToken cancellationToken)
    {
        _logging.Serving(port);
        var builder = ConfigureBuilder(strict);
        await builder
            .WatchAndServeAsync(
                opts => opts with { Port = port },
                _logging.For<DocBuilder>(),
                cancellationToken)
            .ConfigureAwait(false);
        return 0;
    }

    /// <summary>Removes caches and generated output for a clean build.</summary>
    /// <returns>Process exit code.</returns>
    private static int RunClean()
    {
        DeleteDirectoryIfExists(ApiCachePath.Value);

        // Keep any authored API index while removing generated namespace folders.
        if (Directory.Exists(ApiPath.Value))
        {
            foreach (var dir in Directory.EnumerateDirectories(ApiPath.Value))
            {
                Directory.Delete(dir, recursive: true);
            }
        }

        DeleteDirectoryIfExists(IntermediatePath.Value);

        // Removing the output also clears the incremental rendering manifest.
        DeleteDirectoryIfExists(SiteOutputPath.Value);

        return 0;
    }

    /// <summary>Deletes <paramref name="path"/> and its descendants when the directory exists.</summary>
    /// <param name="path">Absolute directory path.</param>
    private static void DeleteDirectoryIfExists(string path)
    {
        if (!Directory.Exists(path))
        {
            return;
        }

        Directory.Delete(path, recursive: true);
    }

    /// <summary>Builds the configured <see cref="DocBuilder"/> shared between the build and serve verbs.</summary>
    /// <param name="strict">When true, internal-link validation diagnostics fail the build.</param>
    /// <returns>The configured builder.</returns>
    private static DocBuilder ConfigureBuilder(bool strict)
    {
        var builder = new DocBuilder()
            .WithInput(WebRootPath.Value)
            .WithOutput(SiteOutputPath.Value)
            .AddExtraCss((FilePath)(WebRootPath / "stylesheets" / "extra.css").Value)
            .WithLogger(_logging.For<DocBuilder>())
            .WithSiteName("ReactiveUI"u8)
            .WithSiteUrl(SiteUrl)
            .WithSiteAuthor("ReactiveUI and Contributors"u8)
            .UseDirectoryUrls()
            .UseCSharpApiGenerator(
                CSharpApiGeneratorOptions.FromManifest(RootDirectory.Value, ApiCachePath.Value) with
                {
                    IndexTitle = "API Reference"u8.ToArray(),
                    IndexIntroduction = ApiIndexIntroduction.ToArray(),
                    IndexOrder = ApiSectionNavOrder,
                },
                _logging.For<CSharpApiGeneratorPlugin>())
            .UseMaterial3Theme(
                static opts => opts
                    .WithSiteName("ReactiveUI"u8)
                    .WithLogo("images/logo.png"u8)
                    .WithSiteUrl(SiteUrl)
                    .WithCopyright("Copyright © ReactiveUI and Contributors"u8)
                    .WithRepoUrl("https://github.com/reactiveui/ReactiveUI"u8)
                    .WithEditUri("https://github.com/reactiveui/website/edit/main/docs/"u8)
                    .WithSectionScopedFooter(true),
                new MdiIconResolver());

        return ConfigureContent(builder, strict);
    }

    /// <summary>Configures content plugins, validation, and feeds.</summary>
    /// <param name="builder">The builder to configure.</param>
    /// <param name="strict">Whether broken internal links produce errors.</param>
    /// <returns>The configured builder.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static DocBuilder ConfigureContent(DocBuilder builder, bool strict) =>
        builder.UseFontAwesome()
            .UseMaterialIcons()
            .UseNav(static opts => (opts with { Tabs = true, Prune = true }).AddExcludes((GlobPattern)"404.md"))
            .UseAutorefs()
            .UseXrefs()
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
            .UseMermaid()
            .UseEmoji()
            .UseLightbox()
            .UseImages()
            .UseGlossary()
            .UseKeys()
            .UseSmartSymbols()
            .UseSnippets()
            .UseTags()
            .UseMagicLink(new MagicLinkOptions { DefaultRepo = "reactiveui/ReactiveUI"u8.ToArray(), ExpandUserMentions = true, })
            .UseCommonMarkdownExtensions()
            .UseMarkdownLinks()
            .UsePagefindSearch(static opts =>

                // API search fragments would exceed Cloudflare Pages' 20,000-file deployment limit.
                opts.WithSectionPriorities("documentation/:80,articles/:40,Announcements/:40,api/:-200"u8)
                    .AddExcludePathPrefixes("api/"u8))
            .UseSitemap()
            .UseGitInfo()

            // One SVG card per page would push the API-heavy site past Cloudflare Pages' 20,000-file limit.
            .UseSocial(static opts => opts.WithoutSvgCards())

            // llms-full.txt would bundle every API page into one file at Cloudflare Pages' 25 MiB per-file limit.
            // The link validator checks the llms.txt head link before the plugin writes the file, so it is not emitted.
            .UseLlms(static opts => opts
                .WithSiteSummary(LlmsSiteSummary)
                .WithEmitFullText(false)
                .WithEmitHeadLink(false))
            .UseLinkValidator(LinkValidatorOptions.Default with { StrictInternal = strict }, _logging.For<LinkValidatorPlugin>())
            .UseWyamBlog(new((PathSegment)AnnouncementsFolder, "Announcements"u8.ToArray()))
            .UseWyamBlog(new((PathSegment)"articles", "Articles"u8.ToArray()))
            .UseFeed(new(
                [.. SiteUrl],
                [.. "ReactiveUI Announcements"u8],
                [.. "Latest announcements from the ReactiveUI team."u8],
                (PathSegment)AnnouncementsFolder))
            .UseFeed(new(
                [.. SiteUrl],
                [.. "ReactiveUI Articles"u8],
                [.. "Articles about ReactiveUI and the ecosystem."u8],
                (PathSegment)"articles"));
}
