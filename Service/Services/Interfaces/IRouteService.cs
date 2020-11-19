using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IRouteService
    {
        IEnumerable<RouteGetDTO> GetRoutes();
        RouteGetDTO GetRoute(Guid id);
        RouteGetDTO CreateRoute(RouteCreateDTO dto);
        RouteGetDTO UpdateRoute(Guid id, RouteCreateDTO dto);

        bool DeleteRoute(Guid id);
    }
}