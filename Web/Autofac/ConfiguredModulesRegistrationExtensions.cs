namespace Web.Autofac
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using global::Autofac;
    using global::Autofac.Core;
    using Microsoft.Extensions.Configuration;



    public static class ConfiguredModulesRegistrationExtensions
    {
        public static ContainerBuilder RegisterConfiguredModulesFromAssemblyContaining<TType>(this ContainerBuilder builder, IConfigurationRoot configuration)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            ContainerBuilder metaBuilder = new ContainerBuilder();

            metaBuilder.RegisterInstance(configuration);
            metaBuilder.RegisterAssemblyTypes(typeof(TType).GetTypeInfo().Assembly)
                       .AssignableTo<IModule>()
                       .As<IModule>()
                       .PropertiesAutowired();

            using (IContainer metaContainer = metaBuilder.Build())
            {
                foreach (IModule module in metaContainer.Resolve<IEnumerable<IModule>>())
                    builder.RegisterModule(module);
            }

            return builder;
        }
    }
}