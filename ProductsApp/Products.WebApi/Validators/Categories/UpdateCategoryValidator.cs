using FluentValidation;
using Products.WebApi.Models.Categories;

namespace Products.WebApi.Validators.Categories
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryModel>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Please specify a category name.");
        }
    }
}
