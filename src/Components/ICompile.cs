using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

/// <summary>
/// Targets and settings for compiling the solution.
/// </summary>
public interface ICompile : IRestore, IClean, IHasConfiguration
{
    /// <summary>
    /// Compile the solution with <c>dotnet build</c>.
    /// </summary>
    Target Compile => _ => _
        .Description("Compile the solution")
        .DependsOn(Clean)
        .DependsOn(Restore)
        .WhenSkipped(DependencyBehavior.Skip)
        .Executes(() =>
        {
            ReportSummary(_ => _
                .WhenNotNull(this as IHasVersioning, (_, o) => _
                    .AddPair("Version", o!.Versioning.FullSemVer)));

            DotNetBuild(_ => _
                .Apply(CompileSettingsBase)
                .Apply(CompileSettings));

            DotNetPublish(_ => _
                    .Apply(PublishSettingsBase)
                    .Apply(PublishSettings)
                    .CombineWith(PublishConfigurations, (_, v) => _.SetProject((string) v.Project)
                        .SetFramework(v.Framework)),
                PublishDegreeOfParallelism);
        });

    /// <summary>
    /// Settings for controlling compilation behavior.
    /// </summary>
    sealed Configure<DotNetBuildSettings> CompileSettingsBase => _ => _
        .SetProjectFile(Solution)
        .SetConfiguration(Configuration)
        .When(IsServerBuild, _ => _
            .EnableContinuousIntegrationBuild())
        .SetNoRestore(SucceededTargets.Contains(Restore))
        .WhenNotNull(this as IHasGitRepository, (_, o) => _
            .SetRepositoryUrl(o!.GitRepository.HttpsUrl))
        .WhenNotNull(this as IHasVersioning, (_, o) => _
            .SetAssemblyVersion(o!.Versioning.AssemblySemVer)
            .SetFileVersion(o.Versioning.AssemblySemFileVer)
            .SetInformationalVersion(o.Versioning.InformationalVersion));

    /// <summary>
    /// Settings for controlling publish behavior.
    /// </summary>
    sealed Configure<DotNetPublishSettings> PublishSettingsBase => _ => _
        .SetConfiguration(Configuration)
        .EnableNoBuild()
        .EnableNoLogo()
        .When(IsServerBuild, _ => _
            .EnableContinuousIntegrationBuild())
        .WhenNotNull(this as IHasGitRepository, (_, o) => _
            .SetRepositoryUrl(o!.GitRepository.HttpsUrl))
        .WhenNotNull(this as IHasVersioning, (_, o) => _
            .SetAssemblyVersion(o!.Versioning.AssemblySemVer)
            .SetFileVersion(o.Versioning.AssemblySemFileVer)
            .SetInformationalVersion(o.Versioning.InformationalVersion));

    /// <summary>
    /// Additional settings for controlling the <c>dotnet build</c> command.
    /// </summary>
    Configure<DotNetBuildSettings> CompileSettings => _ => _;

    /// <summary>
    /// Additional settings for controlling the <c>dotnet publish</c> command.
    /// </summary>
    Configure<DotNetPublishSettings> PublishSettings => _ => _;

    /// <summary>
    /// The publish configurations to build with.
    /// </summary>
    IEnumerable<(Project Project, string Framework)> PublishConfigurations
        => Array.Empty<(Project Project, string Framework)>();

    /// <summary>
    /// The number of projects to publish in parallel when running the <c>dotnet publish</c> command. Defaults to
    /// <c>10</c>.
    /// </summary>
    int PublishDegreeOfParallelism => 10;
}
