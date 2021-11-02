using System.Web.Mvc;

namespace FirstTask.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}