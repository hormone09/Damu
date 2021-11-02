using FirstTask.Resources;
using FirstTaskEntities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstTask.Models
{
	public class ServiceModel
	{
		public int Id { get; set; }

		[Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "IsRequired")]
		[StringLength(300, MinimumLength = 6, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "EmployeeNameLength")]
		public string Name { get; set; }

		[Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "IsRequired")]
		[RegularExpression("[A-z]{1}[0-9]{2}.[0-9]{3}.[0-9]{3}$", ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "FormatServiceCode")]
		public string Code { get; set; }

		[Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "IsRequired")]
		public DateTime? DateOfBegin { get; set; }
		public DateTime? DateOfFinish { get; set; }

		[Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "IsRequired")]
		public decimal Price { get; set; }
		public Statuses Status { get; set; }
		public int TotalRows { get; set; }
	}
}