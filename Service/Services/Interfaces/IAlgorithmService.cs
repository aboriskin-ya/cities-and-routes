using DataAccess.DTO;
using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IAlgorithmService
    {
        List<Guid> FindShortestPath(Guid MapId, Guid CityToId, Guid CityFromId);

        IEnumerable<Guid> SolveTSG(IEnumerable<Guid> SelectedCities, Guid MapId);
    }
}
