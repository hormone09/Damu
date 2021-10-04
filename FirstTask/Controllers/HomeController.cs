using FirstTask.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstTask.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationContext context = new ApplicationContext();
		private const int pageSize = 20;

		public ActionResult Index(int? page)
		{
			if (page == null)
				page = 1;
			
			int skipCount = (Convert.ToInt32(page) - 1) * pageSize;

			var services = context.GetAllServices();
			var model = services.Skip(skipCount).Take(pageSize);
			var servicesCount = services.Count();

			int pagesCount = servicesCount / pageSize + 1;

			ViewBag.Model = model;
			ViewBag.PagesCount = pagesCount;
			

			return View();
		}
	}
}