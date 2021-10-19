using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewQueris
{
	public class ReportViewQuery
	{
		public int CompanyId { get; set; }
		public int ServiceId { get; set; }
		public int EmployeeId { get; set; }
		public string CompanyName { get; set; }
		public string ServiceName { get; set; }
		public string EmployeeName { get; set; }
		public DateTime Date { get; set; }
	}
}