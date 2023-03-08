using Nuke.Common;
using Nuke.Common.Tools.GitVersion;

namespace Xerris.Nuke.Components;

public interface IHasVersioning : INukeBuild
{
    [Required]
    [GitVersion(NoFetch = true)]
    GitVersion Versioning => TryGetValue(() => Versioning)!;
}
