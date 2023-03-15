using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

public interface IClean : IHasSolution
{
    /// <summary>
    /// Whether or not to remove items in the artifacts directory when cleaning.
    /// </summary>
    bool CleanArtifactsDirectory => true;

    Target Clean => _ => _
        .Before<IRestore>()
        .Executes(() =>
        {
            DotNetClean(_ => _
                .SetProject(Solution));

            if (CleanArtifactsDirectory && this is IHasArtifacts hasArtifacts)
                EnsureCleanDirectory(hasArtifacts.ArtifactsDirectory);
        });
}
