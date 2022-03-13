
using GreenFlux.Application.Exceptions;
using GreenFlux.Domain.Entities.GroupAggregate;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.SetConnectorMaxCurrent;

public class SetConnectorMaxCurrentCommandHandler : IRequestHandler<SetConnectorMaxCurrentCommand, bool>
{
    private readonly IGroupRepository _groupRepository;

    public SetConnectorMaxCurrentCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> Handle(SetConnectorMaxCurrentCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetAsync(request.GroupId);

        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);

        var chargeStation = group.ChargeStations.FirstOrDefault(x => x.Id == request.ChargeStationId);

        if (chargeStation == null)
            throw new ChargeStationNotFoundException(request.GroupId, request.ChargeStationId);

        var connector = chargeStation.Connectors.FirstOrDefault(x => x.Id == request.ConnectorNumber);

        if (connector == null)
            throw new ConnectorNotFoundException(request.ChargeStationId, request.ConnectorNumber);

        group.SetConnectorMaxCurrent(request.ChargeStationId, request.ConnectorNumber, request.MaxCurrnetInAmps);

        await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }
}

