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
            CreateMap<Settings, SettingsDTO>();
        }
    }
}
