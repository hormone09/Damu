using System.Web.Mvc;
using FirstTask.Managers;
using FirstTask.Models;
using FirstTask.ViewQueris;

namespace FirstTask.Controllers
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
		public JsonResult Index(ServicesHistoryViewQuery queryView)
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
			query.Path = Server.MapPath("~/Content/Reports/ServicesHistoryReport.mrt");
			var report = manager.GetReport(query);

			return report;
		}
    }
}