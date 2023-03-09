using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Xerris.Nuke.Components;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable RedundantExtendsListEntry
// ReSharper disable InconsistentNaming

[DotNetVerbosityMapping]
[ShutdownDotNetAfterServerBuild]
partial class Build : NukeBuild,
    IHasGitRepository,
    IHasVersioning,
    IRestore,
    IFormat,
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

    Target Clean => _ => _
        .Before<IRestore>()
        .Executes(() =>
        {
            DotNetClean(_ => _
                .SetProject(Solution));

            EnsureCleanDirectory(FromComponent<IHasArtifacts>().ArtifactsDirectory);
        });

    public IEnumerable<string> ExcludedFormatPaths => Enumerable.Empty<string>();

    public bool RunFormatAnalyzers => true;

    Target ICompile.Compile => _ => _
        .Inherit<ICompile>()
        .DependsOn(Clean)
        .DependsOn<IFormat>(x => x.VerifyFormat);

    bool IReportCoverage.CreateCoverageHtmlReport => true;

    IEnumerable<Project> ITest.TestProjects => Partition.GetCurrent(Solution.GetProjects("*.Tests"));

    Configure<DotNetPublishSettings> ICompile.PublishSettings => _ => _
        .When(!ScheduledTargets.Contains(((IPush) this).Push), _ => _
            .ClearProperties());

    Target IPush.Push => _ => _
        .Inherit<IPush>()
        .Consumes(FromComponent<IPush>().Pack)
        .Requires(() =>
            FromComponent<IHasGitRepository>().GitRepository.IsOnMainBranch() ||
            FromComponent<IHasGitRepository>().GitRepository.IsOnReleaseBranch() ||
            FromComponent<IHasGitRepository>().GitRepository.Tags.Any())
        .WhenSkipped(DependencyBehavior.Execute);

    T FromComponent<T>()
        where T : INukeBuild
        => (T) (object) this;
}
