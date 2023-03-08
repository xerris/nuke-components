using Nuke.Common;
using Nuke.Common.Git;

namespace Xerris.Nuke.Components;

public interface IHasGitRepository : INukeBuild
{
    [Required]
    [GitRepository]
    GitRepository GitRepository => TryGetValue(() => GitRepository)!;
}
