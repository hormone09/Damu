using FirstTaskEntities.Models;
using FirstTaskEntities.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FirstTask.Helpers;
using FirstTaskEntities.Enums;
using FirstTask.Managers;
using FirstTaskEntities.Interfaces;
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
			var result = manager.GetPageResult(model);

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public string AddService(Service service)
		{
			var IsSucces = manager.Add(service);

			if (IsSucces)
				return JsonHelper.SerializeSuccess("Услуга успешно добавлена!");
			else
				return JsonHelper.SerializeErorr("Произошла ошибка!");
		}

		[HttpGet]
		public string EditService(Service service)
		{
			var IsSucces = manager.Edit(service);

			if (IsSucces)
				return JsonHelper.SerializeSuccess("Услуга успешно отредактирована!");
			else
				return JsonHelper.SerializeErorr("Произошла ошибка!");
		}

		[HttpPost]
		public string DeleteService(int id)
		{
			var IsSucces = manager.Delete(id);

			if (IsSucces)
				return JsonHelper.SerializeSuccess("Услуга успешно удалена!");
			else
				return JsonHelper.SerializeErorr("Произошла ошибка!");
		}
	}
}