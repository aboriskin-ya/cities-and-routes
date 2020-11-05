using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IPathToGraphService
    {
        List<Guid> CityListToGraph(IEnumerable<City> cityList, Guid cityFromId, Guid cityToId);
        int calculateDistance(City city, City otherCity);
    }
}
