using DataAccess.Models;
using System;

namespace Service.DTO
{
    public class CityCreateDTO
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public Guid MapId { get; set; }
    }
}
