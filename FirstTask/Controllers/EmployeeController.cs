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

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult AddEmployee(EmployeeModel model)
		{
			var IsSucces = manager.Add(model);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно добавлена!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult EditEmployee(EmployeeModel model)
		{
			var IsSucces = manager.Edit(model);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно отредактирована!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult DeleteEmployee(int id)
		{
			var IsSucces = manager.Delete(id);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно удалена!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}
	}
}