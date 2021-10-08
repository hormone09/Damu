using FirstTaskEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Query
{
	public class ServiceListQuery
	{
		public int Limit { get; set; }
		public int Skip { get; set; }
		public string ServiceName { get; set; }
		public decimal Price { get; set; }
		public Statuses? Status { get; set; }
		public DateTime Date1 { get; set; }
		public DateTime Date2 { get; set; }
		public int RowNumber { get; set; }
	}
}
