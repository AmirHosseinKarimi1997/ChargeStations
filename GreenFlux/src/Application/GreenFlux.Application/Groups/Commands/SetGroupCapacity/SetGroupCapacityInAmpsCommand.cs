
using MediatR;

namespace GreenFlux.Application.Groups.Commands.SetGroupCapacity;

public class SetGroupCapacityInAmpsCommand: IRequest<bool>
{
    public SetGroupCapacityInAmpsCommand(int groupId, ulong capacityInAmps)
    {
        GroupId = groupId;
        CapacityInAmps = capacityInAmps;
    }
    public int GroupId { get; private set; }
    public ulong CapacityInAmps { get; private set; }
}

