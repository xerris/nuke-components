using Nuke.Common.IO;

namespace Xerris.Nuke.Components;

public interface IHasReports : IHasArtifacts
{
    AbsolutePath ReportDirectory => ArtifactsDirectory / "reports";
}
