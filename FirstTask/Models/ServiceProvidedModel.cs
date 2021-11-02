using FirstTask.Resources;
using FirstTaskEntities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstTask.Models
{
	public class ServiceProvidedModel
	{
		public int Id { get; set; }
		public ServiceModel Service{ get; set; }
		public CompanyModel Company { get; set; }

		[Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "IsRequired")]
		public decimal? ServicePrice { get; set; }

		[Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "IsRequired")]
		public DateTime? DateOfBegin { get; set; }
		public DateTime? DateOfFinish { get; set; }
		public Statuses Status { get; set; }
		public int TotalRows { get; set; }
	}
}