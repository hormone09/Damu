using FirstTask.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewQueris
{
	public class ServiceProvidedViewQuery : ViewQueryBase
	{
		public int? CompanyId { get; set; }
		public int? ServiceId { get; set; }
	}
}