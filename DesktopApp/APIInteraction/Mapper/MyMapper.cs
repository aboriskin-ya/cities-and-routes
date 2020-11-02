using AutoMapper;

namespace DesktopApp.APIInteraction.Mapper
{
    public static class MyMapper
    {
        private static IMapper Mapper;
        public static IMapper GetMapper()
        {
            if (Mapper == null)
                return new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            return Mapper;
        }
    }
}
