using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DataAccessLayer;
using MvcEmployee;
using MyModels;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcEmployeesApp.App_Start
{
    public class AutofacConfig
    {
        public static void Initilize(HttpConfiguration httpConfiguration)
        {
            var container = Initilize(httpConfiguration, new ContainerBuilder());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer Initilize(HttpConfiguration httpConfiguration, ContainerBuilder container)
        {
            container.RegisterControllers(typeof(MvcApplication).Assembly);
            container.RegisterApiControllers(Assembly.GetExecutingAssembly());

            container.RegisterType<EmployeeDataAccess>().As<IEmployeeDataAccess>()
                     .WithParameter("data", new DataContext(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString));

            container.RegisterType<UserDataAccess>().As<IUserDataAccess>()
                     .WithParameter("data", new DataContext(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString));

            return container.Build();
        }
    }
}