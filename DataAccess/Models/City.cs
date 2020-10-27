using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class City : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Map Map { get; set; }
        public Guid MapId { get; set; }

    }
}
