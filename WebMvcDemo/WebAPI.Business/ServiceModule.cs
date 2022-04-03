using Autofac;
using AutoMapper;
using System.Linq;
using System.Reflection;

namespace WebAPI.Business
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Register business service
            //builder.RegisterAssemblyTypes(Assembly.Load(ThisAssembly.FullName))
            builder.RegisterAssemblyTypes(Assembly.Load("WebAPI.Business"))
                      .Where(t => t.Name.EndsWith("Business"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();

            //Register auto mapper
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
