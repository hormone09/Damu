using FirstTaskEntities.Models;
using System.Web.Mvc;
using FirstTask.Managers;
using FirstTask.ViewQueris;
using FirstTask.Models;
using System.Collections.Generic;

namespace FirstTask.Controllers
{
	public class ServiceController : Controller
	{
		private ServiceManager manager;

		public ServiceController(ServiceManager manager)
		{
			this.manager = manager;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Index(ServiceViewQuery query)
		{
			var result = manager.List(query);

			return Json(result);
		}

		[HttpGet]
		public JsonResult AddService(ServiceModel model)
		{
			var IsSucces = manager.Add(model);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно добавлена!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult EditService(ServiceModel model)
		{
			var IsSucces = manager.Edit(model);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно отредактирована!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult DeleteService(int id)
		{
			var IsSucces = manager.Delete(id);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно удалена!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}
	}
}