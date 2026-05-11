// Copyright (c) 2019-2026 ReactiveUI Association Incorporated and contributors. All rights reserved.
// ReactiveUI Association Incorporated and contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace ReactiveUI.Web;

/// <summary>Console formatter that emits <c>HH:mm:ss [level] message</c> with ANSI-coloured level tags.</summary>
[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by the logging-options pipeline via AddConsoleFormatter<TFormatter, TOptions>.")]
[SuppressMessage(
    "Major Code Smell",
    "S2479:Replace the control character at position 1 by its escape sequence '\\u001B'.",
    Justification = "ANSI CSI introducer is the literal ESC byte (0x1B); compiler output is identical to '\\u001B'.")]
internal sealed class TerseConsoleFormatter : ConsoleFormatter
{
    /// <summary>Formatter name registered with the logging builder.</summary>
    public new const string Name = "terse";

    /// <summary>ANSI reset.</summary>
    private const string AnsiReset = "[0m";

    /// <summary>ANSI green foreground.</summary>
    private const string AnsiGreen = "[32m";

    /// <summary>ANSI yellow foreground.</summary>
    private const string AnsiYellow = "[33m";

    /// <summary>ANSI red foreground.</summary>
    private const string AnsiRed = "[31m";

    /// <summary>ANSI bold-red foreground.</summary>
    private const string AnsiBoldRed = "[1;31m";

    /// <summary>ANSI bright-black (gray) foreground.</summary>
    private const string AnsiGray = "[90m";

    /// <summary>Bound formatter options.</summary>
    private readonly ConsoleFormatterOptions _options;

    /// <summary>Time source for the optional leading timestamp.</summary>
    private readonly TimeProvider _timeProvider;

    /// <summary>Initializes a new instance of the <see cref="TerseConsoleFormatter"/> class.</summary>
    /// <param name="options">Console-formatter options bound from configuration.</param>
    public TerseConsoleFormatter(IOptions<ConsoleFormatterOptions> options)
        : base(Name)
    {
        ArgumentNullException.ThrowIfNull(options);
        _options = options.Value;
        _timeProvider = TimeProvider.System;
    }

    /// <inheritdoc/>
    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        ArgumentNullException.ThrowIfNull(textWriter);

        var message = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception);
        if (message is null && logEntry.Exception is null)
        {
            return;
        }

        if (!string.IsNullOrEmpty(_options.TimestampFormat))
        {
            textWriter.Write(_timeProvider.GetLocalNow().ToString(_options.TimestampFormat, CultureInfo.InvariantCulture));
        }

        textWriter.Write(LevelTag(logEntry.LogLevel));
        textWriter.Write(' ');

        if (message is not null)
        {
            textWriter.WriteLine(message);
        }
        else
        {
            textWriter.WriteLine(logEntry.Exception);
        }
    }

    /// <summary>Returns the bracketed, ANSI-coloured tag for <paramref name="level"/>.</summary>
    /// <param name="level">Log level.</param>
    /// <returns>Coloured tag string.</returns>
    private static string LevelTag(LogLevel level) => level switch
    {
        LogLevel.Trace => AnsiGray + "[trace]" + AnsiReset,
        LogLevel.Debug => AnsiGray + "[debug]" + AnsiReset,
        LogLevel.Information => AnsiGreen + "[info]" + AnsiReset,
        LogLevel.Warning => AnsiYellow + "[warn]" + AnsiReset,
        LogLevel.Error => AnsiRed + "[error]" + AnsiReset,
        LogLevel.Critical => AnsiBoldRed + "[crit]" + AnsiReset,
        _ => "[" + level.ToString().ToLowerInvariant() + "]"
    };
}
