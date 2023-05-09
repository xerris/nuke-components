using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides targets and configuration for cleaning the current solution.
/// </summary>
public interface IClean : IHasSolution
{
    /// <summary>
    /// Whether or not to remove items in the artifacts directory when cleaning.
    /// </summary>
    bool CleanArtifactsDirectory => true;

    /// <summary>
    /// Clean the current solution with <c>dotnet clean</c>.
    /// </summary>
    Target Clean => _ => _
        .Description("Clean the solution and build artifacts")
        .Before<IRestore>()
        .Executes(() =>
        {
            DotNetClean(_ => _
                .SetProject(Solution));

            if (CleanArtifactsDirectory && this is IHasArtifacts hasArtifacts)
                hasArtifacts.ArtifactsDirectory.CreateOrCleanDirectory();
        });
}
