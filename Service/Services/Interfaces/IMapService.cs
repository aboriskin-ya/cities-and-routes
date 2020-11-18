using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IMapService
    {
        IEnumerable<MapGetDTO> GetMaps();
        IEnumerable<MapIdNameGetDTO> GetMapsNames();
        MapGetDTO GetMap(Guid id);
        MapGetDTO CreateMap(MapCreateDTO dto);
        MapCreateDTO UpdateMap(Guid id, MapCreateDTO dto);
        bool DeleteMap(Guid id);
    }
}