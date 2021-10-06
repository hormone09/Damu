using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Interfaces
{
	public interface IRepository<T>
	{
		List<T> List(string query, object param);
		void Add(T company);
		void Update(T company);
		void Remove(int id);
	}
}
