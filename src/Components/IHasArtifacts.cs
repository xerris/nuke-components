using Nuke.Common;
using Nuke.Common.IO;

namespace Xerris.Nuke.Components;

public interface IHasArtifacts : INukeBuild
{
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
}
