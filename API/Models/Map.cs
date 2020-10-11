using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Map
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Map(Guid Id, string Name = "")
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
