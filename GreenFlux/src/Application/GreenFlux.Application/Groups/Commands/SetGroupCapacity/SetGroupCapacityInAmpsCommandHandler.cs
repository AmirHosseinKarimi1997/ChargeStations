
using GreenFlux.Domain.Entities.GroupAggregate;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.SetGroupCapacity;

public class SetGroupCapacityInAmpsCommandHandler : IRequestHandler<SetGroupCapacityInAmpsCommand, bool>
{
    private readonly IGroupRepository _groupRepository;

    public SetGroupCapacityInAmpsCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> Handle(SetGroupCapacityInAmpsCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetAsync(request.GroupId);

        if (group == null)
            throw new Exception();

        group.SetCapacity(request.CapacityInAmps);

        await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}

