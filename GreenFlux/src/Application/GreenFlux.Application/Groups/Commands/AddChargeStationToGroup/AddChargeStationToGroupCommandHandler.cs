
using GreenFlux.Application.Exceptions;
using GreenFlux.Domain.Entities.GroupAggregate;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;

public class AddChargeStationToGroupCommandHandler : IRequestHandler<AddChargeStationToGroupCommand, bool>
{
    private readonly IGroupRepository _groupRepository;

    public AddChargeStationToGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> Handle(AddChargeStationToGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetAsync(request.GroupId);

        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);

        group.AddChargeStation(request.Name);

        await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }
}

