using Nuke.Common.IO;

namespace Xerris.Nuke.Components;

public interface IHasReports : IHasArtifacts
{
    /// <summary>
    /// The output directory for reports.
    /// </summary>
    AbsolutePath ReportDirectory => ArtifactsDirectory / "reports";
}
