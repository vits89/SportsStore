using AutoMapper;
using SportsStore.AppCore.Entities;
using SportsStore.WebMvcApp.ViewModels;

namespace SportsStore.WebMvcApp.Infrastructure.MapperProfiles
{
    public class CartLineMapperProfile : Profile
    {
        public CartLineMapperProfile()
        {
            CreateMap<CartLine, CartLineViewModel>().ForMember(dest => dest.Product, opts => opts.MapFrom(src => src.Product.Name));
        }
    }
}
