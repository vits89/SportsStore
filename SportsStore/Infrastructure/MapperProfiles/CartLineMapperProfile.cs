using AutoMapper;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportsStore.Infrastructure.MapperProfiles
{
    public class CartLineMapperProfile : Profile
    {
        public CartLineMapperProfile()
        {
            CreateMap<CartLine, CartLineViewModel>().ForMember(dest => dest.Product, opts => opts.MapFrom(src => src.Product.Name));
        }
    }
}
