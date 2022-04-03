using Autofac;
using Autofac.Integration.WebApi;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using WebAPI.Entity;
using WebAPI.Repository;

namespace WebAPI.Business
{
    public static class ContainerConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.Load(nameof(WebAPI)));

            //Register db context
            builder.RegisterType<MyEntities>()
                   .As<DbContext>()
                   .InstancePerRequest();

            //Register db factory
            builder.RegisterType<DbFactory>()
                   .As<IDbFactory>()
                   .InstancePerRequest();

            //Register generic repository
            builder.RegisterGeneric(typeof(GenericRepository<>))
                   .As(typeof(IGenericRepository<>))
                   .InstancePerRequest();

            //Register db factory
            builder.RegisterType<UnitOfWork>()
                   .As<UnitOfWork>()
                   .InstancePerRequest();

            //Register business module
            builder.RegisterModule(new BusinessModule());

            //Register auto mapper module
            builder.RegisterModule(new AutoMapperModule());

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}
