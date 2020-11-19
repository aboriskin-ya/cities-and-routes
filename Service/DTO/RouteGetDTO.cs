using System;

namespace Service.DTO
{
    public class RouteGetDTO
    {
        public Guid Id { get; set; }
        public int Distance { get; set; }
        public Guid FirstCityId { get; set; }
        public CityGetDTO FirstCity { get; set; }
        public Guid SecondCityId { get; set; }
        public CityGetDTO SecondCity { get; set; }
        public Guid MapId { get; set; }
    }
}