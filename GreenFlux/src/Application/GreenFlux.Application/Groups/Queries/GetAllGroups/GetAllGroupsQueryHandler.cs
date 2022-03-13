
using GreenFlux.Application.Common;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Interfaces;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetAllGroups;

public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, IEnumerable<GroupDto>>
{
    private readonly IGroupQueries _query;

    public GetAllGroupsQueryHandler(IGroupQueries query)
    {
        _query = query;
    }

    public async Task<IEnumerable<GroupDto>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var data = await _query.GetAllGroupsAsync();

        if (data == null)
            return null;

        var result = data.Select(x => Mapper.MapToDto(x));
        return result;
    }
}

