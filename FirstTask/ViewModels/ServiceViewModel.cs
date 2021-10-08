using FirstTask.Models;

using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewModels
{
	public class ServiceViewModel
	{
		public int? Page { get; set; }
		public string ServiceName { get; set; }
		public decimal Price { get; set; }
		public Statuses? Status { get; set; }
		public DateTime Date1 { get; set; }
		public DateTime Date2 { get; set; }
		public List<Service> Items { get; set; }
		public int Limit { get; set; }
		public int RowNumber { get; set; }
	}
}