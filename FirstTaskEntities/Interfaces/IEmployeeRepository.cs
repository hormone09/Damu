using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Interfaces
{
	public interface IEmployeeRepository
	{
		List<Employee> GetAll();
		void Add(Employee employee);
		void Update(Employee employee, int id);
		void Remove(int id);
	}
}
