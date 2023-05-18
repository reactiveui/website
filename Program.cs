await Bootstrapper
        .Factory
        .CreateDocs(args)
        .AddSetting(Statiq.Markdown.MarkdownKeys.MarkdownExtensions, "bootstrap")
        .RunAsync();
