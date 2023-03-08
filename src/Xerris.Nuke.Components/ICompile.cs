using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

namespace Xerris.Nuke.Components;

public interface ICompile : IRestore, IHasConfiguration
{
    Target Compile => _ => _
        .DependsOn(Restore)
        .WhenSkipped(DependencyBehavior.Skip)
        .Executes(() =>
        {
            // TODO: Versioning
            //ReportSummary(_ => _
            //    .WhenNotNull(this as IHazGitVersion, (_, o) => _
            //        .AddPair("Version", o.Versioning.FullSemVer)));

            DotNetTasks.DotNetBuild(_ => _
                .Apply(CompileSettingsBase)
                .Apply(CompileSettings));

            DotNetTasks.DotNetPublish(_ => _
                    .Apply(PublishSettingsBase)
                    .Apply(PublishSettings)
                    .CombineWith(PublishConfigurations, (_, v) => _.SetProject((string) v.Project)
                        .SetFramework(v.Framework)),
                PublishDegreeOfParallelism);
        });

    sealed Configure<DotNetBuildSettings> CompileSettingsBase => _ => _
        .SetProjectFile(Solution)
        .SetConfiguration(Configuration)
        .When(IsServerBuild, _ => _
            .EnableContinuousIntegrationBuild())
        .SetNoRestore(SucceededTargets.Contains(Restore))
        .WhenNotNull(this as IHasGitRepository, (_, o) => _
            .SetRepositoryUrl(o!.GitRepository.HttpsUrl));
    // TODO: Versioning
    //.WhenNotNull(this as IHazGitVersion, (_, o) => _
    //    .SetAssemblyVersion(o.Versioning.AssemblySemVer)
    //    .SetFileVersion(o.Versioning.AssemblySemFileVer)
    //    .SetInformationalVersion(o.Versioning.InformationalVersion)));

    sealed Configure<DotNetPublishSettings> PublishSettingsBase => _ => _
        .SetConfiguration(Configuration)
        .EnableNoBuild()
        .EnableNoLogo()
        .When(IsServerBuild, _ => _
            .EnableContinuousIntegrationBuild())
        .WhenNotNull(this as IHasGitRepository, (_, o) => _
            .SetRepositoryUrl(o!.GitRepository.HttpsUrl));
    // TODO: Versioning
    //.WhenNotNull(this as IHazGitVersion, (_, o) => _
    //    .SetAssemblyVersion(o.Versioning.AssemblySemVer)
    //    .SetFileVersion(o.Versioning.AssemblySemFileVer)
    //    .SetInformationalVersion(o.Versioning.InformationalVersion)));

    Configure<DotNetBuildSettings> CompileSettings => _ => _;
    Configure<DotNetPublishSettings> PublishSettings => _ => _;

    IEnumerable<(Project Project, string Framework)> PublishConfigurations
        => Array.Empty<(Project Project, string Framework)>();

    int PublishDegreeOfParallelism => 10;
}
