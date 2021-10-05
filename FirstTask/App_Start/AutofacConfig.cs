using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration;
using Autofac.Integration.Mvc;

using FirstTaskEntities.Repository;

namespace FirstTask.App_Start
{
	public class AutofacConfig
	{
		public static void ConfigureContainer()
		{
			/*var builder = new ContainerBuilder();

			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			// регистрируем споставление типов
			builder.RegisterType<ApplicationContext>().As<>();

			// создаем новый контейнер с теми зависимостями, которые определены выше
			var container = builder.Build();

			// установка сопоставителя зависимостей
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));*/
		}
	}
}