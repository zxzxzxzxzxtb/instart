using Autofac;
using Instart.Repository;
using Instart.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service.Base
{
    public class AutofacService
    {
        static IContainer container = null;

        public static T Resolve<T>() {
            try {
                if (container == null) {
                    Register();
                }
            }
            catch (Exception ex) {
                throw new System.Exception("IOC实例化出错!" + ex.Message);
            }
            return container.Resolve<T>();
        }
        
        public static void Register() {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            container = builder.Build();
        }
    }
}
