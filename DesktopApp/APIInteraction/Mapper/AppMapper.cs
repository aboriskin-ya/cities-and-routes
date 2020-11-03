using AutoMapper;
using System;

namespace DesktopApp.APIInteraction.Mapper
{
    public class AppMapper
    {
        private static AppMapper instance;

        private static readonly Object syncRoot = new Object();

        public IMapper Mapper { get; private set; }

        private AppMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
        }
        public static AppMapper GetAppMapper()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new AppMapper();
                    }
                }
            }
            return instance;
        }
    }
}
