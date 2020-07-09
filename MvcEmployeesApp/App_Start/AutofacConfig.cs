using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DataAccessLayer;
using MvcEmployee;
using MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
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

            container.RegisterType<DataAccess>().As<IDataAccess>()
                     .WithParameter("data", new DataContext());

            return container.Build();
        }
    }
}