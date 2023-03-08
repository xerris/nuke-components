using Nuke.Common;
using Nuke.Common.ProjectModel;

namespace Xerris.Nuke.Components;

public interface IHasSolution : INukeBuild
{
    [Solution]
    [Required]
    Solution Solution => TryGetValue(() => Solution)
        ?? throw new InvalidOperationException("No solution file was found for this build project");
}
