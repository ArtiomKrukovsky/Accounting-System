using System.Reflection;
using Autofac;
using Quartz;
using Module = Autofac.Module;

namespace Сonfectionery.API.Application.Modules
{
    public class QuartzModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var infrastructureAssembly = Assembly.Load("Сonfectionery.Infrastructure");

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(x => typeof(IJob).IsAssignableFrom(x)).InstancePerDependency();
        }
    }
}