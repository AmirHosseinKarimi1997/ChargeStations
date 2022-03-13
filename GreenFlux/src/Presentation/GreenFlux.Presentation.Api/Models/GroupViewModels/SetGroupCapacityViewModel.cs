using FluentValidation;

namespace GreenFlux.Api.Models.GroupViewModels;

public class SetGroupCapacityViewModel
{
    public ulong CapacityInAmps { get; set; }
}

public class SetGroupCapacityViewModelValidator : AbstractValidator<SetGroupCapacityViewModel>
{
    public SetGroupCapacityViewModelValidator()
    {

        RuleFor(v => (int)v.CapacityInAmps)
        .GreaterThan(0)
        .NotEmpty();
    }
}