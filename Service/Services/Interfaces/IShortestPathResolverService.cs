using Service.DTO;

namespace Service.Services.Interfaces
{
    public interface IShortestPathResolverService
    {
        ShortestPathResponseDTO FindShortestPath(ShortPathResolverDTO PathResolverDTO, string startName, string finishName);
    }
}
