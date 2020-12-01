using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public class MapGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ImageId { get; set; }
        public List<RouteGetDTO> Routes { get; set; }
        public List<CityGetDTO> Cities { get; set; }
        public SettingsGetDTO Settings { get; set; }
    }
}