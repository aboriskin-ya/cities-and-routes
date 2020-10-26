using AutoMapper;
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
        Route CreateRoute(RouteDTO dto);
        Route UpdateRoute(RouteDTO dto, Route route);
    }
}
