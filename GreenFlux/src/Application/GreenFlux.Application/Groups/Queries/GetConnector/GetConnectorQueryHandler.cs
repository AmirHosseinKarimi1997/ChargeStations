
using GreenFlux.Application.Common;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Interfaces;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetConnector;

public class GetConnectorQueryHandler : IRequestHandler<GetConnectorQuery, ConnectorDto>
{
    private readonly IGroupQueries _query;

    public GetConnectorQueryHandler(IGroupQueries query)
    {
        _query = query;
    }

    public async Task<ConnectorDto> Handle(GetConnectorQuery request, CancellationToken cancellationToken)
    {
        var data = await _query.GetConnectorAsync(request.GroupId, request.ChargeStationId, request.ConnectorNumber);

        if (data == null)
            return null;

        return Mapper.MapToDto(data);
    }
}

