using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FirstTaskEntities.Models;
using System.Configuration;

namespace FirstTaskEntities.Repository
{
	public class ServicesRepository
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
				string query = "INSERT INTO Services (Name, Price, Code, DateOfBegin, DateOfFinish) VALUES (@Name, @Price, @Code, @DateOfBegin, null)";
				connection.Query(query, new { Name = service.Name, Price = service.Price, Code = service.Code, DateOfBegin = service.DateOfBegin });
			}
		}

		public void Update(Service service, int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Services SET Name = @Name, Code = @Code, DateOfBegin = @Date, Price = @Price, Status = @Status WHERE Id= @Id";
				connection.Query<Service>(query, new { Id = id, Name = service.Name, Code = service.Code, Date = service.DateOfBegin, Price = service.Price, Status = service.Status });
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Services SET Status = 2 WHERE Id = @Id";
				connection.Query<Service>(query, new { Id = id });
			}
		}
	}
}