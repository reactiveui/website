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
- It puts `dotnet` on `PATH` two ways. A symlink in `/usr/local/bin` resolves in any shell. A line in
  `~/.bashrc` carries `DOTNET_ROOT` across, which is what Codex documents, because its setup script runs in
  its own Bash session and an `export` there does not reach the agent.

### Where each provider keeps these settings

|                  | Claude Code                                                | Codex                                       |
|------------------|------------------------------------------------------------|---------------------------------------------|
| Where to set it  | The environment selector at `claude.ai/code`, above the message box. There is no settings page or URL. | **Codex settings → Environments**           |
| Network setting  | Access level: Trusted, Custom, or None                      | Agent internet access: Off, or On with an allowlist |
| Allowlist preset | Trusted list, optionally kept when you pick Custom          | None, Common dependencies, or All           |
| Setup script net | Follows the environment's access level                      | Always on, whatever the agent setting is    |
| Cache            | Filesystem snapshot, rebuilt when the script or hosts change, expires in about seven days | Container state for up to 12 hours, with a **Reset cache** button |

The difference in the "Setup script net" row matters. On Codex the SDK install works even with agent
internet off, because setup scripts always have access. The allowlist only governs what the agent itself
reaches later, so it is `api.nuget.org` that has to be on it for a restore during a session.

On Claude Code the setup script runs under the environment's access level, so the SDK hosts have to be
allowed or the install fails. `dot.net` is on the default list; `builds.dotnet.microsoft.com` is not,
because the default entry `dotnet.microsoft.com` carries no wildcard. Add `*.dotnet.microsoft.com`.

Codex's **Common dependencies** preset already covers `dot.net`, `dotnet.microsoft.com` and `nuget.org`.

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
