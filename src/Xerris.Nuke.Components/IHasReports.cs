using JetBrains.Annotations;
using Nuke.Common.IO;

namespace Xerris.Nuke.Components;

[PublicAPI]
public interface IHasReports : IHasArtifacts
{
    AbsolutePath ReportDirectory => ArtifactsDirectory / "reports";
}