
using GreenFlux.Application.Groups.Queries.Dtos;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetConnector;

public class GetConnectorQuery : IRequest<ConnectorDto>
{
    public GetConnectorQuery(int groupId, int chargeStationId, int connectorNumber)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        ConnectorNumber = connectorNumber;
    }

    public int GroupId { get; set; }
    public int ChargeStationId { get; set; }
    public int ConnectorNumber { get; set; }
}

