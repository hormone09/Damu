using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Models
{
	public class ServiceProvidedModel
	{
		public int Id { get; set; }
		public ServiceModel Service{ get; set; }
		public CompanyModel Company { get; set; }
		public decimal? ServicePrice { get; set; }
		public DateTime DateOfBegin { get; set; }
		public DateTime? DateOfFinish { get; set; }
		public Statuses Status { get; set; }
		public int TotalRows { get; set; }
	}
}