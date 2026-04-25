#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// Pure helpers for selecting target frameworks from package contents and
/// matching <c>lib/</c> TFMs against available <c>refs/</c> TFMs. All
/// methods are deliberately stateless so they can be unit-tested in
/// isolation from any IO.
/// </summary>
internal static class TfmResolver
{
    /// <summary>
    /// Selects the best matching TFM from <paramref name="availableTfms"/>
    /// using a per-package override first, then the global preference list,
    /// then the first available TFM as a last resort. Both override and
    /// preference matches try exact match before falling back to a prefix
    /// match (so <c>net10.0-android</c> matches <c>net10.0-android35.0</c>).
    /// </summary>
    /// <param name="availableTfms">TFMs present in the package's <c>lib/</c> directory.</param>
    /// <param name="tfmOverride">Optional per-package TFM override sourced from <see cref="PackageConfig.TfmOverrides"/>.</param>
    /// <param name="tfmPreference">Ordered list of preferred TFMs from <see cref="PackageConfig.TfmPreference"/>.</param>
    /// <returns>The selected TFM, or <see langword="null"/> if <paramref name="availableTfms"/> is empty.</returns>
    public static string? SelectTfm(
        ICollection<string> availableTfms,
        string? tfmOverride,
        string[] tfmPreference)
    {
        if (tfmOverride != null)
        {
            var exact = availableTfms.FirstOrDefault(t =>
                string.Equals(t, tfmOverride, StringComparison.OrdinalIgnoreCase));
            if (exact != null)
            {
                return exact;
            }

            var prefix = availableTfms.FirstOrDefault(t =>
                t.StartsWith(tfmOverride, StringComparison.OrdinalIgnoreCase));
            if (prefix != null)
            {
                return prefix;
            }
        }

        foreach (var pref in tfmPreference)
        {
            var exact = availableTfms.FirstOrDefault(t =>
                string.Equals(t, pref, StringComparison.OrdinalIgnoreCase));
            if (exact != null)
            {
                return exact;
            }
        }

        foreach (var pref in tfmPreference)
        {
            var prefix = availableTfms.FirstOrDefault(t =>
                t.StartsWith(pref, StringComparison.OrdinalIgnoreCase));
            if (prefix != null)
            {
                return prefix;
            }
        }

        return availableTfms.FirstOrDefault();
    }

    /// <summary>
    /// Finds the most appropriate <c>refs/</c> TFM directory to pair with a
    /// package's <c>lib/</c> TFM. Strips any platform suffix from the lib
    /// TFM, prefers an exact match, falls back to a prefix match, and for
    /// netstandard libs picks the highest available modern .NET refs.
    /// </summary>
    /// <param name="libTfm">The TFM under <c>lib/</c> being resolved (for example <c>net10.0-android35.0</c>).</param>
    /// <param name="refsTfms">All TFMs present under <c>refs/</c>.</param>
    /// <returns>The chosen refs TFM, or <see langword="null"/> when no compatible refs exist (legacy TFMs such as <c>monoandroid</c>).</returns>
    public static string? FindBestRefsTfm(string libTfm, List<string> refsTfms)
    {
        var baseTfm = libTfm.Contains('-')
            ? libTfm.Substring(0, libTfm.IndexOf('-'))
            : libTfm;

        var exact = refsTfms.FirstOrDefault(r =>
            string.Equals(r, baseTfm, StringComparison.OrdinalIgnoreCase));
        if (exact != null)
        {
            return exact;
        }

        var prefix = refsTfms.FirstOrDefault(r =>
            baseTfm.StartsWith(r, StringComparison.OrdinalIgnoreCase));
        if (prefix != null)
        {
            return prefix;
        }

        if (baseTfm.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase))
        {
            return refsTfms
                .Where(r => r.StartsWith("net", StringComparison.OrdinalIgnoreCase)
                    && !r.StartsWith("net4", StringComparison.OrdinalIgnoreCase)
                    && !r.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(r => r)
                .FirstOrDefault();
        }

        return null;
    }

    /// <summary>
    /// Returns the platform classification for a TFM (one of
    /// <c>android</c>, <c>ios</c>, <c>maccatalyst</c>, <c>windows</c>) used
    /// to bucket platform-specific docfx output, or <see langword="null"/>
    /// for platform-neutral TFMs that should map to the default <c>api</c>
    /// destination. Recognises both modern (<c>net10.0-android</c>) and
    /// legacy (<c>monoandroid</c>, <c>xamarinios</c>, <c>uap</c>) forms.
    /// </summary>
    /// <param name="tfm">The TFM to classify.</param>
    /// <returns>The platform label, or <see langword="null"/> if the TFM is platform-neutral.</returns>
    public static string? GetPlatformLabel(string tfm)
    {
        if (tfm.Contains("-android", StringComparison.OrdinalIgnoreCase))
        {
            return "android";
        }

        if (tfm.Contains("-ios", StringComparison.OrdinalIgnoreCase))
        {
            return "ios";
        }

        if (tfm.Contains("-maccatalyst", StringComparison.OrdinalIgnoreCase))
        {
            return "maccatalyst";
        }

        if (tfm.Contains("-windows", StringComparison.OrdinalIgnoreCase))
        {
            return "windows";
        }

        if (tfm.StartsWith("monoandroid", StringComparison.OrdinalIgnoreCase))
        {
            return "android";
        }

        if (tfm.StartsWith("xamarinios", StringComparison.OrdinalIgnoreCase))
        {
            return "ios";
        }

        if (tfm.StartsWith("xamarinmac", StringComparison.OrdinalIgnoreCase))
        {
            return "maccatalyst";
        }

        if (tfm.StartsWith("uap", StringComparison.OrdinalIgnoreCase))
        {
            return "windows";
        }

        return null;
    }
}
