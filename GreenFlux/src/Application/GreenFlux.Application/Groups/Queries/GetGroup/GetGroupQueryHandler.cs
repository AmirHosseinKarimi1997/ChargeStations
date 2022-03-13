
using GreenFlux.Application.Common;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Interfaces;
using MediatR;

namespace GreenFlux.Application.Groups.Queries.GetGroup;

public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDto>
{
    private readonly IGroupQueries _query;

    public GetGroupQueryHandler(IGroupQueries query)
    {
        _query = query;
    }

    public async Task<GroupDto> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var data = await _query.GetGroupAsync(request.GroupId);

        if(data == null)
            return default(GroupDto);

        return Mapper.MapToDto(data);
    }
}

