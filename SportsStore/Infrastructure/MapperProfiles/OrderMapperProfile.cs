using AutoMapper;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportsStore.Infrastructure.MapperProfiles
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<Order, OrderViewModel>().ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.OrderID));
            CreateMap<AddOrderViewModel, Order>()
                .ForMember(dest => dest.Line1, opts => opts.MapFrom(src => src.AddressLine1))
                .ForMember(dest => dest.Line2, opts => opts.MapFrom(src => src.AddressLine2))
                .ForMember(dest => dest.Line3, opts => opts.MapFrom(src => src.AddressLine3));
        }
    }
}
