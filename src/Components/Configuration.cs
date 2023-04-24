using System.ComponentModel;
using Nuke.Common.Tooling;

namespace Xerris.Nuke.Components;

/// <summary>
/// The configuration to use for building the project.
/// </summary>
[TypeConverter(typeof(TypeConverter<Configuration>))]
public class Configuration : Enumeration
{
    /// <summary>
    /// Represents the <c>Debug</c> configuration value.
    /// </summary>
    public static Configuration Debug = new() { Value = nameof(Debug) };

    /// <summary>
    /// Represents the <c>Release</c> configuration value.
    /// </summary>
    public static Configuration Release = new() { Value = nameof(Release) };

#pragma warning disable CS1591
    public static implicit operator string(Configuration configuration)
#pragma warning restore CS1591
    {
        return configuration.Value;
    }
}
