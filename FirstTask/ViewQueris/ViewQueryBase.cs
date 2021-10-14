using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewQueris
{
	public class ViewQueryBase
	{
		public int? Page { get; set; }
		public int PageSize { get; set; }
		public int TotalRows { get; set; }
		public Statuses Status { get; set; }
		public string SortingType { get; set; }
	}
}