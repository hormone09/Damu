using FirstTask.Handlers;
using FirstTask.Managers;
using FirstTask.Models;
using FirstTask.ViewQueris;

using System.Linq;
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
			if (!ModelState.IsValid)
			{
				return Json(new MessageHandler(false, ModelState.Values.Select(x => x.Errors).First(x => x.Count > 0).First().ErrorMessage));
			}

			var result = manager.Add(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult EditProvidedService(ServiceProvidedModel model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new MessageHandler(false, ModelState.Values.Select(x => x.Errors).First(x => x.Count > 0).First().ErrorMessage));
			}

			var result = manager.Edit(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult DeleteProvidedService(int id)
		{
			var result = manager.Delete(id);

			return Json(result);
		}

		[HttpPost]
		public JsonResult ActivateServiceProvided(int id)
		{
			var result = manager.Activate(id);

			return Json(result);
		}
	}
}