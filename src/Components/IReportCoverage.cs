using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.ReportGenerator;

namespace Xerris.Nuke.Components;

/// <summary>
/// Targets and configuration for creation of code coverage reports.
/// <remarks>
/// Requires the <see href="https://github.com/danielpalme/ReportGenerator">ReportGenerator</see> client tool to be
/// installed in the build project, for example:
/// <code>
/// &lt;PackageDownload Include="ReportGenerator" version="[x.y.z]" /&gt;
/// </code>
/// </remarks>
/// </summary>
public interface IReportCoverage : ITest, IHasReports, IHasGitRepository
{
    /// <summary>
    /// Whether or not to generate an HTML coverage report.
    /// </summary>
    bool CreateCoverageHtmlReport { get; }

    /// <summary>
    /// The output directory for coverage reports.
    /// </summary>
    AbsolutePath CoverageReportDirectory => ReportDirectory / "coverage-report";

    /// <summary>
    /// The path to the coverage report archive (.zip).
    /// </summary>
    AbsolutePath CoverageReportArchive => Path.ChangeExtension(CoverageReportDirectory, ".zip");

    /// <summary>
    /// Create code coverage reports.
    /// </summary>
    Target ReportCoverage => _ => _
        .TryTriggeredBy<ITest>(x => x.Test)
        .Consumes(Test)
        .Produces(CoverageReportArchive)
        .Executes(() =>
        {
            if (!CreateCoverageHtmlReport)
                return;

            ReportGeneratorTasks.ReportGenerator(_ => _
                .Apply(ReportGeneratorSettingsBase)
                .Apply(ReportGeneratorSettings));

            CoverageReportDirectory.ZipTo(CoverageReportArchive, fileMode: FileMode.Create);
        });

    /// <summary>
    /// Settings for controlling the generation of code coverage reports.
    /// </summary>
    sealed Configure<ReportGeneratorSettings> ReportGeneratorSettingsBase => _ => _
        .SetReports(TestResultDirectory / "*.xml")
        .SetReportTypes(ReportTypes.HtmlInline)
        .SetTargetDirectory(CoverageReportDirectory)
        .SetFramework("net6.0");

    /// <summary>
    /// Additional settings for controlling the generation of code coverage reports.
    /// </summary>
    Configure<ReportGeneratorSettings> ReportGeneratorSettings => _ => _;
}
