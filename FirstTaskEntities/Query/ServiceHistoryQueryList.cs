using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Query
{
	public class ServiceHistoryQueryList : QueryListBase
	{
		/// <summary>
		/// Дата периодоа, в который были оказанны услуги,
		/// в SQL запросе брать от сюда только месяц
		/// </summary>
		public DateTime DatePeriod { get; set; }
	}
}
