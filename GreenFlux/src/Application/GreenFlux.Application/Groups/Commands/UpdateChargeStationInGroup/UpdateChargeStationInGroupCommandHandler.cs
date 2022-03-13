
using GreenFlux.Domain.Entities.GroupAggregate;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.UpdateChargeStationInGroup;

public class UpdateChargeStationInGroupCommandHandler : IRequestHandler<UpdateChargeStationInGroupCommand, bool>
{
    private readonly IGroupRepository _groupRepository;

    public UpdateChargeStationInGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> Handle(UpdateChargeStationInGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetAsync(request.GroupId);
        if (group == null)
            throw new Exception();

        var chargeStation = group.ChargeStations.FirstOrDefault(x => x.Id == request.Id);
        if (chargeStation == null)
            throw new Exception();

        chargeStation.SetName(request.Name);

        await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }
}

