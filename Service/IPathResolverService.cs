using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IPathResolverService
    {
        List<Guid> FindPath(Guid mapId, Guid cityToId, Guid cityFromId);
    }
}
