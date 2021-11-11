using Entities.Query;
using System.Collections.Generic;

namespace Entities.Interfaces
{
	public interface IRepository<T>
	{
		// Базовый класс
		List<T> List(QueryListBase queryList);
		void Add(T instanse);
		void Update(T instanse);
		void Remove(int id);
	}
}
