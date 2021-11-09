using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Query
{
	public class CompanyQueryList : QueryListBase
	{
		/// <summary>
		/// Название компании
		/// </summary>
		public string CompanyName { get; set; }
	}
}
