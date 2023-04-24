using Nuke.Common;
using Nuke.Common.IO;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides properties for controlling build artifact output.
/// </summary>
public interface IHasArtifacts : INukeBuild
{
    /// <summary>
    /// The output directory for build artifacts.
    /// </summary>
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
}
