using FluentValidation;
using GreenFlux.Domain.Common;

namespace GreenFlux.Api.Models.GroupViewModels;

public class AddConnectorViewModel
{
    public int ConnectorNumber { get; set; }

    public uint MaxCurrentInAmps { get; set; }
}

public class AddConnectorViewModelValidator : AbstractValidator<AddConnectorViewModel>
{
	public AddConnectorViewModelValidator()
	{
        RuleFor(v => v.ConnectorNumber)
        .InclusiveBetween(1, ConfigHelper.MaxNumberOfConnectorsAttachedToChargeStations)
        .NotEmpty();

        RuleFor(v => (int)v.MaxCurrentInAmps)
        .GreaterThan(0)
        .NotEmpty();
    }
}

