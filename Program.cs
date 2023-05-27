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
            "../../splat/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.TestRunner,!Benchmarks,}/**/*.cs",
            "../../reactiveui/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Test,!*.Uwp,!*.Templates,!Benchmarks,!*.TestRunner,!*.LeakTests,}/**/*.cs",
            "../../akavache/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../punchclock/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../fusillade/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs")
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
