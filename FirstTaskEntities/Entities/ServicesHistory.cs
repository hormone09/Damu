using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Models
{
	public class ServicesHistory
	{
		public int Id { get; set; }

		/// <summary>
		/// Название услуги
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// Компания, оказавшая услугу
		/// </summary>
		public string CompanyName { get; set; }

		/// <summary>
		/// Сотрудник, оказавший услугу
		/// </summary>
		public string EmployeeName { get; set; }
	}
}
