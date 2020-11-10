using DataAccess.Models;
using Service.Services.Interfaces;
using Service.DTO;
using AutoMapper;

namespace Service.Services
{
    public class PathToGraphService : IPathToGraphService
    {
        private readonly IMapper _mapper;

        public PathToGraphService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ShortPathResolverDTO MapToResolver(Map Map)
        {
            ShortPathResolverDTO PathResolverDTO = _mapper.Map<ShortPathResolverDTO>(Map);
            return PathResolverDTO;
        }

    }
}
