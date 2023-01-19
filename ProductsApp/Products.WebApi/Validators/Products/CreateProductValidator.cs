using FluentValidation;
using Products.WebApi.Models.Products;

namespace Products.WebApi.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductModel>
    {
        public CreateProductValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Please specify a product name.");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Please add a description of a product.")
                .MinimumLength(20)
                .WithMessage("Please add a description of a product at least 20 character length.");

            RuleFor(c => c.Price)
                .GreaterThan(0)
                .WithMessage("A product price must be greater than 0.");
        }
    }
}
