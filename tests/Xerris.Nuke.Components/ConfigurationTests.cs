namespace Xerris.Nuke.Components.Tests;

public class ConfigurationTests
{
    [Fact]
    public void Configuration_outputs_string()
    {
        Configuration.Release.ToString().Should().BeEquivalentTo("Release");

        Configuration.Debug.ToString().Should().BeEquivalentTo("Debug");
    }
}
