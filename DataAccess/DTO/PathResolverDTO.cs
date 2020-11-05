using System;

namespace DataAccess.DTO
{
    public class PathResolverDTO
    {
        public Guid mapId { get; set; }
        public Guid cityFromId { get; set; }
        public Guid cityToId { get; set; }
    }
}
