using FirstTaskEntities.Enums;
using System;

namespace FirstTask.Models
{
	public class ServiceHistoryModel
	{
		public int Id { get; set; }
		public ServiceModel Service { get; set; }
		public CompanyModel Company { get; set; }
		public EmployeeModel Employee { get; set; }
		public DateTime? DateOfCreate { get; set; }
		public DateTime? DateOfFinish { get; set; }
		public DateTime? DateOfDelete { get; set; }
		public Statuses Status { get; set; }
		public string Title { get; set; }
	}
}