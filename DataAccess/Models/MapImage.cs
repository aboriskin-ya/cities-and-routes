using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class MapImage: BaseEntity
    {
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        
    }
}
