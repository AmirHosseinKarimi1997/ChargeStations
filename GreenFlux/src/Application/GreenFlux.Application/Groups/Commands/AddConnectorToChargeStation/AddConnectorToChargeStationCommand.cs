
using FluentValidation;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.AddConnectorToChargeStation;

public class AddConnectorToChargeStationCommand : IRequest<bool>
{
    public AddConnectorToChargeStationCommand(int groupId, int chargeStationId, int connectorNumber, uint maxCurrentInAmps)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        ConnectorNumber = connectorNumber;
        MaxCurrentInAmps = maxCurrentInAmps;
    }

    public int GroupId { get; private set; }

    public int ChargeStationId { get; private set; }

    public int ConnectorNumber { get; private set; }

    public uint MaxCurrentInAmps { get; private set; }
}

public class AddConnectorToChargeStationCommandValidator : AbstractValidator<AddConnectorToChargeStationCommand>
{
    public AddConnectorToChargeStationCommandValidator()
    {
        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.ChargeStationId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.ConnectorNumber)
        .InclusiveBetween(1, 5) //5 from config
        .NotEmpty();

        RuleFor(v => (int)v.MaxCurrentInAmps)
        .GreaterThan(0)
        .NotEmpty();
    }
}


