using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Xerris.Nuke.Components;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild, IFormat, ICompile
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => ((ICompile) x).Compile);

    // The path to the solution file must be explicitly specified because this project is nested below the main build
    // project for the repository. Normally, you don't have to specify a relative path to the solution file.
    [Solution("./Xerris.Nuke.Samples.Format.sln")]
    readonly Solution Solution;
    Solution IHasSolution.Solution => Solution;

    Target Clean => _ => _
        .Before<IRestore>()
        .Executes(() =>
        {
            DotNetClean(_ => _
                .SetProject(Solution));
        });

    public IEnumerable<string> ExcludedFormatPaths => Enumerable.Empty<string>();

    Target ICompile.Compile => _ => _
        .Inherit<ICompile>()
        .DependsOn(Clean)
        .DependsOn<IFormat>(x => x.VerifyFormat);
}
