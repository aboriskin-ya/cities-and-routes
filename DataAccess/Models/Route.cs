using System;

namespace DataAccess.Models
{
    public class Route : BaseEntity
    {
        public int Distance { get; set; }

        public Guid FirstCityId { get; set; }
        public City FirstCity { get; set; }
        public Guid SecondCityId { get; set; }
        public City SecondCity { get; set; }
        public Guid MapId { get; set; }
        public Map Map { get; set; }
    }
}