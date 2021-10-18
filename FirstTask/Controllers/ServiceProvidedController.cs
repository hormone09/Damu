using FirstTask.Managers;
using FirstTask.Models;
using FirstTask.ViewQueris;
using System.Web.Mvc;

namespace FirstTask.Controllers
{
    public class ServiceProvidedController : Controller
	{
		private ServiceProvidedManager manager;

		public ServiceProvidedController(ServiceProvidedManager manager)
		{
			this.manager = manager;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Index(ServiceProvidedViewQuery query)
		{
			var result = manager.List(query);

			return Json(result);
		}

		[HttpPost]
		public JsonResult AddProvidedService(ServiceProvidedModel model)
		{
			var result = manager.Add(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult EditProvidedService(ServiceProvidedModel model)
		{
			var result = manager.Edit(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult DeleteProvidedService(int id)
		{
			var result = manager.Delete(id);

			return Json(result);
		}
	}
}