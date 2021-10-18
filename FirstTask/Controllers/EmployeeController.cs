using FirstTaskEntities.Models;
using System.Web.Mvc;
using FirstTask.Managers;
using FirstTask.Models;
using FirstTask.ViewQueris;

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
			var result = manager.Add(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult EditEmployee(EmployeeModel model)
		{
			var result = manager.Edit(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult DeleteEmployee(int id)
		{
			var result = manager.Delete(id);

			return Json(result);
		}
	}
}