using FluentValidation;

namespace GreenFlux.Api.Models.GroupViewModels;

public class AddChargeStationViewModel
{
    public string Name { get; set; }
}

public class AddChargeStationViewModelValidator : AbstractValidator<AddChargeStationViewModel>
{
	public AddChargeStationViewModelValidator()
	{
		RuleFor(v => v.Name).MaximumLength(200).NotEmpty();
	}
}
