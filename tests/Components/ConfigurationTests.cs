namespace Xerris.Nuke.Components.Tests;

public class ConfigurationTests
{
    // This trivial test exists primarily to offer a hook for test reporting components.
    [Fact]
    public void Configuration_outputs_string()
    {
        Configuration.Release.ToString().Should().BeEquivalentTo("Release");

        Configuration.Debug.ToString().Should().BeEquivalentTo("Debug");
    }
}
