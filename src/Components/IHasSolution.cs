using Nuke.Common;
using Nuke.Common.ProjectModel;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides properties for accessing information about the solution being built.
/// </summary>
public interface IHasSolution : INukeBuild
{
    /// <summary>
    /// Gets a representation of the solution being built.
    /// </summary>
    [Solution]
    [Required]
    Solution Solution => TryGetValue(() => Solution)!;
}
