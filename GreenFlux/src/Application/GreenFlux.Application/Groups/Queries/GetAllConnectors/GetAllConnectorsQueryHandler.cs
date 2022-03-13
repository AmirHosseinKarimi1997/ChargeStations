
using GreenFlux.Application.Common;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Interfaces;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetAllConnectors;

public class GetAllConnectorsQueryHandler : IRequestHandler<GetAllConnectorsQuery, IEnumerable<ConnectorDto>>
{
    private readonly IGroupQueries _query;

    public GetAllConnectorsQueryHandler(IGroupQueries query)
    {
        _query = query;
    }

    public async Task<IEnumerable<ConnectorDto>> Handle(GetAllConnectorsQuery request, CancellationToken cancellationToken)
    {
        var data = await _query.GetAllConnectorsAsync(request.GroupId, request.ChargeStationId);

        if (data == null)
            return null;

        var result = data.Select(x => Mapper.MapToDto(x));
        return result;
    }
}

