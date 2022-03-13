
using FluentValidation;
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

public class GetChargeStationQueryValidator : AbstractValidator<GetChargeStationQuery>
{
    public GetChargeStationQueryValidator()
    {

        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.Id)
        .GreaterThan(0)
        .NotEmpty();
    }
}

