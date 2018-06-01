using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository.Base
{
    public class AutofacRepository
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
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            container = builder.Build();
        }
    }
}
