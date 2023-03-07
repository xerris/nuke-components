using System.Collections.Generic;
using System.IO;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Xerris.Nuke.Components;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild, ILint, ITest, IReportCoverage, IHasConfiguration
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Compile);

    [Solution] readonly Solution Solution;
    Solution IHasSolution.Solution => Solution;

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            DotNetClean(s => s
                .SetProject(Solution));


            Directory.Delete(ArtifactsDirectory);
            Directory.CreateDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Clean, Restore)
        .DependsOn<ILint>(x => x.Lint)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(((IHasConfiguration)this).Configuration)
                .EnableNoRestore());
        });

    bool IReportCoverage.CreateCoverageHtmlReport => true;
    IEnumerable<Project> ITest.TestProjects => Partition.GetCurrent(Solution.GetProjects("*.Tests"));
}
