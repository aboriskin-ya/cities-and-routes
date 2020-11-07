using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IShortestPathResolverService
    {
        List<Guid> FindShortestPath(ShortPathResolverDTO PathResolverDTO, string startName, string finishName);
    }
}
