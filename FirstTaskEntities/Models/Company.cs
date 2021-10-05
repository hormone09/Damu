using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstTaskEntities.Models
{
	public class Company
	{
		/// <summary>
		/// 
		/// </summary>
		public int Id { get; set; }

		public string Name { get; set; }

		[MaxLength(12)]
		[MinLength(12)]
		public string BIN { get; set; }

		// TODO: MASK
		public string Phone { get; set; }

		public DateTime DateOfBegin { get; set; }
		public List<Employee> Employees { get; set; }
	}
}