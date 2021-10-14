using FirstTask.Managers;
using FirstTask.Models;
using FirstTask.ViewQueris;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult AddProvidedService(ServiceProvidedModel model)
		{
			model.Company = new CompanyModel { Id = 4 };
			model.Service = new ServiceModel { Id = 4 };

			var IsSucces = manager.Add(model);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно добавлена!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult EditProvidedService(ServiceProvidedModel model)
		{
			model.Company = new CompanyModel { Id = 4 };
			model.Service = new ServiceModel { Id = 4 };

			var IsSucces = manager.Edit(model);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно отредактирована!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult DeleteProvidedService(int id)
		{
			var IsSucces = manager.Delete(id);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно удалена!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}
	}
}