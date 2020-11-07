using System;

namespace DataAccess.DTO
{
    public class PathResolverDTO
    {
        public Guid MapId { get; set; }
        public Guid CityFromId { get; set; }
        public Guid CityToId { get; set; }
    }
}
