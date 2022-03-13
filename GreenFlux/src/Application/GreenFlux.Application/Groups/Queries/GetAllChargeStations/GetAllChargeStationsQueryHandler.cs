
using GreenFlux.Application.Common;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Interfaces;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetAllChargeStations;

public class GetAllChargeStationsQueryHandler : IRequestHandler<GetAllChargeStationsQuery, IEnumerable<ChargeStationDto>>
{
    private readonly IGroupQueries _query;

    public GetAllChargeStationsQueryHandler(IGroupQueries query)
    {
        _query = query;
    }

    public async Task<IEnumerable<ChargeStationDto>> Handle(GetAllChargeStationsQuery request, CancellationToken cancellationToken)
    {
        var data = await _query.GetAllChargeStationsAsync(request.GroupId);

        if (data == null)
            return default(IEnumerable<ChargeStationDto>);

        var result = data.Select(x => Mapper.MapToDto(x));
        return result;
    }
}

