using Statiq.Markdown;

await Bootstrapper
        .Factory
        .CreateDocs(args)
        .AddSetting(MarkdownKeys.MarkdownExtensions, "bootstrap")
        .AddSourceFiles(
            "../../dynamicdata/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            ////"../../splat/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            //// TODO: OBTAIN SOURCE"../../reactiveui/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../akavache/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../punchclock/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../fusillade/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs")
        .AddProjectFiles(
            "../../splat/src/Splat/Splat.csproj",
            //// TODO FIND ISSUE WITH LOADING THIS"../../splat/src/Splat.Drawing/Splat.Drawing.csproj",
            "../../splat/src/Splat.ApplicationInsights/Splat.ApplicationInsights.csproj",
            "../../splat/src/Splat.Autofac/Splat.Autofac.csproj",
            "../../splat/src/Splat.Exceptionless/Splat.Exceptionless.csproj",
            "../../splat/src/Splat.Log4Net/Splat.Log4Net.csproj",
            "../../splat/src/Splat.Microsoft.Extensions.DependencyInjection/Splat.Microsoft.Extensions.DependencyInjection.csproj",
            "../../splat/src/Splat.Microsoft.Extensions.Logging/Splat.Microsoft.Extensions.Logging.csproj",
            "../../splat/src/Splat.Ninject/Splat.Ninject.csproj",
            "../../splat/src/Splat.NLog/Splat.NLog.csproj",
            "../../splat/src/Splat.Prism/Splat.Prism.csproj",
            "../../splat/src/Splat.Raygun/Splat.Raygun.csproj",
            "../../splat/src/Splat.Serilog/Splat.Serilog.csproj",
            "../../splat/src/Splat.SimpleInjector/Splat.SimpleInjector.csproj")
        .RunAsync();
