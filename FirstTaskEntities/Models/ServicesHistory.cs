using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Models
{
	public class ServicesHistory
	{
		public int Id { get; set; }
		public string ServiceName { get; set; }
		public string CompanyName { get; set; }
		public string EmployeeName { get; set; }
	}
}
