
using FluentValidation;
using GreenFlux.Application.Groups.Queries.Dtos;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetGroup;

public class GetGroupQuery : IRequest<GroupDto>
{
    public GetGroupQuery(int groupId)
    {
        GroupId = groupId;
    }

    public int GroupId { get; private set; }
}

public class GetGroupQueryValidator : AbstractValidator<GetGroupQuery>
{
    public GetGroupQueryValidator()
    {
        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();
    }
}