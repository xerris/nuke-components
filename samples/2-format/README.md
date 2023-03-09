# Sample: Format

This sample checks the formatting of the code in the solution using the
[`IFormat`](../../src/Components/IFormat.cs), and breaks if code is not
formatted according to the .editorconfig for the solution. This component uses
`dotnet format` under the hood.

If formatting passes, a basic build of the solution using the
[`ICompile`](../../Components/ICompile.cs) target.

To see the output, invoke `Format` target with this command:

```powershell
nuke format
```

In the case that formatting issues are found, the `IFormat` component also
provides a target to fix the formatting:

```powershell
nuke fix-format
```

Since the default compile target depends on `IFormat`, the formatting target is
run as part of the default build. This is entirely optional. For example, you
could implement the `IFormat` without listing it as a dependency of the compile
target. Then, the formatting commands (above) would still be available, but
would not be run by default.

```csharp
// With dependency
Target ICompile.Compile => _ => _
    .Inherit<ICompile>()
    .DependsOn(Clean)
    .DependsOn<IFormat>(x => x.VerifyFormat);

// Without dependency
Target ICompile.Compile => _ => _
    .Inherit<ICompile>()
    .DependsOn(Clean);
```
