namespace FirstTask.ViewQueris
{
	public class EmployeeViewQuery : ViewQueryBase
	{
		public int? Id { get; set; }
		public string FullName { get; set; }
		public int? CompanyId { get; set; }
	}
}