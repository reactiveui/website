await Bootstrapper
        .Factory
        .CreateDocs(args)
        .GetSources("reactiveui", "reactiveui", "akavache", "fusillade", "punchclock", "splat")
        .GetSources("reactivemarbles", "DynamicData")
        .AddSetting(MarkdownKeys.MarkdownExtensions, "bootstrap")
        .AddSourceFiles(
            "../../dynamicdata/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../splat/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.TestRunner,!Benchmarks,}/**/*.cs",
            "../../reactiveui/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Test,!*.Uwp,!*.Templates,!Benchmarks,!*.TestRunner,!*.LeakTests,}/**/*.cs",
            "../../akavache/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../punchclock/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs",
            "../../fusillade/src/**/{!.git,!bin,!obj,!packages,!*.Tests,!*.Templates,!*.Benchmarks,}/**/*.cs")
        .RunAsync();
