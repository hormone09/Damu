using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Models
{
	public class CompnayService
	{
		public int Id { get; set; }
		public Company company { get; set; }
		public Service service { get; set; }
		public decimal Price { get; set; }
		public DateTime DateOfBegin { get; set; }
	}
}