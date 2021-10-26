using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FirstTask.Enums;
using FirstTask.Managers;
using FirstTask.Models;
using FirstTask.ViewQueris;

namespace FirstTask.Controllers
{
	public class ServicesHistoryController : Controller
	{
		private ServicesHistoryManager manager;

		public ServicesHistoryController(ServicesHistoryManager manager)
		{
			this.manager = manager;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Index(ServicesHistoryViewQuery queryView)
		{
			var models = manager.List(queryView);

			return Json(models);
		}

		[HttpPost]
		public JsonResult Create(ServicesHistoryModel model)
		{
			var message = manager.Add(model);

			return Json(message);
		}

		[HttpPost]
		public JsonResult Update(ServicesHistoryModel model)
		{
			var message = manager.Update(model);

			return Json(message);
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			var message = manager.Remove(id);

			return Json(message);
		}

		[HttpGet]
		public ActionResult GetReport(ReportViewQuery query)
		{
			query.Path = Server.MapPath("~/Content/Reports/ServicesHistoryReport.mrt");
			var report = manager.GetReport(query);

			return report;
		}
    }
}