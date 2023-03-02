using Nuke.Common;
using Nuke.Common.ProjectModel;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

public interface ILint : ITools
{
    [Solution]
    Solution Solution => TryGetValue(() => Solution);

    [Parameter]
    string? LintExclude => TryGetValue(() => LintExclude);

    string ExcludedPathsArgument => string.IsNullOrEmpty(LintExclude)
        ? $"--exclude {string.Join(' ', LintExclude)}"
        : string.Empty;

    Target Lint => _ => _
        .DependsOn(RestoreTools)
        .Executes(() =>
        {
            Log.Information($"Excluded paths: {string.Join(' ', LintExclude)}");

            DotNet($"format whitespace {Solution} " +
                   $"--verify-no-changes {ExcludedPathsArgument}");

            DotNet($"format style {Solution} " +
                   $"--verify-no-changes {ExcludedPathsArgument}");
        });

    Target FixLint => _ => _
        .DependsOn(RestoreTools)
        .Executes(() =>
        {
            DotNet($"format whitespace {Solution} {ExcludedPathsArgument}");

            DotNet($"format style {Solution} {ExcludedPathsArgument}");
        });
}
