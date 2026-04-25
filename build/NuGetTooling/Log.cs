#nullable enable

using System;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// Provides thread-safe coloured console logging used by the package fetching
/// and docfx configuration components. The single shared lock ensures that
/// concurrent fetch tasks never produce interleaved output.
/// </summary>
internal static class Log
{
    /// <summary>
    /// Synchronisation primitive guarding <see cref="Console"/> mutations so
    /// that colour state and the prefix/message pair remain atomic across
    /// concurrent callers.
    /// </summary>
    private static readonly object _lockConsoleObject = new();

    /// <summary>
    /// Writes an informational message to the console with a green
    /// <c>[INFO]</c> prefix.
    /// </summary>
    /// <param name="message">The message to emit.</param>
    public static void Info(string message)
    {
        lock (_lockConsoleObject)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[INFO] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }

    /// <summary>
    /// Writes an error message to the console with a red <c>[ERROR]</c>
    /// prefix.
    /// </summary>
    /// <param name="message">The error message to emit.</param>
    public static void Error(string message)
    {
        lock (_lockConsoleObject)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }
}
