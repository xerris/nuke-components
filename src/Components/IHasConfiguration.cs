using Nuke.Common;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides parameters for setting the build configuration.
/// </summary>
public interface IHasConfiguration : INukeBuild
{
    /// <summary>
    /// The build configuration. Defaults to "Debug" when the build is running locally, and"Release" when the build is
    /// running on a build server.
    /// </summary>
    [Parameter]
    Configuration Configuration => TryGetValue(() => Configuration) ??
        (IsLocalBuild ? Configuration.Debug : Configuration.Release);
}
