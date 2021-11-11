using Autofac;

using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceRegister.App_Start
{
	public class AutoFacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(context => new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<AutoMapperProfile>();
			}
			)).AsSelf().SingleInstance();

			builder.Register(c =>
			{
				var context = c.Resolve<IComponentContext>();
				var config = context.Resolve<MapperConfiguration>();
				return config.CreateMapper(context.Resolve);
			})
			.As<IMapper>()
			.InstancePerLifetimeScope();
		}
	}
}