
using GreenFlux.Application.Groups.Queries.Dtos;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetAllChargeStations;

public class GetAllChargeStationsQuery : IRequest<IEnumerable<ChargeStationDto>>
{
    public GetAllChargeStationsQuery(int groupId)
    {
        GroupId = groupId;
    }

    public int GroupId { get; private set; }
}

