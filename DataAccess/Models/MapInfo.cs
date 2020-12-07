using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [NotMapped]
    public class MapInfo : BaseEntity
    {
        public string Name { get; set; }
        public int CountRoutes { get; set; }
        public int CountCities { get; set; }
    }
}
