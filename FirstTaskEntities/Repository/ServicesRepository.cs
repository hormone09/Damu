using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FirstTaskEntities.Models;
using System.Configuration;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Interfaces;

namespace FirstTaskEntities.Repository
{
	public class ServicesRepository : IServicesRepository
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<Service> GetAll()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<Service>("SELECT * FROM Services").ToList();
			}
		}

		public void Add(Service service)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO Services (Name, Price, Code, DateOfBegin, DateOfFinish, Status) VALUES (@Name, @Price, @Code, @DateOfBegin, null, @Status)";
				connection.Query(query, new { Name = service.Name, Price = service.Price, Code = service.Code, DateOfBegin = service.DateOfBegin, Status = service.Status });
			}
		}

		public void Update(Service service, int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Services SET Name = @Name, Code = @Code, DateOfBegin = @Date, Price = @Price WHERE Id= @Id";
				connection.Query<Service>(query, new { Id = id, Name = service.Name, Code = service.Code, Date = service.DateOfBegin, Price = service.Price });
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Services SET Status = @Status";
				connection.Query<Service>(query, new { Id = id, ServiceStatuses.Disabled });
			}
		}
	}
}