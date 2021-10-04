using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string PersonalNumber { get; set; }
		public string FullName { get; set; }
		public DateTime BirthdayDate { get; set; }
		
		// TODO: MASK
		public string Number { get; set; }
	}
}