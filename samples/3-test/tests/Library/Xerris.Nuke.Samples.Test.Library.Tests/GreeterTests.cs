namespace Xerris.Nuke.Samples.Test.Library.Tests;

public class GreeterTests
{
    [Fact]
    public void Greeter_says_hello()
    {
        var greeter = new Greeter();

        const string name = "NUKE Build";

        var greeting = greeter.Greet(name);

        greeting.Should().Contain("Hello");
        greeting.Should().Contain(name);
    }
}
