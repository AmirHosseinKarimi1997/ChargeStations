
using MediatR;

namespace GreenFlux.Application.Groups.Commands.SetConnectorMaxCurrent;

public class SetConnectorMaxCurrentCommand : IRequest<bool>
{
    public SetConnectorMaxCurrentCommand(int groupId, int chargeStationId, int connectorNumber, uint maxCurrnetInAmps)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        ConnectorNumber = connectorNumber;
        MaxCurrnetInAmps = maxCurrnetInAmps;
    }

    public int GroupId { get; private set; }
    public int ChargeStationId { get; private set; }
    public int ConnectorNumber { get; private set; }
    public uint MaxCurrnetInAmps { get; private set; }
}

