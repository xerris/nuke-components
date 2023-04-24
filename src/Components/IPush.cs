using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides targets and configuration for pushing NuGet packages.
/// </summary>
public interface IPush : IPack
{
    /// <summary>
    /// Specifies the NuGet server URL. Defaults to the public NuGet feed.
    /// </summary>
    [Parameter]
    string NuGetSource => TryGetValue(() => NuGetSource) ?? "https://api.nuget.org/v3/index.json";

    /// <summary>
    /// The NuGet API key.
    /// </summary>
    [Parameter]
    [Secret]
    string NuGetApiKey => TryGetValue(() => NuGetApiKey)!;

    /// <summary>
    /// Publish NuGet packages using <c>dotnet nuget push</c>.
    /// </summary>
    Target Push => _ => _
        .Description("Publish packages to NuGet")
        .DependsOn(Pack)
        .Requires(() => NuGetApiKey)
        .Executes(() =>
        {
            DotNetNuGetPush(_ => _
                    .Apply(PushSettingsBase)
                    .Apply(PushSettings)
                    .CombineWith(PushPackageFiles, (_, v) => _
                        .SetTargetPath(v))
                    .Apply(PackagePushSettings),
                PushDegreeOfParallelism,
                PushCompleteOnFailure);
        });

    /// <summary>
    /// Settings for controlling the behaviour of the <c>dotnet nuget push</c> command.
    /// </summary>
    sealed Configure<DotNetNuGetPushSettings> PushSettingsBase => _ => _
        .SetSource(NuGetSource)
        .SetApiKey(NuGetApiKey);

    /// <summary>
    /// Additional settings for controlling the behaviour of the <c>dotnet nuget push</c> command.
    /// </summary>
    Configure<DotNetNuGetPushSettings> PushSettings => _ => _;

    /// <summary>
    /// Additional settings for controlling the behaviour packages pushed to NuGet.
    /// </summary>
    Configure<DotNetNuGetPushSettings> PackagePushSettings => _ => _;

    /// <summary>
    /// The NuGet packages to push. Defaults to all files with the <c>.nupkg</c> extension under
    /// <see cref="IPack.PackagesDirectory"/>
    /// </summary>
    IEnumerable<AbsolutePath> PushPackageFiles => PackagesDirectory.GlobFiles("*.nupkg");

    /// <summary>
    /// Whether or not to complete the push target, even if it fails. Defaults to <c>true</c>;
    /// </summary>
    bool PushCompleteOnFailure => true;

    /// <summary>
    /// The degree of parallelism to use when pushing packages. Defaults to <c>5</c>.
    /// </summary>
    int PushDegreeOfParallelism => 5;
}
