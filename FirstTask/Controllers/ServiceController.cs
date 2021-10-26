using FirstTaskEntities.Models;
using System.Web.Mvc;
using FirstTask.Managers;
using FirstTask.ViewQueris;
using FirstTask.Models;
using System.Collections.Generic;

namespace FirstTask.Controllers
{
	public class ServiceController : Controller
	{
		private ServiceManager manager;

		public ServiceController(ServiceManager manager)
		{
			this.manager = manager;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Index(ServiceViewQuery query)
		{
			var result = manager.List(query);

			return Json(result);
		}

		[HttpPost]
		public JsonResult AddService(ServiceModel model)
		{
			var result = manager.Add(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult EditService(ServiceModel model)
		{
			var result = manager.Edit(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult DeleteService(int id)
		{
			var result = manager.Delete(id);

			return Json(result);
		}

		[HttpPost]
		public JsonResult ActivateService(int id)
		{
			var result = manager.Activate(id);

			return Json(result);
		}
	}
}