using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Map : BaseEntity
    {
        public string Name { get; set; }

        public Guid ImageId { get; set; }
        public Image Image { get; set; }
        public List<Route> Routes { get; set; }
        public List<City> Cities { get; set; }
        public Settings Settings { get; set; }
    }
}