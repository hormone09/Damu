using FirstTask.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.ViewQueris
{
	public class ServiceProvidedViewQuery : ViewQueryBase
	{
		public CompanyModel company { get; set; }
		public ServiceModel service { get; set; }
	}
}