# Debugging ReactiveUI

Every ReactiveUI package on NuGet ships with [SourceLink](https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/sourcelink)
metadata. The metadata is embedded in the assembly's PDB, a file that holds debug information. This comes from
`<DebugType>embedded</DebugType>` and `Microsoft.SourceLink.GitHub`, configured once in
[`src/Directory.Build.props`](https://github.com/reactiveui/reactiveui/blob/main/src/Directory.Build.props).
That means you can step from your own code straight into the framework source on GitHub. You land at the exact
commit your installed version was built from. You need no matching local clone. You need no symbol-server
hunting either.

## Visual Studio (2022 and newer)

Two debugger settings unlock SourceLink stepping. Both live under
**Tools → Options → Debugging**:

1. **General** page
   - **Enable Just My Code** — *uncheck*. When Just My Code is on, the debugger
     never asks the SourceLink resolver for framework frames. Turning it off
     lets the resolver fetch the matching ReactiveUI source on demand.
   - **Enable Source Link support** — *check*. This is the master switch
     that wires `Microsoft.VisualStudio.Debugger.SourceLink` into the
     symbol load path.
   - *(Recommended)* **Enable source server support** and **Suppress JIT
     optimization on module load (Managed only)**. These give you readable
     locals when stepping into release-built framework code.
2. **Symbols** page
   - Make sure **Microsoft Symbol Servers** is enabled, or another server
     that hosts the ReactiveUI PDBs. You usually don't need this step,
     because the embedded PDB already ships inside the .dll.

Set a breakpoint, hit it, then *Step Into* (`F11`) any ReactiveUI call
(e.g. `this.WhenAnyValue(...)`). Visual Studio prompts once with
*"Source Link will download <https://raw.githubusercontent.com/reactiveui/...>
— OK?"* Accept it, and you land in the framework's source file at the exact
commit your NuGet package was published from.

## Rider / VS Code (C# Dev Kit)

Rider honors SourceLink out of the box. Make sure
**Settings → Build, Execution, Deployment → Debugger → Enable external source
debug** is on. Disable Just My Code under the same screen.

In VS Code with the C# Dev Kit, set in `launch.json`:

```jsonc
{
    "justMyCode": false,
    "suppressJITOptimizations": true,
    "symbolOptions": {
        "searchMicrosoftSymbolServer": true
    }
}
```

## Quick demo

[Watch the SourceLink debugging demo on YouTube](https://www.youtube.com/watch?v=gyRGhCQPkB4)

## Troubleshooting

- **Step Into still skips framework frames.** Confirm Just My Code is off.
  Also confirm the package version on disk matches a published release tag.
  The SourceLink URL embeds the commit SHA, so a local build without a
  release commit can't be resolved.
- **`The remote endpoint could not be reached`.** Your network blocks
  `raw.githubusercontent.com`. Either allow that host, or run with the
  source already on disk and point Visual Studio at it via
  **Debug → Options → Symbols → Specify excluded modules**.
- **Mobile heads (iOS / Android via .NET MAUI).** The platform debuggers
  honor SourceLink for managed code today. Native interop frames stay
  opaque, though. For pure-managed ReactiveUI calls, the experience matches
  WPF and WinForms.
