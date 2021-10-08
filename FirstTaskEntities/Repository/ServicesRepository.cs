using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FirstTaskEntities.Models;
using System.Configuration;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Interfaces;
using FirstTaskEntities.Query;

namespace FirstTaskEntities.Repository
{
	public class ServicesRepository
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];
		//string query = "SELECT * FROM Services WHERE Status = @Status";

		public List<Service> List(ServiceListQuery queryList)
		{
			string where = "WHERE";
			if (!string.IsNullOrEmpty(queryList.ServiceName)) where += " Name LIKE " + queryList.ServiceName + "% AND ";

			/*if (queryList.Date1 != null && queryList.Date2 == null)
				where += "DateOfBegin >= @Date AND";
			else if(queryList.Date1 != null && queryList.Date2 != null)
				where += "DateOfBegin >= @Date1 AND DateOfBegin <= @Date2 AND";*/

			if (queryList.Status != null)
				where += "Status = @Status AND";

			if (queryList.Price > 0)
				where += "Price = @Price";

			if (where.Equals("WHERE")) where = string.Empty;

			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<Service>($"SELECT *, COUNT(*) OVER() AS TotalRows FROM Services {where} WHERE Status = 1  ORDER BY Id OFFSET @Skip ROWS FETCH NEXT @Limit ROWS ONLY", queryList).ToList();
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
				string query = "UPDATE Services SET Name = @Name, Code = @Code, DateOfBegin = @Date, Price = @Price WHERE Id = @Id";
				connection.Query<Service>(query, new { Id = service.Id, Name = service.Name, Code = service.Code, Date = service.DateOfBegin, Price = service.Price });
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

		public int GetCount(string query, object param)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var count = connection.QuerySingle<int>(query, param);

				return count;
			}
		}
	}
}