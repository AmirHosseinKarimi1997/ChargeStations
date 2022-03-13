
using GreenFlux.Application.Groups.Queries.Dtos;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetChargeStation;

public class GetChargeStationQuery: IRequest<ChargeStationDto>
{
    public GetChargeStationQuery(int groupId, int id)
    {
        GroupId = groupId;
        Id = id;
    }

    public int GroupId { get; private set; }
    
    public int Id { get; private set; }
}

