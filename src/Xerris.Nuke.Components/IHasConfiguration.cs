using JetBrains.Annotations;
using Nuke.Common;

namespace Xerris.Nuke.Components;

[PublicAPI]
public interface IHasConfiguration : INukeBuild
{
    [Parameter]
    Configuration Configuration => TryGetValue(() => Configuration) ??
                                   (IsLocalBuild ? Configuration.Debug : Configuration.Release);
}