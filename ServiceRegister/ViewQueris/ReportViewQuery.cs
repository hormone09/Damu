using ServiceRegister.Enums;
using ServiceRegister.Models;

using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceRegister.ViewQueris
{
	public class ReportViewQuery
	{
		/// <summary>
		/// Идентификатор организции, отчет по которой требуется выполнить
		/// </summary>
		public CompanyModel Company { get; set; }

		/// <summary>
		/// Идентификатор услуги, отчет по которой требуется выполнить
		/// </summary>
		public ServiceModel Service { get; set; }

		/// <summary>
		/// Идентификатор сотрудника, отчет по которому требуется выполнить
		/// </summary>
		public EmployeeModel Employee { get; set; }

		/// <summary>
		/// Расширение конечного файла
		/// </summary>
		public ReportTypesEnum Type { get; set; }

		/// <summary>
		/// Верхняя временная граница отчета
		/// </summary>
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
		public DateTime? DateBegin { get; set; }

		/// <summary>
		/// Нижняя временная граница отчета
		/// </summary>
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
		public DateTime? DateEnd { get; set; }

		public string Path { get; set; }
	}
}