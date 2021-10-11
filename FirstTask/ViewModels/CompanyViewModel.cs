using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewModels
{
	public class CompanyViewModel : ViewModelBase
	{
		public string CompanyName { get; set; }
		public Statuses Status { get; set; }
		public List<Company> Items { get; set; }
	}
}