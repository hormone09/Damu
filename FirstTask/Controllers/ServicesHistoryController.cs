using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FirstTask.ViewQueris;

using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

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

		
		public ActionResult GetReport(ReportViewQuery query)
		{
			string filterEmpty = "'Не назначено'";
			string companyName = filterEmpty, serviceName = filterEmpty, employeeName = filterEmpty;

			if (query.ServiceId != null)
				serviceName = query.ServiceName;
			if (query.EmployeeId != null)
				employeeName = query.EmployeeName;
			if (query.CompanyId != null)
				companyName = query.CompanyName;

			var reportPath = Server.MapPath("~/Content/Reports/ServicesHistoryReport.mrt");
			var report = new StiReport(); 
			report.Load(reportPath);

			/*report["CompanyId"] = query.CompanyId;
			report["EmployeeId"] = query.EmployeeId;
			report["ServiceId"] = query.ServiceId;
			report["CompanyName"] = query.CompanyName;
			report["ServiceName"] = query.ServiceName;
			report["EmployeeName"] = query.EmployeeName;
			report["Date"] = query.Date;*/

			report["CompanyId"] = 6;
			report["EmployeeId"] = 5;
			report["ServiceId"] = 4;
			report["CompanyName"] = "213123";
			report["ServiceName"] = "asdasd";
			report["EmployeeName"] = "asdasd";
			report["Date"] = DateTime.Now;

			return StiMvcReportResponse.ResponseAsPdf(report);
		}
    }
}