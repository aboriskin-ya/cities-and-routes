using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IGraphService
    {
        List<Guid> FindShortestPath(string startName, string finishName);
    }
}
