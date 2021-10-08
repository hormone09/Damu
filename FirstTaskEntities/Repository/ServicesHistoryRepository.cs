using Dapper;

using FirstTaskEntities.Interfaces;
using FirstTaskEntities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Repository
{
	public class ServicesHistoryRepository : IRepository<ServicesHistory>
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];
		public List<ServicesHistory> List(string query, object param)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<ServicesHistory>("SELECT * FROM ServicesHistory").ToList();
			}
		}

		public void Add(ServicesHistory history)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO ServicesHistory (ServiceName, CompanyName, EmployeeName) VALUES (@Service, @Company, @Employee)";
				connection.Query(query, new { Service = history.ServiceName, Company = history.CompanyName, Employee = history.EmployeeName });
			}
		}

		public void Update(ServicesHistory history)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServicesHistory SET ServiceName = @Service, CompanyName = @Company, EmployeeName = @Employee";
				connection.Query<ServicesHistory>(query, new { Id = history.Id, Service = history.ServiceName, Company = history.CompanyName, Employee = history.EmployeeName });
			}
		}

		// TODO: ???
		public void Remove(int id)
		{
			/*using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServicesHistory SET Status = @Status";
				connection.Query<Service>(query, new { Id = id, ServiceStatuses.Disabled });
			}*/
		}
		public int GetCount(string query, object param)
		{
			return 1;
		}
	}
}
