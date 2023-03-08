using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

public interface IPack : ICompile, IHasArtifacts, IHasGitRepository
{
    AbsolutePath PackagesDirectory => ArtifactsDirectory / "packages";

    Target Pack => _ => _
        .DependsOn(Compile)
        .Produces(PackagesDirectory / "*.nupkg")
        .Executes(() =>
        {
            DotNetPack(_ => _
                .Apply(PackSettingsBase)
                .Apply(PackSettings));

            ReportSummary(_ => _
                .AddPair("Packages", PackagesDirectory.GlobFiles("*.nupkg").Count.ToString()));
        });

    sealed Configure<DotNetPackSettings> PackSettingsBase => _ => _
        .SetProject(Solution)
        .SetConfiguration(Configuration)
        .SetNoBuild(SucceededTargets.Contains(Compile))
        .SetProperty("PackageOutputPath", PackagesDirectory)
        .WhenNotNull(this as IHasGitRepository, (_, o) => _
            .SetRepositoryUrl(o!.GitRepository.HttpsUrl))
        .WhenNotNull(this as IHasVersioning, (_, o) => _
            .SetVersion(o!.Versioning.NuGetVersionV2));

    Configure<DotNetPackSettings> PackSettings => _ => _;
}
