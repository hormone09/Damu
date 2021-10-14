using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Models
{
	public class CompanyModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string BIN { get; set; }
		public string Phone { get; set; }
		public DateTime DateOfBegin { get; set; }
		public DateTime? DateOfFinish { get; set; }
		public Statuses Status { get; set; }
		public int TotalRows { get; set; }
	}
}