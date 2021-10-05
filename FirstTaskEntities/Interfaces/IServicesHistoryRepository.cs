using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Interfaces
{
	public interface IServicesHistoryRepository
	{
		List<ServicesHistory> GetAll();
		void Add(ServicesHistory history);
		void Update(ServicesHistory history, int id);
		//void Remove(int id);
	}
}
