using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTaskEntities.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string PersonalNumber { get; set; }
		public string FullName { get; set; }
		public DateTime BirthdayDate { get; set; }
		public Company company { get; set; }
		// TODO: MASK
		public string Phone { get; set; }
	}
}