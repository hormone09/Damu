using FirstTaskEntities.Models;
using System.Web.Mvc;
using FirstTask.Managers;
using FirstTask.Models;
using FirstTask.ViewQueris;
using FirstTask.Handlers;
using System.Linq;

namespace FirstTask.Controllers
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
		public JsonResult AddEmployee(EmployeeModel model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new MessageHandler(false, ModelState.Values.Select(x => x.Errors).First(x => x.Count > 0).First().ErrorMessage));
			}

			var result = manager.Add(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult EditEmployee(EmployeeModel model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new MessageHandler(false, ModelState.Values.Select(x => x.Errors).First(x => x.Count > 0).First().ErrorMessage));
			}

			var result = manager.Edit(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult DeleteEmployee(int id)
		{
			var result = manager.Delete(id);

			return Json(result);
		}

		[HttpPost]
		public JsonResult ActivateEmployee(int id)
		{
			var result = manager.Activate(id);

			return Json(result);
		}
	}
}