using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Interfaces
{
	public interface ICompanyRepository
	{
		List<Company> GetAll();
		void Add(Company company);
		void Update(Company company, int id);
		void Remove(int id);
	}
}
