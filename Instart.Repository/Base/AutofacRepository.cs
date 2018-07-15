using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
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
            builder.RegisterType<ArticleRepository>().As<IArticleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SchoolRepository>().As<ISchoolRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MajorRepository>().As<IMajorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TeacherRepository>().As<ITeacherRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BannerRepository>().As<IBannerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DivisionRepository>().As<IDivisionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CampusRepository>().As<ICampusRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorksRepository>().As<IWorksRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PartnerRepository>().As<IPartnerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AboutInstartRepository>().As<IAboutInstartRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ContactRepository>().As<IContactRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StarStudentRepository>().As<IStarStudentRepository>().InstancePerLifetimeScope();
            container = builder.Build();
        }
    }
}
