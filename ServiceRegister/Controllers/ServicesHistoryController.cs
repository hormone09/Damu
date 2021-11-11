using System.Configuration;
using System.Linq;
using System.Web.Mvc;

using ServiceRegister.Handlers;
using ServiceRegister.Managers;
using ServiceRegister.Models;
using ServiceRegister.ViewQueris;

namespace ServiceRegister.Controllers
{
	public class ServicesHistoryController : Controller
	{
		private ServiceHistoryManager manager;

		public ServicesHistoryController(ServiceHistoryManager manager)
		{
			this.manager = manager;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult List(ServicesHistoryViewQuery queryView)
		{
			var models = manager.List(queryView);

			return Json(models);
		}

		[HttpPost]
		public JsonResult Create(ServiceHistoryModel model)
		{
			var message = manager.Add(model);

			return Json(message);
		}

		[HttpPost]
		public JsonResult Update(ServiceHistoryModel model)
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
			string pathString = ConfigurationManager.AppSettings["ReportSourcePath"];
			query.Path = Server.MapPath("~/Content/Reports/ServicesHistoryReport.mrt");

			var report = manager.GetReport(query);

			return report;
		}
    }
}