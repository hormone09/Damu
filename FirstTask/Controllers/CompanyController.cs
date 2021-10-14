using FirstTask.Managers;
using FirstTask.Models;
using FirstTask.ViewQueris;

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
		public JsonResult Index(CompanyViewQuery query)
		{
			var result = manager.List(query);

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult AddCompany(CompanyModel model)
		{
			var IsSucces = manager.Add(model);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Данные компании успешно добавлены!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult EditCompany(CompanyModel model)
		{
			var IsSucces = manager.Edit(model);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Данные компании успешно отредактированы!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult DeleteCompany(int id)
		{
			var IsSucces = manager.Delete(id);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Данные компании успешно удалены!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}
	}
}