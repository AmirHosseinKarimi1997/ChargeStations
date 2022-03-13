
using MediatR;

namespace GreenFlux.Application.Groups.Commands.RemoveChargeStationFromGroup;

public class RemoveChargeStationFromGroupCommand : IRequest<bool>
{
    public RemoveChargeStationFromGroupCommand(int groupId, int id)
    {
        GroupId = groupId;
        Id = id;
    }

    public int GroupId { get; set; }

    public int Id { get; set; }
}

