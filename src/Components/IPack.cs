using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides targets and configuration for creating NuGet packages using <c>dotnet pack</c>.
/// </summary>
public interface IPack : ICompile, IHasArtifacts, IHasGitRepository
{
    /// <summary>
    /// The output directory for NuGet packages.
    /// </summary>
    AbsolutePath PackagesDirectory => ArtifactsDirectory / "packages";

    /// <summary>
    /// Run <c>dotnet pack</c> on the solution.
    /// </summary>
    Target Pack => _ => _
        .Description("Create NuGet packages for the solution.")
        .DependsOn(Compile)
        .TryAfter<ITest>()
        .Produces(PackagesDirectory / "*.nupkg")
        .Executes(() =>
        {
            DotNetPack(_ => _
                .Apply(PackSettingsBase)
                .Apply(PackSettings));

            ReportSummary(_ => _
                .AddPair("Packages", PackagesDirectory.GlobFiles("*.nupkg").Count.ToString()));
        });

    /// <summary>
    /// Settings for controlling the behavior of the <c>dotnet pack</c> command.
    /// </summary>
    sealed Configure<DotNetPackSettings> PackSettingsBase => _ => _
        .SetProject(Solution)
        .SetConfiguration(Configuration)
        .SetNoBuild(SucceededTargets.Contains(Compile))
        .SetProperty("PackageOutputPath", PackagesDirectory)
        .WhenNotNull(this as IHasGitRepository, (_, o) => _
            .SetRepositoryUrl(o!.GitRepository.HttpsUrl))
        .WhenNotNull(this as IHasVersioning, (_, o) => _
            .SetVersion(o!.Versioning.NuGetVersionV2));

    /// <summary>
    /// Additional settings for controlling the behavior of the <c>dotnet pack</c> command.
    /// </summary>
    Configure<DotNetPackSettings> PackSettings => _ => _;
}
