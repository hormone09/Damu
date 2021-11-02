using Dapper;
using FirstTaskEntities.Interfaces;
using FirstTaskEntities.Models;
using FirstTaskEntities.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace FirstTaskEntities.Repository
{
	public class ServiceHistoryRepository : IRepository<ServiceHistory>
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<ServiceHistory> List(object queryList)
		{
			ServiceHistoryQueryList query;
			if (queryList is ServiceHistoryQueryList)
				query = (ServiceHistoryQueryList)queryList;
			else
				throw new Exception("Некорректный тип обьекта с набором параметров SQL-запроса!");

			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<ServiceHistory>("SELECT * FROM ServicesHistory WHERE DateOfCreate >= @DateBegin AND DateOfCreate <= @DateEnd AND Status = @Status", new { DateBegin = query.DateBegin, DateEnd = query.DateEnd, Status = query.Status }).ToList();
			}
		}
		public void Update(ServiceHistory entity)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServiceHistory SET CompanyId = @CompanyId, ServiceId = @ServiceId, EmployeeId = @EmployeeId WHERE Id = @Id";
				connection.Query(query, new { Id = entity.Id, CompanyId = entity.CompanyId, ServiceId = entity.ServiceId, EmployeeId = entity.EmployeeId, DateOfCreate = entity.DateOfCreate, DateOfDelete = entity.DateOfDelete, Status = entity.Status });
			}
		}

		public void Add(ServiceHistory entity)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO ServicesHistory (CompanyId, ServiceId, EmployeeId, DateOfCreate, DateOfDelete, Status) VALUES (@CompanyId, @ServiceId, @EmployeeId, @DateOfCreate, @DateOfDelete, @Status)";
				connection.Query(query, new { CompanyId = entity.CompanyId, ServiceId = entity.ServiceId, EmployeeId = entity.EmployeeId, DateOfCreate = entity.DateOfCreate, DateOfDelete = entity.DateOfDelete, Status = entity.Status });
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServicesHistory SET Status = 2, DateOfFinish = @DateOfFinish WHERE Id = @Id";
				connection.Query<Service>(query, new { Id = id, DateOfFinish = DateTime.Now });
			}
		}
	}
}
