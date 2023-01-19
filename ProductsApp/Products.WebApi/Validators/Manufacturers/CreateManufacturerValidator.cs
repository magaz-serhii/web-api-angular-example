using FluentValidation;
using Products.WebApi.Models.Manufacturers;

namespace Products.WebApi.Validators.Manufacturers
{
    public class CreateManufacturerValidator : AbstractValidator<CreateManufacturerMadel>
    {
        public CreateManufacturerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Please specify a manufacturer name.");
        }
    }
}
