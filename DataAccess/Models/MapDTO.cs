using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class MapDTO
    {
        public string Name { get; set; }
        public Guid ImageId { get; set; }
    }
}
