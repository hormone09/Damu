using FirstTask.Resources;
using FirstTaskEntities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstTask.Models
{
	public class EmployeeModel
	{
		public int Id { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[StringLength(12, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FormatBIN")]
		public string PersonalNumber { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[StringLength(300, MinimumLength = 10, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmployeeNameLength")]
		public string FullName { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public DateTime? BirthdayDate { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public DateTime? DateOfBegin { get; set; }
		public DateTime? DateOfFinish { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public CompanyModel Company { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public string Phone { get; set; }
		public Statuses Status { get; set; }
		public int TotalRows { get; set; }
	}
}