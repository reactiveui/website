await Bootstrapper
        .Factory
        .CreateDocs(args)
        .GetSources("reactiveui", "reactiveui", "akavache", "fusillade", "punchclock", "splat")
        .GetSources("reactivemarbles", "DynamicData")
        .AddSetting(WebKeys.MakeLinksAbsolute, "true") // Comment out to test locally with preview
        .AddSetting(MarkdownKeys.MarkdownExtensions, "bootstrap")
        .AddSourceFiles(
            "dynamicdata".WithSourceFilter(),
            "splat".WithSourceFilter("*.Drawing"),
            "reactiveui".WithSourceFilter("Benchmarks", "*.Test", "*.LeakTests", "*.TestRunner", "*.Uwp"),
            "akavache".WithSourceFilter(),
            "punchclock".WithSourceFilter(),
            "fusillade".WithSourceFilter())
        .RunAsync();
