using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Data.Service;

namespace GAN_Developer_Test.App_Start
{
    public static class Bootstrapper
    {
        public static void Start()
        {
           SetAutofacContainer();
        }
        private static void SetAutofacContainer()
        {
            //Create Autofac builder
            var builder = new ContainerBuilder();
            //Now register all depedencies to your custom IoC container

            //register mvc controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
               .AsImplementedInterfaces();

            builder.RegisterModelBinderProvider();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //types

          
            builder.RegisterType<FileService>().As<IFileService>().InstancePerRequest();

          
            var containerBuilder = builder.Build();

            //MVC resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder));

            // Create the depenedency resolver for Web Api
            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder);
            var config = new HttpConfiguration();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder);
        }

    }
}