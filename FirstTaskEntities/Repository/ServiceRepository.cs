using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FirstTaskEntities.Models;
using System.Configuration;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Query;
using FirstTaskEntities.Interfaces;

namespace FirstTaskEntities.Repository
{
	public class ServiceRepository : IRepository<Service>
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<Service> List(object queryList)
		{
			ServiceQueryList query;

			if (queryList is ServiceQueryList)
				query = (ServiceQueryList)queryList;
			else
				throw new Exception("Некорректный тип обьекта с набором параметров SQL-запроса!");

			string where = "WHERE Status = @Status";
			string limit = string.Empty;
			string orderType;

			if (string.IsNullOrEmpty(query.SortingType))
				orderType = "Id";
			else
				orderType = query.SortingType;

			if (!string.IsNullOrEmpty(query.ServiceName))
				where += " AND Name LIKE '" + query.ServiceName + "%'";

			if (query.Limit > 0)
				limit = " FETCH NEXT @Limit ROWS ONLY";

			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<Service>($"SELECT *, COUNT(*) OVER() AS TotalRows FROM Services {where} ORDER BY {orderType} OFFSET @Skip ROWS{limit}", query).ToList();
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

		public void Update(Service service)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Services SET Name = @Name, Code = @Code, DateOfBegin = @Date, DateOfFinish = @DateOfFinish, Price = @Price, Status = @Status WHERE Id = @Id";
				connection.Query<Service>(query, new { Id = service.Id, Status = service.Status, Name = service.Name, Code = service.Code, 
					Date = service.DateOfBegin, DateOfFinish = service.DateOfFinish, Price = service.Price });
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Services SET Status = @Status, DateOfFinish = @DateOfFinish WHERE Id = @Id";
				connection.Query<Service>(query, new { Id = id, Status = Statuses.Disabled,  DateOfFinish = DateTime.Now});
			}
		}

		public Service Find(int id)
		{
			var result = new Service();
			using (var connection = new SqlConnection(connectionString))
			{
				var query = "SELECT * FROM Services WHERE Id = @Id";
				result = connection.Query<Service>(query, new { Id = id }).FirstOrDefault();
			}

			return result;
		}
	}
}