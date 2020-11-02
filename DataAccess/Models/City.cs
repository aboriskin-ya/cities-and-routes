using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class City : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Map Map { get; set; }
        public Guid MapId { get; set; }

    }
}
