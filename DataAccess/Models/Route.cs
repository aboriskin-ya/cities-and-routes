using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Route : BaseEntity
    {
        [Required]
        public int Distance { get; set; }
        public City FirstCity { get; set; }
        public Guid FirstCityId { get; set; }
        public City SecondCity { get; set; }
        public Guid SecondCityId { get; set; }
        public Map Map { get; set; }
        public Guid MapId { get; set; }
    }
}
