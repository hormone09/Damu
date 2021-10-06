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

namespace FirstTask.Controllers
{
    public class ServiceController : Controller
    {
		private ServiceManager manager;

		public ServiceController(ServiceManager manager)
		{
			this.manager = manager;
		}

		public ActionResult Index(int? page)
		{
			int pageCount = 0;
			ViewBag.Data = manager.GetPageResult(page, out pageCount);
			ViewBag.Page = page;
			ViewBag.PageCount = pageCount;

			return View();
		}

		[HttpPost]
		public string DeleteService(int id)
		{
			var result = manager.Delete(id);

			if (result)
				return JsonHelper.SerializeSuccess("Услуга успешно удалена!");
			else
				return JsonHelper.SerializeErorr("Произошла ошибка!");
		}
	}
}