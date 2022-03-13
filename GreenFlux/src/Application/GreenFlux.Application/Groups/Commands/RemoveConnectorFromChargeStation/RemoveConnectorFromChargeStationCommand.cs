
using MediatR;

namespace GreenFlux.Application.Groups.Commands.RemoveConnectorFromChargeStation;

public class RemoveConnectorFromChargeStationCommand : IRequest<bool>
{
    public RemoveConnectorFromChargeStationCommand(int groupId, int chargeStationId, int connectorNumber)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        ConnectorNumber = connectorNumber;
    }

    public int GroupId { get; set; }

    public int ChargeStationId { get; set; }

    public int ConnectorNumber { get; set; }
}

