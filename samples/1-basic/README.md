# Sample: Basic

This sample performs a basic build of the solution using the
[`ICompile`](../../src/Xerris.Nuke.Components/ICompile.cs) target.

In this example, the `Compile` target depends on the `Clean` target. When the
default build is run, the repository will be cleaned, and then the solution
will be built. These components use `dotnet clean` and `dotnet build` under the
hood.

To see the output, invoke the default build from the solution directory for this
sample:

```powershell
nuke
```
