
using FluentValidation;
using GreenFlux.Domain.Common;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.RemoveConnectorFromChargeStation;

public class RemoveConnectorFromChargeStationCommand : IRequest<bool>
{
    public RemoveConnectorFromChargeStationCommand(int groupId, int chargeStationId, int connectorNumber)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        ConnectorNumber = connectorNumber;
    }

    public int GroupId { get; private set; }

    public int ChargeStationId { get; private set; }

    public int ConnectorNumber { get; private set; }
}

public class RemoveConnectorFromChargeStationCommandValidator : AbstractValidator<RemoveConnectorFromChargeStationCommand>
{
    public RemoveConnectorFromChargeStationCommandValidator()
    {
        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.ChargeStationId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.ConnectorNumber)
        .InclusiveBetween(1, ConfigHelper.MaxNumberOfConnectorsAttachedToChargeStations)
        .NotEmpty();
    }
}

