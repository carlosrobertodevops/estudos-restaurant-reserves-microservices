using AutoMapper;

namespace Restaurant.Application.Mapper
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            //CreateMap<CreateRouteCommand, Route>()
            //    .ConstructUsing(r => new Route(r.From,
            //                                   r.To,
            //                                   r.Value,
            //                                   new RouteValidator()));

            //CreateMap<Route, RouteViewModel>().ForMember(rv => rv.Id, m => m.MapFrom(r => r.Id))
            //                                  .ForMember(rv => rv.Origem, m => m.MapFrom(r => r.From))
            //                                  .ForMember(rv => rv.Destino, m => m.MapFrom(r => r.To))
            //                                  .ForMember(rv => rv.Valor, m => m.MapFrom(r => r.Value));
        }
    }
}
