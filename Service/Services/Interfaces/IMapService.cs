using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IMapService
    {
        IEnumerable<MapGetDTO> GetMaps();
        IEnumerable<MapInfoGetDTO> GetMapsInfo();
        MapGetDTO GetMap(Guid id);
        MapGetDTO CreateMap(MapCreateDTO dto);
        MapGetDTO UpdateMap(MapCreateDTO dto, Guid id);
        bool DeleteMap(Guid id);
    }
}