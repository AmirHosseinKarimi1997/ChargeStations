using FluentValidation;

namespace GreenFlux.Api.Models.GroupViewModels;

public class CreateGroupViewModel
{
    public string Name { get; set; }

    public ulong CapacityInAmps { get; set; }
}

public class CreateGroupViewModelValidator : AbstractValidator<CreateGroupViewModel>
{
    public CreateGroupViewModelValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotEmpty();

        RuleFor(v => (int)v.CapacityInAmps)
        .GreaterThan(0)
        .NotEmpty();
    }
}

