using FirstTask.Managers;
using FirstTask.ViewModels;

using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstTask.Controllers
{
    public class CompanyController : Controller
	{
		private CompanyManager manager;

		public CompanyController(CompanyManager manager)
		{
			this.manager = manager;
		}
		public ActionResult Index()
        {
            return View();
		}

		[HttpPost]
		public JsonResult Index(CompanyViewModel model)
		{
			var result = manager.GetCompanies(model);

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult AddService(Company company)
		{
			var IsSucces = manager.Add(company);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Данные компании успешно добавлены!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult EditService(Company company)
		{
			var IsSucces = manager.Edit(company);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Данные компании успешно отредактированы!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult DeleteService(int id)
		{
			var IsSucces = manager.Delete(id);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Данные компании успешно удалены!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}
	}
}