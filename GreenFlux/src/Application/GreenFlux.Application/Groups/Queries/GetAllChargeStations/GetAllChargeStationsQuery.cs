
using FluentValidation;
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

public class GetAllChargeStationsQueryValidator : AbstractValidator<GetAllChargeStationsQuery>
{
    public GetAllChargeStationsQueryValidator()
    {

        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();
    }
}


