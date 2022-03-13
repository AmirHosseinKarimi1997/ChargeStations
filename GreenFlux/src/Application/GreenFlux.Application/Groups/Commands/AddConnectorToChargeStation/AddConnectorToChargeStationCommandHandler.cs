
using GreenFlux.Application.Exceptions;
using GreenFlux.Domain.Entities.GroupAggregate;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.AddConnectorToChargeStation;

public class AddConnectorToChargeStationCommandHandler : IRequestHandler<AddConnectorToChargeStationCommand, bool>
{
    private readonly IGroupRepository _groupRepository;

    public AddConnectorToChargeStationCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> Handle(AddConnectorToChargeStationCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetAsync(request.GroupId);

        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);

        var chargeStation = group.ChargeStations.FirstOrDefault(x => x.Id == request.ChargeStationId);

        if (chargeStation == null)
            throw new ChargeStationNotFoundException(request.GroupId, request.ChargeStationId);

        group.AddConnectorToChargeStation(request.ChargeStationId, request.ConnectorNumber, request.MaxCurrentInAmps);

        await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }
}

