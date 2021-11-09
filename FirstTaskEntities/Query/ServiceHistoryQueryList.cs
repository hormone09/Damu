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
		/// Верхняя временная граница для выгрузки записей
		/// </summary>
		public DateTime DateBegin { get; set; }

		/// <summary>
		/// Нижняя временная граница для выгрузки записей
		/// </summary>
		public DateTime DateEnd { get; set; }
	}
}
