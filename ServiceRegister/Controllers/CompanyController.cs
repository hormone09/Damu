using ServiceRegister.Handlers;
using ServiceRegister.Managers;
using ServiceRegister.Models;
using ServiceRegister.ViewQueris;
using System.Linq;
using System.Web.Mvc;

namespace ServiceRegister.Controllers
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
		public JsonResult List(CompanyViewQuery query)
		{
			var result = manager.List(query);

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult Add([Bind(Exclude = "Status")]CompanyModel model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new MessageHandler(false, ModelState.Values.Select(x => x.Errors).First(x => x.Count > 0).First().ErrorMessage));
			}

			var result = manager.Add(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult Edit(CompanyModel model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new MessageHandler(false, ModelState.Values.Select(x => x.Errors).First(x => x.Count > 0).First().ErrorMessage));
			}

			var result = manager.Edit(model);

			return Json(result);
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			var result = manager.Delete(id);

			return Json(result);
		}

		[HttpPost]
		public JsonResult Activate(int id)
		{
			var result = manager.Activate(id);

			return Json(result);
		}
	}
}