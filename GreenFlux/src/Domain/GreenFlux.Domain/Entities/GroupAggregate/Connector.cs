
using GreenFlux.Domain.Common;
using GreenFlux.Domain.Exceptions;

namespace GreenFlux.Domain.Entities.GroupAggregate;

public class Connector: AuditableEntity
{
    public Connector(int connectorNumber, uint maxCurrentInAmps)
    {
        SetConnectorNumber(connectorNumber);
        SetMaxCurrentInAmps(maxCurrentInAmps);
    }

    public int Id { get; private set; }

    public int ConnectorNumber { get; private set; }

    public int ChargeStationId { get; private set; }

    public uint MaxCurrentInAmps { get; private set; }

    public ChargeStation ChargeStation { get; private set; } = null!;

    internal void SetMaxCurrentInAmps(uint maxCurrentAmps)
    {
        this.MaxCurrentInAmps = maxCurrentAmps;
    }

    internal void SetConnectorNumber(int connectorNumber)
    {
        if (connectorNumber > ConfigHelper.MaxNumberOfConnectorsAttachedToChargeStations)
            throw new ConnectorNumberIsNotValidException();

        this.ConnectorNumber = connectorNumber;
    }
}

