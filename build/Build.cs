using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Xerris.Nuke.Components;

// ReSharper disable RedundantExtendsListEntry
// ReSharper disable InconsistentNaming

[DotNetVerbosityMapping]
[ShutdownDotNetAfterServerBuild]
partial class Build : NukeBuild,
    IHasGitRepository,
    IHasVersioning,
    IRestore,
    IFormat,
    IClean,
    ICompile,
    ITest,
    IReportCoverage,
    IPack,
    IPush
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => ((ICompile) x).Compile);

    [Solution]
    readonly Solution Solution;
    Solution IHasSolution.Solution => Solution;

    public IEnumerable<string> ExcludedFormatPaths => Enumerable.Empty<string>();

    public bool RunFormatAnalyzers => true;

    Target ICompile.Compile => _ => _
        .Inherit<ICompile>()
        .DependsOn<IFormat>(x => x.VerifyFormat);

    bool IReportCoverage.CreateCoverageHtmlReport => true;

    IEnumerable<Project> ITest.TestProjects => Partition.GetCurrent(Solution.GetProjects("*.Tests"));

    Configure<DotNetPublishSettings> ICompile.PublishSettings => _ => _
        .When(!ScheduledTargets.Contains(((IPush) this).Push), _ => _
            .ClearProperties());

    Target IPush.Push => _ => _
        .Inherit<IPush>()
        .Consumes(this.FromComponent<IPush>().Pack)
        .Requires(() => this.FromComponent<IHasGitRepository>().GitRepository.Tags.Any())
        .WhenSkipped(DependencyBehavior.Execute);
}
