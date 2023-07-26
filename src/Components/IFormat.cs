using Nuke.Common;
using Nuke.Common.IO;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

/// <summary>
/// Provides targets and configuration for verifying and applying code style preferences.
/// </summary>
public interface IFormat : IHasSolution
{
    /// <summary>
    /// Paths to exclude from formatting and formatting verification. Paths must be relative to the build's root
    /// directory (<see cref="NukeBuild.RootDirectory"/>)
    /// </summary>
    IEnumerable<AbsolutePath> ExcludedFormatPaths { get; }

    /// <summary>
    /// Whether or not to run third-party analyzers as part of formatting verification.
    /// </summary>
    bool RunFormatAnalyzers => false;

    private string ExcludedPathsArgument => ExcludedFormatPaths.Any()
        ? $"--exclude {string.Join(' ', ExcludedFormatPaths)}"
        : string.Empty;

    /// <summary>
    /// Verify code style preferences using <c>dotnet format</c>.
    /// </summary>
    Target VerifyFormat => _ => _
        .Description("Verify code formatting for the solution.")
        .TryBefore<ICompile>()
        .Executes(() =>
        {
            DotNet($"format whitespace {Solution.Path} " +
                "--verify-no-changes " +
                ExcludedPathsArgument);

            DotNet($"format style {Solution.Path} " +
                "--verify-no-changes " +
                ExcludedPathsArgument);
            if (RunFormatAnalyzers)
            {
                DotNet($"format analyzers {Solution.Path} " +
                    "--verify-no-changes " +
                    ExcludedPathsArgument);
            }
        });

    /// <summary>
    /// Apply code style preferences using <c>dotnet format</c>.
    /// </summary>
    Target Format => _ => _
        .Executes(() =>
        {
            DotNet($"format whitespace {Solution.Path} " +
                $"{ExcludedPathsArgument}");

            DotNet($"format style {Solution.Path} " +
                $"{ExcludedPathsArgument}");

            if (RunFormatAnalyzers)
            {
                DotNet($"format analyzers {Solution.Path} " +
                    $"{ExcludedPathsArgument}");
            }
        });
}
