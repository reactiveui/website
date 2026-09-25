# Debugging ReactiveUI

Every ReactiveUI package on NuGet ships with [SourceLink](https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/sourcelink)
metadata embedded in the assembly's PDB (`<DebugType>embedded</DebugType>` +
`Microsoft.SourceLink.GitHub`, configured once in
[`src/Directory.Build.props`](https://github.com/reactiveui/reactiveui/blob/main/src/Directory.Build.props)).
That means you can step from your own code straight into the framework source
on GitHub at the exact commit your installed version was built from — no
matching local clone, no symbol-server hunting.

## Visual Studio (2022 and newer)

Two debugger settings unlock SourceLink stepping. Both live under
**Tools → Options → Debugging**:

1. **General** page
   - **Enable Just My Code** — *uncheck*. With Just My Code on, the debugger
     never asks the SourceLink resolver for framework frames; turning it off
     lets the resolver fetch the matching ReactiveUI source on demand.
   - **Enable Source Link support** — *check*. This is the master switch
     that wires `Microsoft.VisualStudio.Debugger.SourceLink` into the
     symbol load path.
   - *(Recommended)* **Enable source server support** + **Suppress JIT
     optimization on module load (Managed only)** — gives you readable
     locals when stepping into release-built framework code.
2. **Symbols** page
   - Make sure **Microsoft Symbol Servers** (or another server hosting the
     ReactiveUI PDBs — typically not needed since `embedded` ships the PDB
     inside the .dll itself) is enabled. With embedded PDBs, no extra
     server entry is required.

Set a breakpoint, hit it, then *Step Into* (`F11`) any ReactiveUI call
(e.g. `this.WhenAnyValue(...)`). Visual Studio prompts once with
*"Source Link will download <https://raw.githubusercontent.com/reactiveui/...>
— OK?"*; accept and you'll land in `WhenAnyMixin.cs` at the exact commit your
NuGet package was published from.

## Rider / VS Code (C# Dev Kit)

Rider honors SourceLink out of the box — make sure
**Settings → Build, Execution, Deployment → Debugger → Enable external source
debug** is on, and disable Just My Code under the same screen.

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

<div class="youtube-video-container"><iframe src="https://www.youtube.com/embed/gyRGhCQPkB4" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

## Troubleshooting

- **Step Into still skips framework frames.** Confirm Just My Code is off
  and that the package version on disk matches a published release tag (the
  SourceLink URL embeds the commit SHA — local builds without a release
  commit can't be resolved).
- **`The remote endpoint could not be reached`.** Your network blocks
  `raw.githubusercontent.com`. Either whitelist that host or run with the
  source already on disk and point Visual Studio at it via
  **Debug → Options → Symbols → Specify excluded modules**.
- **Mobile heads (iOS / Android via .NET MAUI).** The platform debuggers
  honor SourceLink for managed code today, but native interop frames stay
  opaque. For pure-managed ReactiveUI calls the experience matches WPF /
  WinForms.
