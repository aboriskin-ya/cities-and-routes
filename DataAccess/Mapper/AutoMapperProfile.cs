using AutoMapper;
using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MapDTO, Map>();
            CreateMap<CityDTO, City>().ForMember("X", opt => opt.MapFrom(src => src.Position.X)).ForMember("Y", opt => opt.MapFrom(src => src.Position.Y));
            CreateMap<RouteDTO, Route>();
            CreateMap<Settings, SettingsDTO>();
        }
    }
}
