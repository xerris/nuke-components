using Nuke.Common;
using Nuke.Common.Git;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides properties for accessing information about the current Git repository.
/// </summary>
public interface IHasGitRepository : INukeBuild
{
    /// <summary>
    /// Gets information about the current Git repository.
    /// </summary>
    [Required]
    [GitRepository]
    GitRepository GitRepository => TryGetValue(() => GitRepository)!;
}
