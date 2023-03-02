using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.ProjectModel;

namespace Xerris.Nuke.Components;

[PublicAPI]
public interface IHasSolution : INukeBuild
{
    [Solution]
    [Required]
    Solution Solution => TryGetValue(() => Solution)
                         ?? throw new InvalidOperationException("A solution file is required");
}