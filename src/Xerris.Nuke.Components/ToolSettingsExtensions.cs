namespace Xerris.Nuke.Components;

public static class ToolSettingsExtensions
{
    public static T WhenNotNull<T, TObject>(this T settings, TObject obj, Func<T, TObject, T> configurator)
    {
        return obj != null ? configurator.Invoke(settings, obj) : settings;
    }
}
