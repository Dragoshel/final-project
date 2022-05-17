namespace FinalProject.Repos;

public class FinalProjectException : Exception
{
    public FinalProjectException()
    {
    }

    public FinalProjectException(string message)
        : base(message)
    {
    }

    public FinalProjectException(string message, Exception inner)
        : base(message, inner)
    {
    }
}