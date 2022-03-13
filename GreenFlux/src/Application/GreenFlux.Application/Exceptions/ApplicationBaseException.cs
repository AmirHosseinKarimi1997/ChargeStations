
namespace GreenFlux.Application.Exceptions;

public class ApplicationBaseException : Exception
{
    public ApplicationBaseException()
    : base()
    {
    }

    public ApplicationBaseException(string message)
        : base(message)
    {
    }

    public ApplicationBaseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

