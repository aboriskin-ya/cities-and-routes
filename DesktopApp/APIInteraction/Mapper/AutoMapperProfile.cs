using AutoMapper;
using DesktopApp.Models;
using Service.DTO;
using Service.PathResolver;

namespace DesktopApp.APIInteraction.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityCreateDTO>()
                .ForMember(dest => dest.Position,
                    map => map.MapFrom(
                    source => new DataAccess.Models.Position
                    {
                        X = source.X,
                        Y = source.Y
                    }));

            CreateMap<CityGetDTO, City>()
                .ForMember("X", opt => opt.MapFrom(src => src.Position.X))
                .ForMember("Y", opt => opt.MapFrom(src => src.Position.Y));

            CreateMap<Route, RouteCreateDTO>();
            CreateMap<RouteGetDTO, Route>();
            CreateMap<TravelSalesmanResponse, TravelSalesman>();
        }
    }
}