
using GreenFlux.Domain.Entities.GroupAggregate;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.RemoveChargeStationFromGroup;

public class RemoveChargeStationFromGroupCommandHandler : IRequestHandler<RemoveChargeStationFromGroupCommand, bool>
{

    private readonly IGroupRepository _groupRepository;

    public RemoveChargeStationFromGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> Handle(RemoveChargeStationFromGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetAsync(request.GroupId);

        if (group == null)
            throw new Exception();

        group.RemoveChargeStation(request.Id);

        await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }
}

