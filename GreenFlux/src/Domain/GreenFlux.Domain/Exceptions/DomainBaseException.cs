
namespace GreenFlux.Domain.Exceptions;

public class DomainBaseException : Exception
{
    public DomainBaseException()
        : base()
    {
    }

    public DomainBaseException(string message)
        : base(message)
    {
    }

    public DomainBaseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

