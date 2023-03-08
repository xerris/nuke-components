using Nuke.Common;

namespace Xerris.Nuke.Components;

public interface IHasConfiguration : INukeBuild
{
    [Parameter]
    Configuration Configuration => TryGetValue(() => Configuration) ??
        (IsLocalBuild ? Configuration.Debug : Configuration.Release);
}
