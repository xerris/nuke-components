using Nuke.Common;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

public interface IFormat : IHasSolution
{
    /// <summary>
    /// Paths to exclude from formatting and formatting verification.
    /// </summary>
    IEnumerable<string> ExcludedFormatPaths { get; }

    /// <summary>
    /// Whether or not to run third-party analyzers as part of formatting verification.
    /// </summary>
    bool RunFormatAnalyzers => false;

    private string ExcludedPathsArgument => ExcludedFormatPaths.Any()
        ? $"--exclude {string.Join(' ', ExcludedFormatPaths)}"
        : string.Empty;

    Target VerifyFormat => _ => _
        .TryBefore<ICompile>()
        .Executes(() =>
        {
            DotNet($"format whitespace {Solution} " +
                "--verify-no-changes " +
                $"{ExcludedPathsArgument}");

            DotNet($"format style {Solution} " +
                "--verify-no-changes " +
                $"{ExcludedPathsArgument}");

            if (RunFormatAnalyzers)
            {
                DotNet($"format analyzers {Solution} " +
                    "--verify-no-changes " +
                    $"{ExcludedPathsArgument}");
            }
        });

    Target Format => _ => _
        .Executes(() =>
        {
            DotNet($"format whitespace {Solution} " +
                $"{ExcludedPathsArgument}");

            DotNet($"format style {Solution} " +
                $"{ExcludedPathsArgument}");

            if (RunFormatAnalyzers)
            {
                DotNet($"format analyzers {Solution} " +
                    $"{ExcludedPathsArgument}");
            }
        });
}
