using FluentValidation;

namespace GreenFlux.Api.Models.GroupViewModels;

public class UpdateChargeStationViewModel
{
    public string Name { get; set; }
}

public class UpdateChargeStationViewModelValidator : AbstractValidator<UpdateChargeStationViewModel>
{
    public UpdateChargeStationViewModelValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotEmpty();
    }
}
