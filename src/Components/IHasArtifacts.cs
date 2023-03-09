using Nuke.Common;
using Nuke.Common.IO;

namespace Xerris.Nuke.Components;

public interface IHasArtifacts : INukeBuild
{
    /// <summary>
    /// The output directory for build artifacts.
    /// </summary>
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
}
