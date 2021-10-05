using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTaskEntities.Models
{
	public class Service
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Company company { get; set; }

		// TODO: MASK
		public string Code { get; set; }

		public DateTime DateOfBegin { get; set; }
		public DateTime DateOfFinish { get; set; }

		public decimal Price { get; set; }

		public ServiceStatuses Status { get; set; }
	}
}