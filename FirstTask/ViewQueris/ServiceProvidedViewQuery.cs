namespace FirstTask.ViewQueris
{
	public class ServiceProvidedViewQuery : ViewQueryBase
	{
		/// <summary>
		/// Идентификатор компании, оказывающей услуги
		/// </summary>
		public int? CompanyId { get; set; }

		/// <summary>
		/// Идентификатор оказываемой услуги
		/// </summary>
		public int? ServiceId { get; set; }
	}
}