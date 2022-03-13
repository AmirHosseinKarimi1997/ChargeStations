
using FluentValidation;
using GreenFlux.Domain.Common;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.SetConnectorMaxCurrent;

public class SetConnectorMaxCurrentCommand : IRequest<bool>
{
    public SetConnectorMaxCurrentCommand(int groupId, int chargeStationId, int connectorNumber, uint maxCurrnetInAmps)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        ConnectorNumber = connectorNumber;
        MaxCurrnetInAmps = maxCurrnetInAmps;
    }

    public int GroupId { get; private set; }
    public int ChargeStationId { get; private set; }
    public int ConnectorNumber { get; private set; }
    public uint MaxCurrnetInAmps { get; private set; }
}

public class SetConnectorMaxCurrentCommandValidator : AbstractValidator<SetConnectorMaxCurrentCommand>
{
    public SetConnectorMaxCurrentCommandValidator()
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

        RuleFor(v => (int)v.MaxCurrnetInAmps)
        .GreaterThan(0)
        .NotEmpty();
    }
}