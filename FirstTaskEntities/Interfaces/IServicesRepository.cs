using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Interfaces
{
	public interface IServicesRepository
	{
		List<Service> GetAll();
		void Add(Service service);
		void Update(Service service, int id);
		void Remove(int id);
	}
}
