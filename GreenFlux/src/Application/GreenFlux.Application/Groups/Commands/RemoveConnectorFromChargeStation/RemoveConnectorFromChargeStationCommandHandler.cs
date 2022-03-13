
using GreenFlux.Application.Exceptions;
using GreenFlux.Domain.Entities.GroupAggregate;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.RemoveConnectorFromChargeStation;

public class RemoveConnectorFromChargeStationCommandHandler : IRequestHandler<RemoveConnectorFromChargeStationCommand, bool>
{
    private readonly IGroupRepository _groupRepository;

    public RemoveConnectorFromChargeStationCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> Handle(RemoveConnectorFromChargeStationCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetAsync(request.GroupId);

        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);

        var chargeStation = group.ChargeStations.FirstOrDefault(x => x.Id == request.ChargeStationId);

        if (chargeStation == null)
            throw new ChargeStationNotFoundException(request.GroupId, request.ChargeStationId);

        group.RemoveConnectorFromChargeStation(request.ChargeStationId,request.ConnectorNumber);

        await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }
}

