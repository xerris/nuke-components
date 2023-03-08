using Nuke.Common;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

public interface ILint : ITools, IHasSolution
{
    [Parameter]
    string? LintExclude => TryGetValue(() => LintExclude);

    // TODO: Exclusions as required property?

    private string ExcludedPathsArgument => !string.IsNullOrWhiteSpace(LintExclude)
        ? $"--exclude {string.Join(' ', LintExclude)}"
        : string.Empty;

    Target Lint => _ => _
        .DependsOn(RestoreTools)
        .Executes(() =>
        {
            DotNet($"format whitespace {Solution} " +
                "--verify-no-changes " +
                $"{ExcludedPathsArgument}");

            DotNet($"format style {Solution} " +
                "--verify-no-changes " +
                $"{ExcludedPathsArgument}");
        });

    Target FixLint => _ => _
        .DependsOn(RestoreTools)
        .Executes(() =>
        {
            DotNet($"format whitespace {Solution} " +
                $"{ExcludedPathsArgument}");

            DotNet($"format style {Solution} " +
                $"{ExcludedPathsArgument}");
        });
}
