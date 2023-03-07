using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Git;

namespace Xerris.Nuke.Components;

[PublicAPI]
public interface IHasGitRepository : INukeBuild
{
    [GitRepository][Required] GitRepository GitRepository => TryGetValue(() => GitRepository);
}