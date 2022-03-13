
using FluentValidation;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Domain.Common;
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

public class GetConnectorQueryValidator : AbstractValidator<GetConnectorQuery>
{
    public GetConnectorQueryValidator()
    {

        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.ChargeStationId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.ConnectorNumber)
        .InclusiveBetween(0, ConfigHelper.MaxNumberOfConnectorsAttachedToChargeStations)
        .NotEmpty();
    }
}
