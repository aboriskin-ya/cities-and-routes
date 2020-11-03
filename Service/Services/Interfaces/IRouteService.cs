using DataAccess.Models;
using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IRouteService
    {
        IEnumerable<Route> GetRoutes();
        Route GetRoute(Guid id);
        Route CreateRoute(RouteDTO dto);
        Route UpdateRoute(RouteDTO dto, Guid id);
    }
}
