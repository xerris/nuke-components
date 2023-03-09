using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.ReportGenerator;

namespace Xerris.Nuke.Components;

public interface IReportCoverage : ITest, IHasReports, IHasGitRepository
{
    bool CreateCoverageHtmlReport { get; }

    string CoverageReportDirectory => ReportDirectory / "coverage-report";

    string CoverageReportArchive => Path.ChangeExtension(CoverageReportDirectory, ".zip");

    Target ReportCoverage => _ => _
        .DependsOn(Test)
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
                CoverageReportDirectory,
                CoverageReportArchive,
                fileMode: FileMode.Create);
        });

    sealed Configure<ReportGeneratorSettings> ReportGeneratorSettingsBase => _ => _
        .SetReports(TestResultDirectory / "*.xml")
        .SetReportTypes(ReportTypes.HtmlInline)
        .SetTargetDirectory(CoverageReportDirectory)
        .SetFramework("net7.0");

    Configure<ReportGeneratorSettings> ReportGeneratorSettings => _ => _;
}
