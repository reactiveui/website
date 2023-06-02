await Bootstrapper
        .Factory
        .CreateDocs(args)
        .FetchTheme()
        .GetSources("reactiveui", "reactiveui", "akavache", "fusillade", "punchclock", "splat")
        .GetSources("reactivemarbles", "DynamicData")
        .ConfigureLinks(args)
        .AddSetting(MarkdownKeys.MarkdownExtensions, "bootstrap")
        .AddSourceFiles(
            "dynamicdata".WithSourceFilter(),
            "splat".WithSourceFilter("*.Drawing"),
            "reactiveui".WithSourceFilter("Benchmarks", "*.Test", "*.LeakTests", "*.TestRunner", "*.Uwp"),
            "akavache".WithSourceFilter(),
            "punchclock".WithSourceFilter(),
            "fusillade".WithSourceFilter())
        .RunAsync();
