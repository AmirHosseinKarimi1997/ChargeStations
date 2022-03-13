
using MediatR;

namespace GreenFlux.Application.Groups.Commands.AddConnectorToChargeStation;

public class AddConnectorToChargeStationCommand : IRequest<bool>
{
    public AddConnectorToChargeStationCommand(int groupId, int chargeStationId, int connectorNumber, uint maxCurrentInAmps)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        ConnectorNumber = connectorNumber;
        MaxCurrentInAmps = maxCurrentInAmps;
    }

    public int GroupId { get; set; }

    public int ChargeStationId { get; set; }

    public int ConnectorNumber { get; set; }

    public uint MaxCurrentInAmps { get; set; }
}

