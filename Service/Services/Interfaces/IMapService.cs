using Service.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IMapService
    {
        IEnumerable<MapGetDTO> GetMaps();
        Task<IEnumerable<MapInfoGetDTO>> GetMapsInfoAsync();
        MapGetDTO GetMap(Guid id);
        MapGetDTO CreateMap(MapCreateDTO dto);
        MapGetDTO UpdateMap(MapCreateDTO dto, Guid id);
        bool DeleteMap(Guid id);
    }
}