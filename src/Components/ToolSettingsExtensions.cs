namespace Xerris.Nuke.Components;

/// <summary>
/// Extension methods for tool execution configuration.
/// </summary>
public static class ToolSettingsExtensions
{
    /// <summary>
    /// Invokes the specified configuration when the subject is not null.
    /// </summary>
    /// <typeparam name="T">The settings type.</typeparam>
    /// <typeparam name="TObject">The object type.</typeparam>
    /// <param name="settings">The settings being configured.</param>
    /// <param name="obj">The object to check.</param>
    /// <param name="configurator">The configuration delegate.</param>
    /// <returns>The configuration object.</returns>
    public static T WhenNotNull<T, TObject>(this T settings, TObject obj, Func<T, TObject, T> configurator)
    {
        return obj is not null ? configurator.Invoke(settings, obj) : settings;
    }
}
