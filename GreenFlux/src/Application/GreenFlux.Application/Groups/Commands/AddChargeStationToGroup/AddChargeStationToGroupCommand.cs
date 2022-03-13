
using MediatR;

namespace GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;

public class AddChargeStationToGroupCommand : IRequest<bool>
{
    public AddChargeStationToGroupCommand(int groupId, string name)
    {
        GroupId = groupId;
        Name = name;
    }

    public int GroupId { get; private set; }

    public string Name { get; private set; }

}

