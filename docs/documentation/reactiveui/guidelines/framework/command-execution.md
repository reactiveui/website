# Command execution

A view model that loads data, lets a user edit it, and sends it back raises questions a single `ReactiveCommand`
does not answer on its own. When should it load? How should saving be gated on both validity and an in-flight
save? How should a failure reach the screen? [Commands](../../handbook/commands/index.md) covers the properties a
command exposes for this. [Asynchronous commands](asynchronous-commands.md) covers keeping the command's own work
visible to them.

[A viewmodel using ReactiveUI that loads and sends data](https://codereview.stackexchange.com/questions/74642/a-viewmodel-using-reactiveui-6-that-loads-and-sends-data)
walks through one such view model end to end.
