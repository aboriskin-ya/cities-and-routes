using Service.DTO;
using DataAccess.Models;

namespace Service.Services.Interfaces
{
    public interface IPathToGraphService
    {
        ShortPathResolverDTO MapToGraph(Map Map);
    }
}
