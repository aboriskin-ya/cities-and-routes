using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Map: BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public Image Image { get; set; }
        public Guid ImageId { get; set; }
    }
}
