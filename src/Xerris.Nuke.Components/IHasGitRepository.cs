using Nuke.Common;
using Nuke.Common.Git;

namespace Xerris.Nuke.Components;

public interface IHasGitRepository : INukeBuild
{
    [Required]
    [GitRepository]
    GitRepository GitRepository => TryGetValue(() => GitRepository) ??
        throw new InvalidOperationException("This build project does not seem to be part of a git repository");
}
