using ServiceRegister.Handlers;
using ServiceRegister.Managers;
using ServiceRegister.Models;
using ServiceRegister.ViewQueris;

using System.Linq;
using System.Web.Mvc;

namespace ServiceRegister.Controllers
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
		public JsonResult List(ServiceProvidedViewQuery query)
		{
			var result = manager.List(query);

			return Json(result);
		}

		[HttpPost]
		public JsonResult Add([Bind(Exclude = "Status")] ServiceProvidedModel model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new MessageHandler(false, ModelState.Values.Select(x => x.Errors).First(x => x.Count > 0).First().ErrorMessage));
			}

			var result = manager.Add(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult Edit(ServiceProvidedModel model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new MessageHandler(false, ModelState.Values.Select(x => x.Errors).First(x => x.Count > 0).First().ErrorMessage));
			}

			var result = manager.Edit(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			var result = manager.Delete(id);

			return Json(result);
		}

		[HttpPost]
		public JsonResult Activate(int id)
		{
			var result = manager.Activate(id);

			return Json(result);
		}
	}
}