using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Image: BaseEntity
    {
        [Required]
        public byte[] Data { get; set; }

        [Required]
        public string ContentType { get; set; }
        
    }
}
