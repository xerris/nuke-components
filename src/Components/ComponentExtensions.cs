using Nuke.Common;

namespace Xerris.Nuke.Components;

public static class ComponentExtensions
{
    public static TComponent FromComponent<TComponent>(this INukeBuild build)
        where TComponent : INukeBuild
    {
        return (TComponent) build;
    }
}
