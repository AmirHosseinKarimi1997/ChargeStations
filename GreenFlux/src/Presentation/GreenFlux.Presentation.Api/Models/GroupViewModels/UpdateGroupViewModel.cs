using FluentValidation;

namespace GreenFlux.Api.Models.GroupViewModels;

public class UpdateGroupViewModel
{
    public string Name { get; set; }

    public ulong CapacityInAmps { get; set; }
}

public class UpdateGroupViewModelValidator : AbstractValidator<UpdateGroupViewModel>
{
    public UpdateGroupViewModelValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotEmpty();


        RuleFor(v => (int)v.CapacityInAmps)
        .GreaterThan(0)
        .NotEmpty();
    }
}
