using FluentValidation;

namespace GreenFlux.Api.Models.GroupViewModels;

public class SetConnectorMaxCurrentViewModel
{
    public uint MaxCurrnetInAmps { get; set; }
}

public class SetConnectorMaxCurrentViewModelValidator : AbstractValidator<SetConnectorMaxCurrentViewModel>
{
    public SetConnectorMaxCurrentViewModelValidator()
    {

        RuleFor(v => (int)v.MaxCurrnetInAmps)
        .GreaterThan(0)
        .NotEmpty();
    }
}
