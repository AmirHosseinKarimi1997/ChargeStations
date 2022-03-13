
using FluentValidation;
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

public class GetAllConnectorsQueryValidator : AbstractValidator<GetAllConnectorsQuery>
{
    public GetAllConnectorsQueryValidator()
    {

        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.ChargeStationId)
        .GreaterThan(0)
        .NotEmpty();
    }
}
