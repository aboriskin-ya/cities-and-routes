using AutoMapper;
using DataAccess.Models;
using Service.DTO;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class PathToGraphService : IPathToGraphService
    {
        private readonly IMapper _mapper;

        public PathToGraphService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ShortPathResolverDTO MapToGraph(Map Map)
        {
            ShortPathResolverDTO PathResolverDTO = _mapper.Map<ShortPathResolverDTO>(Map);
            return PathResolverDTO;
        }

    }
}
