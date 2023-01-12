using AutoMapper;
using Domain;
using Products.WebApi.DTO.Products;
using Products.WebApi.Models.Products;

namespace Products.WebApi.Mappings
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductListItemDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductModel, Product>();
            CreateMap<UpdateProductModel, Product>();
        }
    }
}
