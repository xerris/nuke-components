# nuke-components

Shared components for the [NUKE build system](https://nuke.build/).

Read more about shared components in the [official docs.](https://nuke.build/docs/sharing/build-components/)

## Build

This project uses the NUKE build tool (naturally). NUKE builds can be invoked
in the following ways:

#### Script

There are PowerShell, cmd, and bash scripts for invoking builds and build
targets. To select a build target, specify it either as an argument or with the
`--target` switch. For example:

```powershell
./build.ps1 # Run the default build
./build.ps1 test # Run the 'test' target
./build.ps1 --target test # Run the 'test' target
./build.ps1 --help # Lists available targets
```

#### NUKE global tool

The .NET tools manifest for this solution includes the [NUKE global
tool](https://nuke.build/docs/getting-started/setup.html). You can install local
tools with this command:

```powershell
dotnet tool restore
```

Then, targets can be run from a shell with the `nuke` command. This tool is
very convenient as it enables tab completion of target names. For example:

```powershell
nuke build
nuke test
nuke integration-test
# etc.
```

### Console app

NUKE builds are pure C# console apps. So, to run a build you can just run the
`_build` project from your IDE, just as you would any other executable.

### IDE plugins

These support plugins can invoke builds, and make working with NUKE easier:

- [Microsoft VisualStudio](https://nuke.build/visualstudio)
- [Microsoft VSCode](https://nuke.build/vscode)
- [JetBrains ReSharper](https://nuke.build/resharper)
- [JetBrains Rider](https://nuke.build/rider)
