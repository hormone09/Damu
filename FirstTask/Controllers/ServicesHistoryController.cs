using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FirstTask.Enums;
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
			var obj = new { Id = 1, Start = DateTime.Parse("20.10.2021 07:00:00"), End = DateTime.Parse("20.10.2021 07:15:00"), Company = new { Name = "Компания" }, Service = new { Name = "Услуга" }, Employee = new { Name = "Сотрудник" } };

			return Json(obj, JsonRequestBehavior.AllowGet);
		}
			
		[HttpGet]
		public ActionResult GetReport(ReportViewQuery query)
		{
			if (query.Date == null)
				query.Date = DateTime.Now;

			string filterEmpty = "'Все'";
			string hasFilter = "Задана";
			string companyName = filterEmpty, serviceName = filterEmpty, employeeName = filterEmpty;

			if (query.ServiceId != null)
				serviceName = hasFilter;
			if (query.EmployeeId != null)
				employeeName = hasFilter;
			if (query.CompanyId != null)
				companyName = hasFilter;

			var reportPath = Server.MapPath("~/Content/Reports/ServicesHistoryReport.mrt");
			var report = new StiReport(); 
			report.Load(reportPath);

			report["Date"] = query.Date;
			report["CompanyId"] = query.CompanyId;
			report["EmployeeId"] = query.EmployeeId;
			report["ServiceId"] = query.ServiceId;
			report.Dictionary.Variables["ServiceFilter"].Value = serviceName;
			report.Dictionary.Variables["CompanyFilter"].Value = companyName;
			report.Dictionary.Variables["EmployeeFilter"].Value = employeeName;
			report.Dictionary.Variables["Date"].Value = ((DateTime)query.Date).ToLongDateString();

			switch (query.Type)
			{
				case ReportTypes.EXEL:
					return StiMvcReportResponse.ResponseAsExcel2007(report);
				case ReportTypes.WORD:
					return StiMvcReportResponse.ResponseAsWord2007(report);
				case ReportTypes.PDF:
					return StiMvcReportResponse.ResponseAsPdf(report);
				case ReportTypes.XML:
					return StiMvcReportResponse.ResponseAsXml(report);
				case ReportTypes.PNG:
					return StiMvcReportResponse.ResponseAsPng(report);
				default: 
					return StiMvcReportResponse.ResponseAsPdf(report);
			}
		}
    }
}