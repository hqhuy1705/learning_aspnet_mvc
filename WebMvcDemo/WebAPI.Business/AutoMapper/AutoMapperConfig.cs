using AutoMapper;

namespace WebAPI.Business
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapperConfig()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            return mapper;
        }
    }
}
