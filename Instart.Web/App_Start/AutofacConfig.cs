using Autofac;
using Autofac.Integration.Mvc;
using Instart.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            //builder.RegisterType<UserService>().As<IUserService>();

            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            Assembly serviceAssembly = Assembly.Load("Instart.Service");
            builder.RegisterTypes(serviceAssembly.GetTypes()).AsImplementedInterfaces();

            Assembly repositoryAssembly = Assembly.Load("Instart.Repository");
            builder.RegisterTypes(repositoryAssembly.GetTypes()).AsImplementedInterfaces();
            
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}