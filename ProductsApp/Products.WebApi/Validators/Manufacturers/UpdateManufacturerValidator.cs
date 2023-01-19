using FluentValidation;
using Products.WebApi.Models.Manufacturers;

namespace Products.WebApi.Validators.Manufacturers
{
    public class UpdateManufacturerValidator : AbstractValidator<UpdateManufacturerMadel>
    {
        public UpdateManufacturerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Please specify a manufacturer name.");
        }
    }
}
