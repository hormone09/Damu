using FirstTask.Models;

using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewModels
{
	public class ServiceViewModel : ViewModelBase
	{
		public string ServiceName { get; set; }
		public decimal Price { get; set; }
		public Statuses Status { get; set; }
		public string SortingType { get; set; }
		public List<Service> Items { get; set; }
	}
}