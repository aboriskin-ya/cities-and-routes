using AutoMapper;
using DataAccess.Models;
using Service.DTO;

namespace Service.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MapCreateDTO, Map>();
            CreateMap<Map, MapGetDTO>();
            CreateMap<Map, MapIdNameGetDTO>();

            CreateMap<CityCreateDTO, City>()
                .ForMember("X", opt => opt.MapFrom(src => src.Position.X))
                .ForMember("Y", opt => opt.MapFrom(src => src.Position.Y));
            CreateMap<City, CityGetDTO>()
                .ForPath(dest => dest.Position.X, opt => opt.MapFrom(src => src.X))
                .ForPath(dest => dest.Position.Y, opt => opt.MapFrom(src => src.Y));

            CreateMap<RouteCreateDTO, Route>();
            CreateMap<Route, RouteGetDTO>();

            CreateMap<SettingsUpdateDTO, Settings>();
            CreateMap<SettingsCreateDTO, Settings>();
            CreateMap<Settings, SettingsGetDTO>();

            CreateMap<Map, ShortPathResolverDTO>();
        }
    }
}