﻿using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Query
{
	public abstract class QueryListBase
	{
		public Statuses Status { get; set; }
		public int Limit { get; set; }
		public int Skip { get; set; }
		public int RowNumber { get; set; }
		public string SortingType { get; set; }
	}
}