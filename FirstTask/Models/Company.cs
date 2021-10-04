using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstTask.Models
{
	public class Company
	{
		public int Id { get; set; }

		public string Name { get; set; }

		[MaxLength(12)]
		[MinLength(12)]
		public string BIN { get; set; }

		// TODO: MASK
		public string Phone { get; set; }

		public DateTime DateOfBegin { get; set; }
	}
}