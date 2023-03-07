using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.ReportGenerator;

namespace Xerris.Nuke.Components;

[PublicAPI]
public interface IReportCoverage : ITest, IHasReports, IHasGitRepository
{
    bool CreateCoverageHtmlReport { get; }

    string CoverageReportDirectory => ReportDirectory / "coverage-report";

    string CoverageReportArchive => Path.ChangeExtension(CoverageReportDirectory, ".zip");

    Target ReportCoverage => _ => _
        .DependsOn(Test)
        .TryAfter<ITest>()
        .Consumes(Test)
        .Produces(CoverageReportArchive)
        .Executes(() =>
        {
            if (!CreateCoverageHtmlReport)
                return;

            ReportGeneratorTasks.ReportGenerator(_ => _
                .Apply(ReportGeneratorSettingsBase)
                .Apply(ReportGeneratorSettings));

            CompressionTasks.CompressZip(
                directory: CoverageReportDirectory,
                archiveFile: CoverageReportArchive,
                fileMode: FileMode.Create);
        });

    sealed Configure<ReportGeneratorSettings> ReportGeneratorSettingsBase => _ => _
        .SetReports(TestResultDirectory / "*.xml")
        .SetReportTypes(ReportTypes.HtmlInline)
        .SetTargetDirectory(CoverageReportDirectory)
        .SetFramework("net7.0");

    Configure<ReportGeneratorSettings> ReportGeneratorSettings => _ => _;
}