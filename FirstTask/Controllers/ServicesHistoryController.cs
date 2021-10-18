using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstTask.Controllers
{
    public class ServicesHistoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


		public JsonResult LIST(object model)
		{
			var obj = new { Id = 1, Start = DateTime.Now, End = DateTime.Now.AddMinutes(15), Company = new { Name = "Компания" }, Service = new { Name = "Услуга" }, Employee = new { Name = "Сотрудник" } };

			return Json(obj, JsonRequestBehavior.AllowGet);
		}
    }
}