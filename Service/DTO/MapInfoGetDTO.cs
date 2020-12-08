using System;

namespace Service.DTO
{
    public class MapInfoGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CountRoutes { get; set; }
        public int CountCities { get; set; }
        public DateTimeOffset CreateOnUTC { get; set; }
    }
}