# nuke-components | ![release](https://github.com/xerris/nuke-components/actions/workflows/release.yml/badge.svg)

Shared components for the [NUKE build system](https://nuke.build/).

Read more about shared components in the [official docs.](https://nuke.build/docs/sharing/build-components/)

## Usage

To use the shared components in your build, simply install the NuGet package:

```powershell
dotnet add package .\build\MyNukeBuild.csproj Xerris.Nuke.Components
```

See the [samples](./samples/) for examples of how to use these components in
your build projects

## Build

This project uses the NUKE build tool (naturally). NUKE builds can be invoked
in the following ways:

### NUKE global tool

The preferred way to invoke NUKE builds is with the [global tool](https://nuke.build/docs/getting-started/setup.html).
To install it, run the following command:

```powershell
dotnet tool install nuke.globaltool -g
```

Verify your installation by listing the available targets with this command:

```powershell
nuke --help
```

Build targets can now be run like so:

```powershell
nuke build
nuke test
nuke verify-format
# etc.
```

> ℹ For added flavour, enable tab-completion for the global tool in your shell.
> See the official docs for instructions [here](https://nuke.build/docs/global-tool/shell-completion/).

### Scripts

NUKE generates PowerShell, cmd, and bash scripts that invoke builds and build
targets. To select a build target, specify it either as an argument or with the
`--target` switch. For example:

```powershell
./build.ps1 # Run the default build
./build.ps1 test # Run the 'test' target
./build.ps1 --target test # Run the 'test' target
```

### Console app

NUKE builds are pure C# console apps. So, to run a build you can run the
`_build` project from your IDE, just as you would any other executable.

### IDE plugins

NUKE also provides plugins to invoke builds from your preferred IDE:

- [Microsoft VisualStudio](https://nuke.build/visualstudio)
- [Microsoft VSCode](https://nuke.build/vscode)
- [JetBrains ReSharper](https://nuke.build/resharper)
- [JetBrains Rider](https://nuke.build/rider)
