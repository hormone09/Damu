﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Query
{
	public class CompanyQueryList : QueryListBase
	{
		public int? Id { get; set; }
		public string CompanyName { get; set; }
	}
}
