using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;

namespace Xerris.Nuke.Components;

[PublicAPI]
public interface IHasArtifacts : INukeBuild
{
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
}