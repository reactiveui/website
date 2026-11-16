// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace ReactiveUI.Web;

/// <summary>Provides loggers and build progress messages.</summary>
internal sealed partial class WebsiteLogging
{
    /// <summary>Logger factory shared with downstream consumers.</summary>
    private readonly ILoggerFactory _factory;

    /// <summary>Default logger used by the source-generated message delegates.</summary>
    private readonly ILogger<WebsiteLogging> _logger;

    /// <summary>Initializes a new instance of the <see cref="WebsiteLogging"/> class.</summary>
    public WebsiteLogging()
    {
        _factory = LoggerFactory.Create(static builder =>
            builder
                .SetMinimumLevel(LogLevel.Information)
                .AddConsole(static opts => opts.FormatterName = TerseConsoleFormatter.FormatterName)
                .AddConsoleFormatter<TerseConsoleFormatter, Microsoft.Extensions.Logging.Console.ConsoleFormatterOptions>(static opts =>
                {
                    opts.TimestampFormat = "HH:mm:ss ";
                    opts.IncludeScopes = false;
                }));
        _logger = _factory.CreateLogger<WebsiteLogging>();
    }

    /// <summary>Logs a "site built" message.</summary>
    /// <param name="siteOutputPath">Absolute path to the rendered site output directory.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SiteBuilt(string siteOutputPath) => LogSiteBuilt(_logger, siteOutputPath);

    /// <summary>Logs a "now serving" message.</summary>
    /// <param name="port">Port the dev server is bound to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Serving(int port) => LogServing(_logger, port);

    /// <summary>Creates an <see cref="ILogger"/> typed against <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">Consumer type used as the log category.</typeparam>
    /// <returns>The typed logger.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ILogger<T> For<T>() => _factory.CreateLogger<T>();

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Site built at {SiteOutputPath}")]
    private static partial void LogSiteBuilt(ILogger logger, string siteOutputPath);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Serving site at http://127.0.0.1:{Port} (Ctrl-C to stop)")]
    private static partial void LogServing(ILogger logger, int port);
}
