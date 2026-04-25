#nullable enable

using System;
using System.ComponentModel;
using Nuke.Common.Tooling;

namespace ReactiveUI.Web;

/// <summary>
/// Strongly-typed enumeration of MSBuild configurations exposed to the
/// build via Nuke's parameter binding. Modelled as a Nuke <see
/// cref="Enumeration"/> so the value parses from the command line and
/// converts implicitly to a string when handed to the toolchain.
/// </summary>
[TypeConverter(typeof(TypeConverter<Configuration>))]
internal sealed class Configuration : Enumeration
{
    /// <summary>The <c>Debug</c> configuration.</summary>
    public static readonly Configuration Debug = new() { Value = nameof(Debug) };

    /// <summary>The <c>Release</c> configuration.</summary>
    public static readonly Configuration Release = new() { Value = nameof(Release) };

    /// <summary>
    /// Implicit conversion to a plain configuration string for callers
    /// that consume the raw MSBuild value (such as <c>dotnet</c>
    /// command-line arguments).
    /// </summary>
    /// <param name="configuration">The configuration to convert.</param>
    /// <returns>The configuration's string value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="configuration"/> is <see langword="null"/>.</exception>
    public static implicit operator string(Configuration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        return configuration.Value;
    }

    /// <summary>Returns the underlying configuration value.</summary>
    /// <returns>The configuration's string value.</returns>
    public override string ToString() => Value;
}
