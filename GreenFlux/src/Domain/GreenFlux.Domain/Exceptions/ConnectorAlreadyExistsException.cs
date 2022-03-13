
namespace GreenFlux.Domain.Exceptions;
public class ConnectorAlreadyExistsException : DomainBaseException
{
    public ConnectorAlreadyExistsException()
        : base("connector already exists in this ChargeStation.")
    {
    }

}

