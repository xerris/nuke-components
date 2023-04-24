using Nuke.Common;

namespace Xerris.Nuke.Components;

/// <summary>
/// Extension methods for NUKE <see href="https://nuke.build/docs/sharing/build-components/">build components</see>.
/// </summary>
public static class ComponentExtensions
{
    /// <summary>
    /// Get a reference to the current build definition as a component type.
    /// </summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    /// <param name="build">The build definition.</param>
    /// <returns></returns>
    public static TComponent FromComponent<TComponent>(this INukeBuild build)
        where TComponent : INukeBuild
    {
        return (TComponent) build;
    }
}
