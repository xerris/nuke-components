using Nuke.Common;
using Nuke.Common.Tools.GitVersion;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides properties for accessing versioning information calculated with
/// <see href="https://gitversion.net/">GitVersion</see>.
/// </summary>
/// <remarks>
/// Requires the <see href="https://gitversion.net/docs/usage/cli/installation">GitVersion</see> client tool to be
/// installed in the build project, for example:
/// <code>
/// &lt;PackageDownload Include="GitVersion.Tool" version="[x.y.z]" /&gt;
/// </code>
/// </remarks>
public interface IHasVersioning : INukeBuild
{
    /// <summary>
    /// The versioning information for the current commit or tag.
    /// </summary>
    [Required]
    [GitVersion(NoFetch = true)]
    GitVersion Versioning => TryGetValue(() => Versioning)!;
}
