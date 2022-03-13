
using FluentValidation;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;

public class AddChargeStationToGroupCommand : IRequest<bool>
{
    public AddChargeStationToGroupCommand(int groupId, string name)
    {
        GroupId = groupId;
        Name = name;
    }

    public int GroupId { get; private set; }

    public string Name { get; private set; }

}

public class AddChargeStationToGroupCommandValidator : AbstractValidator<AddChargeStationToGroupCommand>
{
    public AddChargeStationToGroupCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.GroupId)
        .GreaterThan(0)
        .NotEmpty();
    }
}

