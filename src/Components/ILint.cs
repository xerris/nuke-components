using JetBrains.Annotations;
using Nuke.Common;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

[PublicAPI]
public interface ILint : ITools, IHasSolution
{
    IEnumerable<string> ExcludedLintPaths { get; }

    private string ExcludedPathsArgument => ExcludedLintPaths.Any()
        ? $"--exclude {string.Join(' ', ExcludedLintPaths)}"
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
