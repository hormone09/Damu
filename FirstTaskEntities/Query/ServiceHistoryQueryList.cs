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
		/// Дата начала недели, выбранной в расписании
		/// </summary>
		public DateTime DateBegin { get; set; }

		/// <summary>
		/// Дата конца недели, выбранной в расписании
		/// </summary>
		public DateTime DateEnd { get; set; }
	}
}
