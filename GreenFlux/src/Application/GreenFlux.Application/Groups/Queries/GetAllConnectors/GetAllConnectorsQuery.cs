
using GreenFlux.Application.Groups.Queries.Dtos;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetAllConnectors;

public class GetAllConnectorsQuery: IRequest<IEnumerable<ConnectorDto>>
{
    public GetAllConnectorsQuery(int groupId, int chargeStationId)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
    }

    public int GroupId { get; set; }
    public int ChargeStationId { get; set; }
}

