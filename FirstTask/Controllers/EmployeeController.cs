using FirstTaskEntities.Models;
using System.Web.Mvc;
using FirstTask.Managers;
using FirstTask.ViewModels;

namespace FirstTask.Controllers
{
	public class EmployeeController : Controller
	{
		private EmployeeManager manager;

		public EmployeeController(EmployeeManager manager)
		{
			this.manager = manager;
		}

		public JsonResult Index()
		{
			return Json(manager.GetEmployeies(new EmployeeViewModel { Page = 1, PageSize = 20}).Items, JsonRequestBehavior.AllowGet);
		}
	}
}