using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

/// <summary>
/// Targets and configuration for restoring dependencies for the solution.
/// </summary>
public interface IRestore : IHasSolution
{
    /// <summary>
    /// Restore dependencies using <c>dotnet restore</c>.
    /// </summary>
    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .Apply(RestoreSettingsBase)
                .Apply(RestoreSettings));
        });

    /// <summary>
    /// Settings for configuring the <c>dotnet restore</c> command.
    /// </summary>
    sealed Configure<DotNetRestoreSettings> RestoreSettingsBase => _ => _
        .SetProjectFile(Solution)
        .SetIgnoreFailedSources(IgnoreFailedSources);

    /// <summary>
    /// Additional settings for configuring the <c>dotnet restore</c> command.
    /// </summary>
    Configure<DotNetRestoreSettings> RestoreSettings => _ => _;

    /// <summary>
    /// Whether or not to ignore failed sources during restore. Defaults to <c>false</c>.
    /// </summary>
    [Parameter("Ignore unreachable sources during " + nameof(Restore))]
    bool IgnoreFailedSources => TryGetValue<bool?>(() => IgnoreFailedSources) ?? false;
}
