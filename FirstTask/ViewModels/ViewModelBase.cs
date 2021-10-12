using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewModels
{
	public class ViewModelBase
	{
		public int? Page { get; set; }
		public int PageSize { get; set; }
		public int RowNumber { get; set; }
		public Statuses Status { get; set; }
		public string SortingType { get; set; }
	}
}