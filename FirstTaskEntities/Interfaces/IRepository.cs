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
		List<T> List(object queryList);
		void Add(T instanse);
		void Update(T instanse);
		void Remove(int id);
	}
}
