﻿using FirstTask.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewQueris
{
	public class ReportViewQuery
	{
		public int? CompanyId { get; set; }
		public int? ServiceId { get; set; }
		public int? EmployeeId { get; set; }
		public ReportTypes Type { get; set; }
		public DateTime? Date { get; set; }
	}
}