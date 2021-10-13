using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Query
{
	public class EmployeeQueryList : QueryListBase
	{
		public int CompanyId { get; set; }
		public string FullName { get; set; }
		public DateTime DateOfBegin { get; set; }
	}
}
