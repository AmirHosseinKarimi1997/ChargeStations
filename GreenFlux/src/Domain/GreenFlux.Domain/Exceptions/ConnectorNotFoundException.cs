
namespace GreenFlux.Domain.Exceptions;

public class ConnectorNotFoundException : DomainBaseException
{
    public ConnectorNotFoundException()
        : base()
    {
    }

    public ConnectorNotFoundException(string message)
        : base(message)
    {
    }

    public ConnectorNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ConnectorNotFoundException(object chargeStationId, object connectorNumber)
        : base($"Connector with chargeStationId  ({chargeStationId}) and connectorNumber ({connectorNumber}) was not found.")
    {
    }
}

