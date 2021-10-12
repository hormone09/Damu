using FirstTaskEntities.Models;
using System.Web.Mvc;
using FirstTask.Managers;
using FirstTask.ViewModels;

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
		public JsonResult Index(ServiceViewModel model)
		{
			var result = manager.GetServices(model);

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult AddService(Service service)
		{
			var IsSucces = manager.Add(service);

			if (IsSucces)
				return Json(new { IsSuccess = true, Message = "Услуга успешно добавлена!" }, JsonRequestBehavior.AllowGet);
			else
				return Json(new { IsSuccess = false, Error = "Произошла ошибка!" }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult EditService(Service service)
		{
			var IsSucces = manager.Edit(service);

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