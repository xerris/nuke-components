using Nuke.Common.IO;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides properties for controlling build report output.
/// </summary>
public interface IHasReports : IHasArtifacts
{
    /// <summary>
    /// The output directory for reports.
    /// </summary>
    AbsolutePath ReportDirectory => ArtifactsDirectory / "reports";
}
