
using FluentValidation;
using MediatR;

namespace GreenFlux.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommand: IRequest<bool>
{
    public DeleteGroupCommand(int id)
    {
        Id = id;
    }
    public int Id { get; private set; }

}

public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(v => v.Id)
        .GreaterThan(0)
        .NotEmpty();
    }
}

