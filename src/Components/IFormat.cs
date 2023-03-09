using Nuke.Common;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

public interface IFormat : IHasSolution
{
    IEnumerable<string> ExcludedFormatPaths { get; }

    private string ExcludedPathsArgument => ExcludedFormatPaths.Any()
        ? $"--exclude {string.Join(' ', ExcludedFormatPaths)}"
        : string.Empty;

    Target VerifyFormat => _ => _
        .Executes(() =>
        {
            // No fluent API support for this tool yet
            DotNet($"format whitespace {Solution} " +
                "--verify-no-changes " +
                $"{ExcludedPathsArgument}");

            DotNet($"format style {Solution} " +
                "--verify-no-changes " +
                $"{ExcludedPathsArgument}");

            // todo: analyzers?
        });

    Target Format => _ => _
        .Executes(() =>
        {
            // No fluent API support for this tool yet
            DotNet($"format whitespace {Solution} " +
                $"{ExcludedPathsArgument}");

            DotNet($"format style {Solution} " +
                $"{ExcludedPathsArgument}");
        });
}
