using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Route : BaseEntity
    {
        [Required]
        public int Distance { get; set; }
        public Guid FirstCityId { get; set; }
        public Guid SecondCityId { get; set; }
        public Map Map { get; set; }
        public Guid MapId { get; set; }   
    }
}
