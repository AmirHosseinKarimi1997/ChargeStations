
using GreenFlux.Domain.Entities.GroupAggregate;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandHandler: IRequestHandler<DeleteGroupCommand, bool>
{
    private readonly IGroupRepository _groupRepository;

    public DeleteGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetAsync(request.Id);

        if (group == null)
            throw new Exception();

        _groupRepository.Delete(group);

        await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}

