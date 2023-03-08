using System.Collections.Generic;
using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Xerris.Nuke.Components;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable RedundantExtendsListEntry
// ReSharper disable InconsistentNaming

class Build : NukeBuild,
    IHasGitRepository,
    IRestore,
    ILint,
    ICompile,
    IPack,
    ITest,
    IReportCoverage
//IPublish // TODO
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
                .SetProject(((IHasSolution) this).Solution));

            EnsureCleanDirectory(((IHasArtifacts) this).ArtifactsDirectory);
        });

    Target ICompile.Compile => _ => _
        .Inherit<ICompile>()
        .DependsOn(Clean)
        .DependsOn<ILint>(x => x.Lint);

    bool IReportCoverage.CreateCoverageHtmlReport => true;

    IEnumerable<Project> ITest.TestProjects => Partition.GetCurrent(Solution.GetProjects("*.Tests"));
}
