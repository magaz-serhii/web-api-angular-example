using FluentValidation;
using Products.WebApi.Models.Categories;

namespace Products.WebApi.Validators.Categories
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryModel>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Please specify a category name.");
        }
    }
}
