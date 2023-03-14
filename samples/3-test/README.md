# Sample: Test with Coverage Report

This sample runs unit tests in the solution using the [`ITest`](../../src/Components/Test.cs)
component. This component uses `dotnet test` under the hood.

To see the output, invoke the `Test` target with this command:

```powershell
nuke test
```

To additionally generate a coverage report and output it to the `./artifacts`
directory, invoke the `ReportCoverage` target as well:

```powershell
nuke test report-coverage
```

Coverage reports require a couple extra things:

- The [ReportGenerator](https://www.nuget.org/packages/ReportGenerator) tool
  must be installed in the NUKE build project as a `<PackageDownload/>`. See the
  project file in this sample.
- To collect coverage, test projects must use the [Coverlet collector](https://www.nuget.org/packages/coverlet.collector).
