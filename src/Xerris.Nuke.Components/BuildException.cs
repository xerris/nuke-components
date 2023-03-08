namespace Xerris.Nuke.Components;

public class BuildException : Exception
{
    public BuildException(string? message)
        : base(message)
    {
    }

    public BuildException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
