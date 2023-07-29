using System.Diagnostics;
using System.IO.Compression;

using Polly;

namespace ReactiveUI.Web;

internal static class SourceFetcher
{
    private static readonly object _lockWorkloadObject = new();

    public static void GetSources(this AbsolutePath path, string owner, params string[] repositories)
    {
        FetchGitHubZip(path, owner, repositories, "external", true);
    }


    public static void FetchTheme(this AbsolutePath path, string[] args, string owner = "glennawatson", string repository = "Docable5")
    {
        var isProduction = !args.Any(x => x.Contains("preview"));
        if (isProduction)
        {
            Log.Information($"Skipping Theme Fetching");
            return;
        }

        Log.Information($"Fetching Theme");
        FetchGitHubZip(path, owner, new[] { repository }, "theme", false);
    }

    private static void FetchGitHubZip(AbsolutePath path, string owner, string[] repositories, AbsolutePath outputFolder, bool includeRepositoryInFinal)
    {
        var zipCache = path / "external" / "zip";
        zipCache.CreateOrCleanDirectory();
        if (includeRepositoryInFinal)
        {
            outputFolder.CreateDirectory();
        }

        using var client = new HttpClient();

        // Create the semaphore.
        using var semaphore = new SemaphoreSlim(3, 3);

        var waitAndRetry = Policy.Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                6, // We can also do this with WaitAndRetryForever... but chose WaitAndRetry this time.
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt))); // Back off!  2, 4, 8, 16 etc times 1/4-second

        Task.WaitAll(repositories.Select(repository => Task.Run(async () =>
        {
            await semaphore.WaitAsync();
            try
            {
                LogRepositoryInfo(owner, repository, "Downloading");

                var url = $"https://codeload.github.com/{owner}/{repository}/zip/main";

                var zipFilePath = zipCache.GetFile($"{owner}-{repository}.zip");
                zipFilePath.DeleteFile();
                var extractZipPath = zipCache.GetDirectory($"{owner}-{repository}-extract");
                var finalPath = !includeRepositoryInFinal ? outputFolder : outputFolder / repository;

                extractZipPath.CreateOrCleanDirectory();

                // Retry the following call according to the policy
                await waitAndRetry.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        LogRepositoryInfo(owner, repository, "Could not find a valid document at: " + url + " " + response.StatusCode);
                        throw new HttpRequestException("Could not find a valid document at: " + url, default, response.StatusCode);
                    }

                    using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                        stream = zipFilePath.ToFileInfo().OpenRead())
                    {
                        await contentStream.CopyToAsync(stream);
                    }

                    LogRepositoryInfo(owner, repository, "Extracting Files");
                    zipFilePath.UncompressTo(extractZipPath);

                    try
                    {
                        var zipInternalPath = extractZipPath.GetDirectory(repository + "-main");
                        finalPath.CreateOrCleanDirectory();
                        zipInternalPath.MoveToDirectory(finalPath);
                    }
                    catch (Exception ex)
                    {
                        LogRepositoryError(owner, repository, "Failed to move: " + ex);
                    }

                    zipFilePath.DeleteFile();

                    LogRepositoryInfo(owner, repository, "Downloaded");
                });
            }
            catch (Exception ex)
            {
                LogRepositoryError(owner, repository, "Failed to download: " + ex);
            }
            finally
            {
                semaphore.Release();
            }
        })).ToArray());
    }


    private static void LogRepositoryInfo(string owner, string repository, string message) =>
        Log.Information($"{message} {owner}/{repository}");

    private static void LogRepositoryError(string owner, string repository, string message) => Log.Error($"{message} {owner}/{repository}");

    private static AbsolutePath GetFile(this AbsolutePath path, string name) => path.GetFiles().FirstOrDefault(path => path.Name.Equals(name));

    private static AbsolutePath GetDirectory(this AbsolutePath path, string name) => path.GetDirectories().FirstOrDefault(path => path.Name.Equals(name));
}
