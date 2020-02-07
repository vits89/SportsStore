using AutoMapper;
using SportsStore.AppCore.Entities;
using SportsStore.WebMvcApp.ViewModels;

namespace SportsStore.WebMvcApp.Infrastructure.MapperProfiles
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, ProductViewModel>().ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.ProductID));
            CreateMap<Product, UpdateProductViewModel>().ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.ProductID));
            CreateMap<ProductViewModel, Product>().ForMember(dest => dest.ProductID, opts => opts.MapFrom(src => src.Id));
            CreateMap<UpdateProductViewModel, Product>().ForMember(dest => dest.ProductID, opts => opts.MapFrom(src => src.Id));
        }
    }
}
