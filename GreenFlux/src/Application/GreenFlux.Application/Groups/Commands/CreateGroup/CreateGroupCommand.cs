
using FluentValidation;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommand : IRequest<int>
{
    public CreateGroupCommand(string name, ulong capacityAmps)
    {
        Name = name;
        CapacityInAmps = capacityAmps;
    }

    public string Name { get; private set; }

    public ulong CapacityInAmps { get; private set; }    
}

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(v => v.Name)
        .MaximumLength(200)
        .NotEmpty();

        RuleFor(v => (int)v.CapacityInAmps)
        .GreaterThan(0)
        .NotEmpty();
    }
}

