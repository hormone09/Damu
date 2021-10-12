using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Models
{
	public class EmployeeModel
	{
		public int Id { get; }
		public string PersonalNumber { get; set; }
		public string FullName { get; set; }
		public DateTime BirthdayDate { get; set; }
		public DateTime DateOfBegin { get; set; }
		public DateTime DateOfFinish { get; set; }
		public Company Company { get; set; }
		public string Phone { get; set; }
		public Statuses Status { get; set; }
	}
}