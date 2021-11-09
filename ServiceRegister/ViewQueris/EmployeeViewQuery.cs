namespace ServiceRegister.ViewQueris
{
	public class EmployeeViewQuery : ViewQueryBase
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int? Id { get; set; }

		/// <summary>
		/// Полное имя
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// Идентификатор организации, к которой относится сотрудник
		/// </summary>
		public int? CompanyId { get; set; }
	}
}