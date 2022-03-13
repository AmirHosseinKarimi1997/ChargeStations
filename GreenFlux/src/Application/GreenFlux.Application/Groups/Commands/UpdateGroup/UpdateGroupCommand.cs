
using FluentValidation;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.UpdateGroup;

public class UpdateGroupCommand : IRequest<bool>
{
    public UpdateGroupCommand(int id, string name, ulong capacityInAmps)
    {
        Id = id;
        Name = name;
        CapacityInAmps = capacityInAmps;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public ulong CapacityInAmps { get; private set; }    
}

public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {

        RuleFor(v => v.Id)
        .GreaterThan(0)
        .NotEmpty();

        RuleFor(v => v.Name)
        .MaximumLength(200)
        .NotEmpty();

        RuleFor(v => (int)v.CapacityInAmps)
        .GreaterThan(0)
        .NotEmpty();
    }
}

