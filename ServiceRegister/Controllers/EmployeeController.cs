using Entities.Models;
using System.Web.Mvc;
using ServiceRegister.Managers;
using ServiceRegister.Models;
using ServiceRegister.ViewQueris;
using ServiceRegister.Handlers;
using System.Linq;

namespace ServiceRegister.Controllers
{
	public class EmployeeController : Controller
	{
		private EmployeeManager manager;

		public EmployeeController(EmployeeManager manager)
		{
			this.manager = manager;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Index(EmployeeViewQuery query)
		{
			var result = manager.List(query);

			return Json(result);
		}

		[HttpPost]
		public JsonResult Add(EmployeeModel model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new MessageHandler(false, ModelState.Values.Select(x => x.Errors).First(x => x.Count > 0).First().ErrorMessage));
			}

			var result = manager.Add(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult Edit(EmployeeModel model)
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