using FirstTask.App_Start;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using DatabaseEditor;
using System.Threading;

namespace FirstTask
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AutofacConfig.ConfigureContainer();
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			Thread thread = new Thread(StatusEditor.Start);
			thread.Start();
		}
	}
}
