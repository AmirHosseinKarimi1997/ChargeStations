
using GreenFlux.Domain.Common;

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

    public void SetMaxCurrentInAmps(uint currentAmps)
    {
        var sumCurrentAmps = this.ChargeStation.Group.CalculateMaxCurrentSumInAmps();

        if (Id > 0)
            sumCurrentAmps -= MaxCurrentInAmps;

        if (sumCurrentAmps + currentAmps > this.ChargeStation.Group.CapacityInAmps)
            throw new Exception();

        this.MaxCurrentInAmps = currentAmps;
    }

    public void SetConnectorNumber(int connectorNumber)
    {
        //5 from config
        if (connectorNumber > 5)
            throw new Exception();

        var isAlreadyExist = this.ChargeStation.Connectors.Any(x => x.ConnectorNumber == connectorNumber);

        if (isAlreadyExist)
            throw new Exception();

        this.ConnectorNumber = connectorNumber;
    }
}

