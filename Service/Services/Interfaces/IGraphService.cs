using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IGraphService
    {
        List<Guid> FindShortestPath(string startName, string finishName);
    }
}
