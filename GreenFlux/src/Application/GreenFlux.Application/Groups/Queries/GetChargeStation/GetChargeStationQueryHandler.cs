
using GreenFlux.Application.Common;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Interfaces;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetChargeStation;

public class GetChargeStationQueryHandler : IRequestHandler<GetChargeStationQuery, ChargeStationDto>
{
    private readonly IGroupQueries _query;

    public GetChargeStationQueryHandler(IGroupQueries query)
    {
        _query = query;
    }

    public async Task<ChargeStationDto> Handle(GetChargeStationQuery request, CancellationToken cancellationToken)
    {
        var data = await _query.GetChargeStationAsync(request.GroupId, request.Id);

        if (data == null)
            return default(ChargeStationDto);

        return Mapper.MapToDto(data);
    }
}

