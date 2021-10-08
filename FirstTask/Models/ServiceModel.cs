using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Models
{
	public class ServiceModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public DateTime DateOfBegin { get; set; }
		public DateTime DateOfFinish { get; set; }
		public decimal Price { get; set; }
		public Statuses Status { get; set; }
	}
}