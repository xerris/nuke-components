using Nuke.Common;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Xerris.Nuke.Components;

public interface ITools : INukeBuild
{
    Target RestoreTools => _ => _
        .Executes(() =>
        {
            DotNetToolRestore(_ => _);
        });
}
