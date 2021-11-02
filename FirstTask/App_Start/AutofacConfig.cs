﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

using AutoMapper;

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

			// managers
			builder.RegisterType<ServiceManager>().AsSelf();
			builder.RegisterType<CompanyManager>().AsSelf();
			builder.RegisterType<EmployeeManager>().AsSelf();
			builder.RegisterType<ServiceProvidedManager>().AsSelf();
			builder.RegisterType<ServiceHistoryManager>().AsSelf();
			builder.RegisterModule<AutoFacModule>();

			// создаем новый контейнер с теми зависимостями, которые определены выше
			var container = builder.Build();

			// установка сопоставителя зависимостей
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}