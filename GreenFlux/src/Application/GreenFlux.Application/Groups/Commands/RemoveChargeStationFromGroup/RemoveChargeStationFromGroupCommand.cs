
using FluentValidation;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.RemoveChargeStationFromGroup;

public class RemoveChargeStationFromGroupCommand : IRequest<bool>
{
    public RemoveChargeStationFromGroupCommand(int groupId, int id)
    {
        GroupId = groupId;
        Id = id;
    }

    public int GroupId { get; private set; }

    public int Id { get; private set; }
}


public class RemoveChargeStationFromGroupCommandValidator : AbstractValidator<RemoveChargeStationFromGroupCommand>
{
    public RemoveChargeStationFromGroupCommandValidator()
    {
        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.Id)
        .GreaterThan(0)
        .NotEmpty();
    }
}
