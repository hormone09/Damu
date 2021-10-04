using FirstTask.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace FirstTask.Repository
{
	public class ApplicationContext
	{
		private string connectionString = "Server=DESKTOP-8LKEMKN; Database=FirstTask; Trusted_Connection=True;";

		public ApplicationContext()
		{
			using (var conntection = new SqlConnection(connectionString))
			{
				var companyCount = conntection.Query<Company>("SELECT COUNT(*) FROM Companies");
				var serviceCount = conntection.Query<Service>("SELECT * FROM Services").Count();
				/*var companyServiceCount = conntection.Query<CompnayService>("SELECT COUNT(*) FROM");
				var employeeCount = conntection.Query<Employee>("SELECT COUNT(*) FROM");*/

				if(serviceCount < 30)
				{
					var temp = 30 - serviceCount;

					for(int i=0; i<temp; i++)
					{
						conntection.Query("INSERT INTO Services(Name, Code, DateOfBegin, Price) VALUES ('Name', 'Code', '12.10.2001', 560.23)");
					}
				}
			}
		}

		public List<Company> GetAllCompanies()
		{
			using (var conntection = new SqlConnection(connectionString))
			{
				return conntection.Query<Company>("SELECT * FROM Companies").ToList();
			}
		}

		public List<Service> GetAllServices()
		{
			using (var conntection = new SqlConnection(connectionString))
			{
				return conntection.Query<Service>("SELECT * FROM Services").ToList();
			}
		}
	}
}