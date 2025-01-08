using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nuke.Common.IO;
using Polly;

namespace ReactiveUI.Web;

internal static class SourceFetcher
{
    private static readonly object _lockConsoleObject = new();
    private static readonly object _lockWorkloadObject = new();

    public static void GetSources(this AbsolutePath fileSystem, AbsolutePath rootDirectory, string owner, params string[] repositories)
    {
        FetchGitHubZip(fileSystem, rootDirectory, owner, repositories, "external", true, true);
    }

    private static void FetchGitHubZip(AbsolutePath fileSystem, AbsolutePath rootDirectory, string owner, string[] repositories, string outputFolder, bool fetchNuGet, bool useSrc)
    {
        var zipCache = fileSystem / "zip";
        zipCache.CreateDirectory();
        (fileSystem / outputFolder).CreateDirectory();

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

                var zipFilePath = zipCache / $"{owner}-{repository}.zip";
                zipFilePath.DeleteFile();
                var extractZipPath = zipCache / $"{owner}-{repository}-extract";
                var finalPath = fileSystem / outputFolder / repository;

                extractZipPath.DeleteDirectory();
                extractZipPath.CreateDirectory();

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
                        stream = new FileStream(zipFilePath, FileMode.OpenOrCreate))
                    {
                        await contentStream.CopyToAsync(stream);
                    }

                    LogRepositoryInfo(owner, repository, "Extracting Files");
                    ZipFile.ExtractToDirectory(zipFilePath, extractZipPath, true);

                    try
                    {
                        var zipInternalPath = extractZipPath / repository + "-main";
                        finalPath.DeleteDirectory();
                        finalPath.CreateDirectory();
                        zipInternalPath.MoveToDirectory(finalPath);
                    }
                    catch (Exception ex)
                    {
                        LogRepositoryError(owner, repository, "Failed to move: " + ex);
                    }

                    if (fetchNuGet)
                    {
                        var directory = useSrc ? finalPath / $"{repository}-main" / "src" : finalPath;
                        File.Copy(rootDirectory / "global.json", directory / "global.json", true);
                        WorkflowRestore(owner, repository, finalPath, useSrc);
                        FetchNuGet(owner, repository, finalPath, useSrc);
                    }

                    zipFilePath.DeleteDirectory();

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

    private static void FetchNuGet(string owner, string repository, AbsolutePath finalPath, bool useSrc)
    {
        LogRepositoryInfo(owner, repository, "Restoring Packages for ");

        var directory = useSrc ? finalPath / $"{repository}-main" / "src" : finalPath;
        RunDotNet(directory, $"restore {repository}.sln");
    }

    private static void WorkflowRestore(string owner, string repository, AbsolutePath finalPath, bool useSrc)
    {
        lock (_lockWorkloadObject)
        {
            LogRepositoryInfo(owner, repository, "Restoring workload for ");

            var directory = useSrc ? finalPath / $"{repository}-main" / "src" : finalPath;
            RunDotNet(directory, $"workload  restore {repository}.sln");
        }
    }

    private static void RunDotNet(AbsolutePath finalPath, string parameters)
    {
        ProcessStartInfo startInfo = new()
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "cmd.exe",
            Arguments = $"/C dotnet {parameters}",
            WorkingDirectory = finalPath.ToString()
        };
        Process process = new()
        {
            StartInfo = startInfo
        };
        process.Start();
        process.WaitForExit();
        process.Dispose();
    }

    internal static void LogInfo(string message)
    {
        lock (_lockConsoleObject)
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

    internal static void LogRepositoryError(string owner, string repository, string message)
    {
        LogError($"{message} {owner}/{repository}...");
    }

    internal static void LogError(string message)
    {
        lock (_lockConsoleObject)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR] ");
            Console.ResetColor();
            Console.Write(message);
            Console.WriteLine();
        }
    }
}
