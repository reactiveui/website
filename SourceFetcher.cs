using System.IO.Compression;

using Polly.RateLimit;
using Polly;
using System.Diagnostics;

namespace ReactiveUI.Web;

internal static class SourceFetcher
{
    private static readonly object _lockObject = new();

    public static Bootstrapper GetSources(this Bootstrapper bootStrapper, string owner, params string[] repositories)
    {
        Directory.CreateDirectory("external/Zip");

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
            LogToConsole(repository, "Downloading");

            var url = $"https://codeload.github.com/{owner}/{repository}/zip/main";

            var zipPath = $"external/Zip/{repository}.zip";
            var extractPath = $"external/Zip/{repository}/";
            var finalPath = $"external/{repository}";

            File.Delete(zipPath);

            // Retry the following call according to the policy - 15 times.
            await policy.ExecuteAsync(() => waitAndRetry.ExecuteAsync(async () =>
            {
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    bootStrapper.FileSystem.GetCachePath(zipPath);
                    throw new HttpRequestException("Could not find a valid document at: " + url, default, response.StatusCode);
                }

                using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                    stream = new FileStream(zipPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await contentStream.CopyToAsync(stream);
                }

                LogToConsole(repository, "Extracting Files");

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

                LogToConsole(repository, "Restoring Packages for ");

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = $"/C dotnet restore {repository}.sln",
                    WorkingDirectory = Path.Combine(finalPath, "src")
                };
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                LogToConsole(repository,"Downloaded");
            }));
        })).ToArray());

        return bootStrapper;
    }

    public static string WithSourceFilter(this string repository, params string[] exclude)
    {
        var excludeFilter = exclude.Length > 0 ? string.Join(",", exclude.Select(x => $"!{x}")) + "," : string.Empty;
        return $"../../{repository}/src/**/{{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,{excludeFilter}}}/**/*.cs";
    }

    public static Bootstrapper ConfigureLinks(this Bootstrapper bootStrapper)
    {
        var isProduction = Environment.GetCommandLineArgs().Any(x => x.Contains("preview")) ? "false" : "true";
        LogInfo($"Is Production Build: {isProduction}");
        return bootStrapper.AddSetting(WebKeys.MakeLinksAbsolute, isProduction);
    }

    internal static void LogInfo(string message)
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

    private static void LogToConsole(string repository, string message)
    {
        lock (_lockObject)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[INFO] ");
            Console.ResetColor();
            Console.Write($"{message} {repository}...");
            Console.WriteLine();
        }
    }
}
