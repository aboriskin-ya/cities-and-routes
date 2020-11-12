using System;

namespace Service.DTO
{
    public class RouteGetDTO
    {
        public Guid Id { get; set; }
        public Guid FirstCityId { get; set; }
        public Guid SecondCityId { get; set; }
        public Guid MapId { get; set; }
        public int Distance { get; set; }
    }
}