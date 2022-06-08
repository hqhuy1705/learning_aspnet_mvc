using Autofac;
using AutoMapper;

namespace WebAPI.Business
{
    public class AutoMapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Register auto mapper configuration
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            })).AsSelf().SingleInstance();

            //Create mapper
            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }
    }
}
