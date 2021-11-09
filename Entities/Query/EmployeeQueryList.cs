using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Query
{
	public class EmployeeQueryList : QueryListBase
	{
		/// <summary>
		/// Идентификатор компании, к которой относится сотрудник
		/// </summary>
		public int? CompanyId { get; set; }

		/// <summary>
		/// Полное имя
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// Дата начала работы
		/// </summary>
		public DateTime DateOfBegin { get; set; }
	}
}
