using Statiq.Markdown;
using System.IO.Compression;

Directory.CreateDirectory("external/Zip");
await GetSource("reactiveui"); 
await GetSource("akavache");
await GetSource("fusillade");
await GetSource("punchclock");
await GetSource("splat");
await GetSource("DynamicData", "reactivemarbles");

await Bootstrapper
        .Factory
        .CreateDocs(args)
        .AddSetting(MarkdownKeys.MarkdownExtensions, "bootstrap")
        .AddSourceFiles(
            "../../dynamicdata/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            ////"../../splat/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../reactiveui/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,!*.TestRunner,!*.LeakTests,}/**/*.cs",
            "../../akavache/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../punchclock/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../fusillade/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs")
        .AddProjectFiles(
            "../../splat/src/Splat/Splat.csproj",
            //// TODO FIND ISSUE WITH LOADING THIS"../../splat/src/Splat.Drawing/Splat.Drawing.csproj",
            "../../splat/src/Splat.ApplicationInsights/Splat.ApplicationInsights.csproj",
            "../../splat/src/Splat.Autofac/Splat.Autofac.csproj",
            "../../splat/src/Splat.Exceptionless/Splat.Exceptionless.csproj",
            "../../splat/src/Splat.Log4Net/Splat.Log4Net.csproj",
            "../../splat/src/Splat.Microsoft.Extensions.DependencyInjection/Splat.Microsoft.Extensions.DependencyInjection.csproj",
            "../../splat/src/Splat.Microsoft.Extensions.Logging/Splat.Microsoft.Extensions.Logging.csproj",
            "../../splat/src/Splat.Ninject/Splat.Ninject.csproj",
            "../../splat/src/Splat.NLog/Splat.NLog.csproj",
            "../../splat/src/Splat.Prism/Splat.Prism.csproj",
            "../../splat/src/Splat.Raygun/Splat.Raygun.csproj",
            "../../splat/src/Splat.Serilog/Splat.Serilog.csproj",
            "../../splat/src/Splat.SimpleInjector/Splat.SimpleInjector.csproj")
        .RunAsync();

static async Task GetSource(string repository, string owner = "reactiveui")
{
    var url = $"https://codeload.github.com/{owner}/{repository}/zip/main";
    var zipPath = $"external/Zip/{repository}.zip";
    File.Delete(zipPath);

    using (HttpClient client = new HttpClient())
    {
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
            stream = new FileStream(zipPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await contentStream.CopyToAsync(stream);
        }
    }

    var extractPath = $"external/Zip/{repository}/";
    var finalPath = $"external/{repository}";
    ZipFile.ExtractToDirectory(zipPath, extractPath);
    if (Directory.Exists(finalPath))
    {
        Directory.Delete(finalPath, true);
    }

    Directory.Move($"{extractPath}{repository}-main", finalPath);
    if (Directory.Exists(extractPath))
    {
        Directory.Delete(extractPath, true);
    }

    File.Delete(zipPath);
}
