using DataAccess.Models;
using System.Collections.Generic;

namespace Service.DTO
{
    public class ShortPathResolverDTO
    {
        public List<City> Cities { get; set; }
        public List<Route> Routes { get; set; }
    }
}
