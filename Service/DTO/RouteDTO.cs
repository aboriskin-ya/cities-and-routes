using System;

namespace Service.DTO
{
    public class RouteDTO
    {
        public Guid FirstCityId { get; set; }
        public Guid SecondCityId { get; set; }
        public Guid MapId { get; set; }
        public int Distance { get; set; }
    }
}
