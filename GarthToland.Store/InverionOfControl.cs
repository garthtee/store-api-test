using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace GarthToland.Store
{
    public class InverionOfControl
    {
        public static IContainer BuildContainer(IConfiguration configuratiuon, IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            RegisterByConvention(builder);

            builder.Populate(services);

            return builder.Build();
        }

        private static void RegisterByConvention(ContainerBuilder builder)
        {
            Assembly[] assemblies =
            {
                typeof(InverionOfControl).Assembly,
            };

            foreach (var assembly in assemblies)
            {
                builder
                    .RegisterAssemblyTypes(assembly)
                    .Where(HasInterfaceWithMatchingName)
                    .AsImplementedInterfaces();
            }
        }

        private static bool HasInterfaceWithMatchingName(Type type)
        {
            string interfaceName = string.Concat("I", type.Name);
            return type.GetInterface(interfaceName) != null;
        }
    }
}
