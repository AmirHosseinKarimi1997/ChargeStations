
namespace GreenFlux.Domain.Exceptions;
public class ChargeStationNotFoundException : DomainBaseException
{
    public ChargeStationNotFoundException()
        : base()
    {
    }

    public ChargeStationNotFoundException(string message)
        : base(message)
    {
    }

    public ChargeStationNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ChargeStationNotFoundException(object groupId, object chargeStationId)
        : base($"ChargeStation with groupId  ({groupId}) and chargeStationId ({chargeStationId}) was not found.")
    {
    }
}

