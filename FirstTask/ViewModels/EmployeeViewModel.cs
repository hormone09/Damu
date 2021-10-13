using FirstTask.Models;

using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewModels
{
	public class EmployeeViewModel : ViewModelBase
	{
		public string FullName { get; set; }
		public List<EmployeeModel> Items { get; set; }
	}
}