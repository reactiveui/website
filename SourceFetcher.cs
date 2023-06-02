using System.Diagnostics;
using System.IO.Compression;

using Polly;

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

    public static Bootstrapper ConfigureLinks(this Bootstrapper bootstrapper)
    {
        var isProduction = Environment.GetCommandLineArgs().Any(x => x.Contains("preview")) ? "false" : "true";
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

        // Create the semaphore.
        using var semaphore = new SemaphoreSlim(3, 3);

        var waitAndRetry = Policy.Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                6, // We can also do this with WaitAndRetryForever... but chose WaitAndRetry this time.
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2,
                                                    attempt))); // Back off!  2, 4, 8, 16 etc times 1/4-second

        Task.WaitAll(repositories.Select(repository => Task.Run(async () =>
        {
            await semaphore.WaitAsync();
            try
            {
                LogToConsole(owner, repository, "Downloading");

                var url = $"https://codeload.github.com/{owner}/{repository}/zip/main";

                var zipFilePath = zipCache.GetFile($"{owner}-{repository}.zip");
                zipFilePath.Delete();
                var extractZipPath = zipCache.GetDirectory($"{owner}-{repository}-extract");
                var finalPath = !includeRepositoryInFinal ? fileSystem.GetRootDirectory(outputFolder) : fileSystem.GetRootDirectory(Path.Combine(outputFolder, repository));

                extractZipPath.Recreate();

                // Retry the following call according to the policy
                await waitAndRetry.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        LogToConsole(owner, repository, "Could not find a valid document at: " + url + " " + response.StatusCode);
                        throw new HttpRequestException("Could not find a valid document at: " + url, default, response.StatusCode);
                    }

                    using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                        stream = zipFilePath.OpenWrite())
                    {
                        await contentStream.CopyToAsync(stream);
                    }

                    LogToConsole(owner, repository, "Extracting Files");

                    ZipFile.ExtractToDirectory(zipFilePath.Path.FullPath, extractZipPath.Path.FullPath);

                    finalPath.DeleteSafe(true);

                    var zipInternalPath = extractZipPath.GetDirectory(repository + "-main");

                    zipInternalPath.MoveTo(finalPath);

                    if (fetchNuGet)
                    {
                        FetchNuGet(owner, repository, finalPath);
                    }

                    zipFilePath.Delete();

                    LogToConsole(owner, repository, "Downloaded");
                });
            }
            catch (Exception ex)
            {
                LogErrorToConsole(owner, repository, "Failed to download: " + ex);
            }
            finally
            {
                semaphore.Release();
            }
        })).ToArray());
    }

    private static void DeleteSafe(this IDirectory directory, bool recursive)
    {
        if (directory.Exists)
        {
            directory.Delete(recursive);
        }
    }

    private static void Recreate(this IDirectory directory)
    {
        directory.DeleteSafe(true);
        directory.Create();
    }

    private static void FetchNuGet(string owner, string repository, IDirectory finalPath)
    {
        LogToConsole(owner, repository, "Restoring Packages for ");

        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "cmd.exe",
            Arguments = $"/C dotnet restore {repository}.sln",
            WorkingDirectory = Path.Combine(finalPath.Path.FullPath, "src")
        };
        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit();
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

    private static void LogToConsole(string owner, string repository, string message)
    {
        lock (_lockObject)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[INFO] ");
            Console.ResetColor();
            Console.Write($"{message} {owner}/{repository}...");
            Console.WriteLine();
        }
    }

    private static void LogErrorToConsole(string owner, string repository, string message)
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
