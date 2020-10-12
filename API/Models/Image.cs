using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
        public Image(Guid Id, byte[] Data)
        {
            this.Id = Id;
        }
    }
}
