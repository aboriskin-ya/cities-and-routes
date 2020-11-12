using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public class MapGetDTO
    {
        public string Name { get; set; }
        public Guid ImageId { get; set; }
        public List<RouteCreateDTO> RouteDTOs { get; set; }
        public List<CityCreateDTO> CityDTOs { get; set; }
        public SettingsDTO SettingsDTO { get; set; }
    }
}
