using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTaskEntities.Models
{
	public class ServiceCompleted
	{
		public int Id { get; set; }

		public Company Company { get; set; }

		// TODO: MASK
		public Service Service { get; set; }

		public Employee Employee { get; set; }

		public DateTime Date { get; set; }
	}
}