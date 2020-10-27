using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Image : BaseEntity
    {
        [Required]
        public byte[] Data { get; set; }

        [Required]
        public string ContentType { get; set; }

    }
}
