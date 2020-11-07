using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IPathResolverService
    {
        List<Guid> FindPath(Guid MapId, Guid CityToId, Guid CityFromId);
    }
}
