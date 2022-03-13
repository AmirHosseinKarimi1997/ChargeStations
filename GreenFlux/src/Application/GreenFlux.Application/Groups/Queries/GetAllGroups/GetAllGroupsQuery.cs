
using FluentValidation;
using GreenFlux.Application.Groups.Queries.Dtos;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetAllGroups;

public class GetAllGroupsQuery : IRequest<IEnumerable<GroupDto>>
{
}

public class GetAllGroupsQueryValidator : AbstractValidator<GetAllGroupsQuery>
{
    public GetAllGroupsQueryValidator()
    {
    }
}
