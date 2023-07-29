// Copyright (c) 2023 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Web;

internal static class Helper
{
    private static readonly object _lockConsoleObject = new();

    public static Bootstrapper ConfigureLinks(this Bootstrapper bootstrapper, string[] args)
    {
        var isProduction = args.Any(x => x.Contains("preview", StringComparison.OrdinalIgnoreCase)) ? "false" : "true";
        LogInfo($"Is Production Build: {isProduction}");
        return bootstrapper.AddSetting(WebKeys.MakeLinksAbsolute, isProduction);
    }

    public static string WithSourceFilter(this string repository, params string[] exclude)
    {
        var excludeFilter = exclude.Length > 0 ? string.Join(",", exclude.Select(x => $"!{x}")) + "," : string.Empty;
        return $"../../{repository}/src/**/{{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,{excludeFilter}}}/**/*.cs";
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Deliberate")]
    private static void LogInfo(string message)
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
}
