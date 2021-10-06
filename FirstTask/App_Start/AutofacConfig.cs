using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

using FirstTask.Managers;

using FirstTaskEntities.Interfaces;
using FirstTaskEntities.Models;
using FirstTaskEntities.Repository;

namespace FirstTask.App_Start
{
	public static class AutofacConfig
	{
		public static void ConfigureContainer()
		{
			var builder = new ContainerBuilder();

			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			// регистрируем споставление типов
			builder.RegisterType<ServicesRepository>().As<IRepository<Service>>().WithParameter("rep", new ServicesRepository());
			builder.RegisterType<EmployeeRepository>().As<IRepository<Employee>>().WithParameter("rep", new EmployeeRepository());
			builder.RegisterType<ServiceProvidedRepository>().As<IRepository<ServiceProvided>>().WithParameter("rep", new ServiceProvidedRepository());
			builder.RegisterType<ServicesHistoryRepository>().As<IRepository<ServicesHistory>>().WithParameter("rep", new ServicesHistoryRepository());
			builder.RegisterType<CompanyRepository>().As<IRepository<Company>>().WithParameter("rep", new CompanyRepository());

			// managers
			builder.RegisterType<ServiceManager>().AsSelf();

			// создаем новый контейнер с теми зависимостями, которые определены выше
			var container = builder.Build();

			// установка сопоставителя зависимостей
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}