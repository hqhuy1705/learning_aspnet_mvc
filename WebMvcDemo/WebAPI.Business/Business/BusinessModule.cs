using Autofac;
using System.Linq;
using System.Reflection;

namespace WebAPI.Business
{
    public class BusinessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Register business service
            //builder.RegisterAssemblyTypes(Assembly.Load(ThisAssembly.FullName))
            builder.RegisterAssemblyTypes(Assembly.Load("WebAPI.Business"))
                      .Where(t => t.Name.EndsWith("Business"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();
        }
    }
}
