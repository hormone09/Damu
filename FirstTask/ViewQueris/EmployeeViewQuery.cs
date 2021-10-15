using FirstTask.Models;

using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewQueris
{
	public class EmployeeViewQuery : ViewQueryBase
	{
		public int? Id { get; set; }
		public string FullName { get; set; }
		public int? CompanyId { get; set; }
	}
}