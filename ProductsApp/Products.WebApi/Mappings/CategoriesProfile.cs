using AutoMapper;
using Domain;
using Products.WebApi.DTO.Categories;
using Products.WebApi.Models.Categories;

namespace Products.WebApi.Mappings
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListItemDto>();
            CreateMap<CreateCategoryModel, Category>();
            CreateMap<UpdateCategoryModel, Category>();
        }
    }
}
