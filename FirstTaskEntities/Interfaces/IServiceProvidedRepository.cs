using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Interfaces
{
	public interface IServiceProvidedRepository
	{
		List<ServiceProvided> GetAll();
		void Add(ServiceProvided service);
		void Update(ServiceProvided service, int id);
		void Remove(int id);
	}
}
