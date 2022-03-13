
namespace GreenFlux.Domain.Exceptions;
public class ConnectorNumberIsNotValidException : DomainBaseException
{
    public ConnectorNumberIsNotValidException()
        : base($"connector number is not valid. it should be between 0 and {5}.") //5 from config
    {
    }

}

