using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Map: BaseEntity
    {
        public string Name { get; set; }
        public MapImage Image { get; set; }
    }
}
