await Bootstrapper
        .Factory
        .CreateDocs(args)
        .FetchTheme()
        //.GetSources("reactiveui", "reactiveui", "akavache", "fusillade", "punchclock", "splat")
        //.GetSources("reactivemarbles", "DynamicData")
        .ConfigureLinks(args)
        .AddSetting(MarkdownKeys.MarkdownExtensions, "bootstrap")
        //.AddSourceFiles(
        //    "dynamicdata".WithSourceFilter(),
        //    "splat".WithSourceFilter("*.Drawing"),
        //    "reactiveui".WithSourceFilter("Benchmarks", "*.Test", "*.LeakTests", "*.TestRunner", "*.Uwp"),
        //    "akavache".WithSourceFilter(),
        //    "punchclock".WithSourceFilter(),
        //    "fusillade".WithSourceFilter())
        .RunAsync();

var entries = Directory.GetFileSystemEntries("output", "*", SearchOption.AllDirectories);
foreach (var entry in entries)
{
    Console.WriteLine(entry);
}

StreamReader sr = new StreamReader("output\\index.html");
//Read the first line of text
var line = sr.ReadLine();
//Continue to read until you reach end of file
while (line != null)
{
    //write the line to console window
    Console.WriteLine(line);
    //Read the next line
    line = sr.ReadLine();
}
//close the file
sr.Close();
