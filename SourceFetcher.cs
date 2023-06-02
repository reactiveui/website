using System.Diagnostics;
using System.IO.Compression;

using Polly;
using Polly.RateLimit;

namespace ReactiveUI.Web;

internal static class SourceFetcher
{
    private static readonly object _lockObject = new();

    public static Bootstrapper GetSources(this Bootstrapper bootstrapper, string owner, params string[] repositories)
    {
        FetchGitHubZip(bootstrapper.FileSystem, owner, repositories, "external", true, true);

        return bootstrapper;
    }

    public static string WithSourceFilter(this string repository, params string[] exclude)
    {
        var excludeFilter = exclude.Length > 0 ? string.Join(",", exclude.Select(x => $"!{x}")) + "," : string.Empty;
        return $"../../{repository}/src/**/{{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,{excludeFilter}}}/**/*.cs";
    }

    public static Bootstrapper ConfigureLinks(this Bootstrapper bootstrapper, string[] args)
    {
        var isProduction = args.Any(x => x.Contains("preview")) ? "false" : "true";
        LogInfo($"Is Production Build: {isProduction}");
        return bootstrapper.AddSetting(WebKeys.MakeLinksAbsolute, isProduction);
    }

    public static Bootstrapper FetchTheme(this Bootstrapper bootstrapper, string owner = "glennawatson", string repository = "Docable5")
    {
        LogInfo($"Fetching Theme");
        FetchGitHubZip(bootstrapper.FileSystem, owner, new[] { repository }, "theme", false, false);
        return bootstrapper;
    }

    private static void FetchGitHubZip(IFileSystem fileSystem, string owner, string[] repositories, string outputFolder, bool fetchNuGet, bool includeRepositoryInFinal)
    {
        var zipCache = fileSystem.GetCacheDirectory("external/zip");
        zipCache.Create();

        using var client = new HttpClient();

        var waitAndRetry = Policy.Handle<Exception>()
            .WaitAndRetryAsync(
                6, // We can also do this with WaitAndRetryForever... but chose WaitAndRetry this time.
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2,
                                                    attempt))); // Back off!  2, 4, 8, 16 etc times 1/4-second

        var rateLimitTime = TimeSpan.FromSeconds(1);

        // Allow up to 3 executions per second.
        var rateLimit = Policy.RateLimitAsync(3, rateLimitTime);

        var policy = Policy.Handle<RateLimitRejectedException>()
            .WaitAndRetryAsync(5, _ => rateLimitTime)
            .WrapAsync(rateLimit);

        Task.WaitAll(repositories.Select(repository => Task.Run(async () =>
        {
            try
            {
                LogRepositoryInfo(owner, repository, "Downloading");

                var url = $"https://codeload.github.com/{owner}/{repository}/zip/main";

                var zipFilePath = zipCache.GetFile($"{owner}-{repository}.zip");
                zipFilePath.DeleteSafe();
                var extractZipPath = zipCache.GetDirectory($"{owner}-{repository}-extract");
                var finalPath = !includeRepositoryInFinal ? fileSystem.GetRootDirectory(outputFolder) : fileSystem.GetRootDirectory(Path.Combine(outputFolder, repository));

                extractZipPath.Recreate();

                // Retry the following call according to the policy
                await policy.ExecuteAsync(() => waitAndRetry.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        LogRepositoryInfo(owner, repository, "Could not find a valid document at: " + url + " " + response.StatusCode);
                        throw new HttpRequestException("Could not find a valid document at: " + url, default, response.StatusCode);
                    }

                    using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                        stream = zipFilePath.OpenWrite())
                    {
                        await contentStream.CopyToAsync(stream);
                    }

                    LogRepositoryInfo(owner, repository, "Extracting Files");

                    ZipFile.ExtractToDirectory(zipFilePath.Path.FullPath, extractZipPath.Path.FullPath);

                    finalPath.DeleteSafe(true);

                    var zipInternalPath = extractZipPath.GetDirectory(repository + "-main");

                    zipInternalPath.MoveTo(finalPath);

                    if (fetchNuGet)
                    {
                        FetchNuGet(owner, repository, finalPath);
                    }

                    zipFilePath.DeleteSafe();

                    LogRepositoryInfo(owner, repository, "Downloaded");
                }));
            }
            catch (Exception ex)
            {
                LogRepositoryError(owner, repository, "Failed to download: " + ex);
            }
        })).ToArray());
    }

    private static void DeleteSafe(this IFile? file)
    {
        if (file?.Exists == true)
        {
            file.Delete();
        }
    }

    private static void DeleteSafe(this IDirectory? directory, bool recursive)
    {
        if (directory?.Exists == true)
        {
            directory.Delete(recursive);
        }
    }

    private static void Recreate(this IDirectory? directory)
    {
        directory.DeleteSafe(true);
        directory?.Create();
    }

    private static void FetchNuGet(string owner, string repository, IDirectory finalPath)
    {
        LogRepositoryInfo(owner, repository, "Restoring Packages for ");

        ProcessStartInfo startInfo = new()
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "cmd.exe",
            Arguments = $"/C dotnet restore {repository}.sln",
            WorkingDirectory = Path.Combine(finalPath.Path.FullPath, "src")
        };
        Process process = new()
        {
            StartInfo = startInfo
        };
        process.Start();
        process.WaitForExit();
        process.Dispose();
    }

    private static void LogInfo(string message)
    {
        lock (_lockObject)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[INFO] ");
            Console.ResetColor();
            Console.Write($"{message}");
            Console.WriteLine();
        }
    }

    private static void LogRepositoryInfo(string owner, string repository, string message) =>
        LogInfo($"{message} {owner}/{repository}...");

    private static void LogRepositoryError(string owner, string repository, string message)
    {
        lock (_lockObject)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR] ");
            Console.ResetColor();
            Console.Write($"{message} {owner}/{repository}...");
            Console.WriteLine();
        }
    }
}
