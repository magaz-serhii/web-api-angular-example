using AutoMapper;
using Domain;
using Products.WebApi.DTO.Manufacturers;
using Products.WebApi.Models.Manufacturers;

namespace Products.WebApi.Mappings
{
    public class ManufacturersProfile : Profile
    {
        public ManufacturersProfile()
        {
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<Manufacturer, ManufacturerListItemDto>();
            CreateMap<CreateManufacturerMadel, Manufacturer>();
            CreateMap<UpdateManufacturerMadel, Manufacturer>();
        }
    }
}
