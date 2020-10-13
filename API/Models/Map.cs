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
        public Image Image { get; set; }

        public Map(Guid Id, string Name, Guid ImageId)
        {
            this.Id = Id;
            this.Name = Name;
            this.Image = new Image(ImageId, new byte[0]);
        }
    }
}
