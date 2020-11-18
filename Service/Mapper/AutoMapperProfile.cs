using AutoMapper;
using DataAccess.Models;
using Service.DTO;

namespace Service.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MapGetDTO, Map>();
            CreateMap<Map, MapGetDTO>();
            CreateMap<Map, MapCreateDTO>();
            CreateMap<CityCreateDTO, City>().ForMember("X", opt => opt.MapFrom(src => src.Position.X)).ForMember("Y", opt => opt.MapFrom(src => src.Position.Y));
            CreateMap<City, CityGetDTO>().ForPath(dest => dest.Position.X, opt => opt.MapFrom(src => src.X)).ForPath(dest => dest.Position.Y, opt => opt.MapFrom(src => src.Y));
            CreateMap<City, CityCreateDTO>().ForPath(dest => dest.Position.X, opt => opt.MapFrom(src => src.X)).ForPath(dest => dest.Position.Y, opt => opt.MapFrom(src => src.Y));
            CreateMap<RouteCreateDTO, Route>();
            CreateMap<Route, RouteGetDTO>();
            CreateMap<SettingsUpdateDTO, Settings>();
            CreateMap<SettingsDTO, Settings>();
            CreateMap<Settings, SettingsDTO>();
            CreateMap<Map, ShortPathResolverDTO>();
        }
    }
}
