using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Models
{
	public class Service
	{
		public int Id { get; set; }
		public string Name { get; set; }

		// TODO: MASK
		public string Code { get; set; }

		public DateTime DateOfBegin { get; set; }

		public decimal Price { get; set; }
	}
}