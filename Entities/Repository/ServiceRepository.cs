using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Entities.Models;
using System.Configuration;
using Entities.Enums;
using Entities.Query;
using Entities.Interfaces;

namespace Entities.Repository
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
				throw new TypeUnloadedException();

			string where = string.Empty;
			string limit = string.Empty;
			string orderType;

			if (query.Status == Statuses.Active || query.Status == Statuses.Disabled)
				where = "WHERE Status = @Status";
			else if (query.Status == 0)
				where = "WHERE Status IN(1,2)";

			if (string.IsNullOrEmpty(query.SortingType))
				orderType = "Id";
			else
				orderType = query.SortingType;

			if (!string.IsNullOrEmpty(query.ServiceName))
				where += " AND Name LIKE '%" + query.ServiceName + "%'";

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
				connection.Query<Service>(query, new { Id = service.Id, Name = service.Name, Code = service.Code, 
					Date = service.DateOfBegin, DateOfFinish = service.DateOfFinish, Price = service.Price, Status = service.Status });
			}
		}

		public void Remove(int id)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				SqlTransaction transaction = connection.BeginTransaction();
				SqlCommand command = connection.CreateCommand();

				command.Transaction = transaction;

				try
				{
					command.CommandText = "UPDATE ServiceProvided SET Status = @Status WHERE ServiceId = @ServiceId";
					command.Parameters.Add(new SqlParameter { Value = Statuses.Disabled, ParameterName = "Status" });
					command.Parameters.Add(new SqlParameter { Value = id, ParameterName = "ServiceId" });
					command.ExecuteNonQuery();

					command.CommandText = "UPDATE Services SET Status = @Status WHERE Id = @ServiceId";
					command.ExecuteNonQuery();

					transaction.Commit();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					throw ex;
				}
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