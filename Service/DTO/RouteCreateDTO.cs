using System;

namespace Service.DTO
{
    public class RouteCreateDTO
    {
        public int Distance { get; set; }
        public Guid FirstCityId { get; set; }
        public Guid SecondCityId { get; set; }
        public Guid MapId { get; set; }
    }
}