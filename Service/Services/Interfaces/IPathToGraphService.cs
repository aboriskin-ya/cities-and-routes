using DataAccess.Models;
using Service.DTO;

namespace Service.Services.Interfaces
{
    public interface IPathToGraphService
    {
        ShortPathResolverDTO MapToGraph(Map Map);
    }
}
