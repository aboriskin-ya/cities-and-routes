using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IRouteService
    {
        IEnumerable<Route> GetRoute();
        Route GetRoute(Guid id);
        void CreateRoute(Route route);
        Route UpdateRoute(Route route);
    }
}
