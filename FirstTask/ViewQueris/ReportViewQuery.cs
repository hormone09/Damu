using FirstTask.Enums;
using System;

namespace FirstTask.ViewQueris
{
	public class ReportViewQuery
	{
		/// <summary>
		/// Идентификатор организции, отчет по которой требуется выполнить
		/// </summary>
		public int? CompanyId { get; set; }

		/// <summary>
		/// Идентификатор услуги, отчет по которой требуется выполнить
		/// </summary>
		public int? ServiceId { get; set; }

		/// <summary>
		/// Идентификатор сотрудника, отчет по которому требуется выполнить
		/// </summary>
		public int? EmployeeId { get; set; }

		/// <summary>
		/// Расширение конечного файла
		/// </summary>
		public ReportTypesEnum Type { get; set; }
		
		/// <summary>
		/// Верхняя временная граница отчета
		/// </summary>
		public DateTime? DateBegin { get; set; }

		/// <summary>
		/// Нижняя временная граница отчета
		/// </summary>
		public DateTime? DateEnd { get; set; }

		public string Path { get; set; }
	}
}