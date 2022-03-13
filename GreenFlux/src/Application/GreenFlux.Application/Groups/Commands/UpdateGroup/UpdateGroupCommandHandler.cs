
using GreenFlux.Domain.Entities.GroupAggregate;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, bool>
{
    private readonly IGroupRepository _groupRepository;

    public UpdateGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetAsync(request.Id);

        if (group == null)
            throw new Exception();

        group.SetName(request.Name);
        group.SetCapacity(request.CapacityInAmps);

        await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
