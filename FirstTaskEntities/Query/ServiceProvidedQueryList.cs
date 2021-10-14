using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Query
{
	public class ServiceProvidedQueryList : QueryListBase
	{
		public int? CompanyId { get; set; }
		public int? ServiceId { get; set; }
	}
}
