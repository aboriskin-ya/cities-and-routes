using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IRouteService
    {
        IEnumerable<Route> GetRoute();
        Route GetRoute(Guid id);
        Route CreateRoute(RouteDTO dto);
        Route UpdateRoute(RouteDTO dto, Guid id);
    }
}
