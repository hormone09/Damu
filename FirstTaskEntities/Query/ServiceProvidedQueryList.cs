using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Query
{
	public class ServiceProvidedQueryList : QueryListBase
	{
		/// <summary>
		/// Идентификатор организации, оказывающей услугу
		/// </summary>
		public int? CompanyId { get; set; }

		/// <summary>
		/// Идентификатор оказываемой услуги
		/// </summary>
		public int? ServiceId { get; set; }
	}
}
