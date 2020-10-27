using System;

namespace DataAccess.Models
{
    public class CityDTO
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public Guid MapId { get; set; }
    }
}
