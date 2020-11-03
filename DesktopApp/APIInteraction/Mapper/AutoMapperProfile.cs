using AutoMapper;
using DataAccess.Models;
using Service.DTO;

namespace DesktopApp.APIInteraction.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityDTO>()
                .ForMember(dest => dest.Position,
                    map => map.MapFrom(
                    source => new DataAccess.Models.Position
                    {
                          X = source.X,
                          Y = source.Y
                    }));

            CreateMap<SettingsDTO, Settings>();
        }
    }
}
