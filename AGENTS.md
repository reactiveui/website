# AGENTS.md

[`CLAUDE.md`](CLAUDE.md) is the authoritative guide for this repository. Read it. It covers the build
commands, the repository conventions, the writing rules every page follows, and the work currently in
flight.

This file exists so an agent that looks for `AGENTS.md` finds its way there. It repeats only what you need
before you can run anything.

## Set up the environment

A fresh container has no .NET SDK. Install one before you do anything else:

```bash
./.claude/setup.sh
```

The script installs the .NET 10 and .NET 11 SDKs, links `dotnet` into `/usr/local/bin`, checks that
`api.nuget.org` answers, and shallow-clones the active ReactiveUI repositories beside this one. Running it
again is cheap, because every step checks before it acts.

The script exits 0 even when a download is blocked, and prints the host it could not reach. An environment
that cannot install .NET needs outbound access to `dot.net` and `*.dotnet.microsoft.com`. One that cannot
restore needs `api.nuget.org`.

### Running it as a cloud setup script

Both Claude Code and Codex run a setup script when they provision a container. Point that script at this
one rather than pasting a copy, so a fix here reaches every environment:

```bash
#!/bin/bash
curl -fsSL https://raw.githubusercontent.com/reactiveui/website/main/.claude/setup.sh -o /tmp/rxui-setup.sh || true
bash /tmp/rxui-setup.sh || true
exit 0
```

Three constraints that script has to meet, and this one does:

- It exits 0. A non-zero exit stops the session from starting.
- It finishes in a few minutes. Clones are shallow and run in parallel for that reason.
- It puts `dotnet` on `PATH` with a symlink, not a profile file. An agent's shell is neither a login shell
  nor an interactive one, so it reads no profile.

## Build the site

```bash
./build.sh serve    # watch, rebuild on save, serve on 127.0.0.1:8000
./build.sh build    # build once; this is the gate before any push
```

Link checking is on by default. A broken internal link fails the build.

## Before you write a page

Read the writing rules in [`CLAUDE.md`](CLAUDE.md). They are not optional, and they are not the defaults you
would otherwise use. The short version: write for a grade 8 reader who knows basic C#, put the main point
first, keep sentences short, use the active voice, and say "you".

Apply them to new work and to work you modify. Do not rewrite a page you were not asked to touch.
