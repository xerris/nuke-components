using Nuke.Common.CI.GitHubActions;
using Xerris.Nuke.Components;

[GitHubActions(
    "continuous",
    GitHubActionsImage.WindowsLatest,
    GitHubActionsImage.UbuntuLatest,
    GitHubActionsImage.MacOsLatest,
    FetchDepth = 0,
    OnPushBranchesIgnore = new[] { "main", "release/v*" },
    OnPullRequestBranches = new[] { "main" },
    PublishArtifacts = true,
    InvokedTargets = new[] { nameof(ITest.Test), nameof(IPack.Pack) },
    CacheKeyFiles = new[] { "global.json", "source/**/*.csproj" },
    EnableGitHubToken = true)]
// TODO: Package signing
[GitHubActions(
    "release",
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    OnPushBranches = new[] { "main", "release/v*" },
    OnPushTags = new[] { "v*" },
    PublishArtifacts = true,
    InvokedTargets = new[] { nameof(ITest.Test), nameof(IPack.Pack), nameof(IPush.Push) },
    CacheKeyFiles = new[] { "global.json", "source/**/*.csproj" },
    EnableGitHubToken = true)]
partial class Build
{
}
