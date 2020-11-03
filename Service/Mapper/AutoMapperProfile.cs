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
            CreateMap<Map, MapCreateDTO>();
            CreateMap<CityDTO, City>().ForMember("X", opt => opt.MapFrom(src => src.Position.X)).ForMember("Y", opt => opt.MapFrom(src => src.Position.Y));
            CreateMap<RouteDTO, Route>();
            CreateMap<Settings, SettingsDTO>();
        }
    }
}
