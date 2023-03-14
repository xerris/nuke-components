using Nuke.Common.CI.GitHubActions;
using Xerris.Nuke.Components;

[GitHubActions(
    "continuous",
    GitHubActionsImage.WindowsLatest,
    GitHubActionsImage.UbuntuLatest,
    GitHubActionsImage.MacOsLatest,
    FetchDepth = 0,
    OnPullRequestBranches = new[] { "main" },
    OnPushBranches = new[] { "main", "release/v*" },
    PublishArtifacts = true,
    InvokedTargets = new[] { nameof(ITest.Test) },
    CacheKeyFiles = new[] { "global.json", "src/**/*.csproj" })]
// TODO: Package signing
[GitHubActions(
    "release",
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    OnPushTags = new[] { "v*" },
    PublishArtifacts = true,
    InvokedTargets = new[] { nameof(ITest.Test), nameof(IPack.Pack), nameof(IPush.Push) },
    CacheKeyFiles = new[] { "global.json", "src/**/*.csproj" },
    ImportSecrets = new[] { nameof(IPush.NuGetApiKey) })]
partial class Build
{
}
