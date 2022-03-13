
using MediatR;

namespace GreenFlux.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommand: IRequest<bool>
{
    public DeleteGroupCommand(int id)
    {
        Id = id;
    }
    public int Id { get; private set; }

}

