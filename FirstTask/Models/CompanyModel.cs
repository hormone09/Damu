using FirstTask.Resources;
using FirstTaskEntities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstTask.Models
{
	public class CompanyModel
	{
		public int Id { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[StringLength(300, MinimumLength = 6, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "CompnayNameLength")]
		public string Name { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[StringLength(12, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FormatBIN")]
		public string BIN { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public string Phone { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public DateTime? DateOfBegin { get; set; }

		public DateTime? DateOfFinish { get; set; }

		public Statuses Status { get; set; }

		public int TotalRows { get; set; }
	}
}