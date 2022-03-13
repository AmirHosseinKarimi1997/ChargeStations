
using FluentValidation;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.UpdateChargeStationInGroup;

public class UpdateChargeStationInGroupCommand : IRequest<bool>
{
    public UpdateChargeStationInGroupCommand(int groupId, int id, string name)
    {
        GroupId = groupId;
        Id = id;
        Name = name;
    }

    public int GroupId { get; private set; }

    public int Id { get; private set; }

    public string Name { get; private set; }
}

public class UpdateChargeStationInGroupCommandValidator : AbstractValidator<UpdateChargeStationInGroupCommand>
{
    public UpdateChargeStationInGroupCommandValidator()
    {
        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.Id)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.Name)
        .MaximumLength(200)
        .NotEmpty();
    }
}