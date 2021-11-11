using Dapper;
using Entities.Interfaces;
using Entities.Models;
using Entities.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Entities.Repository
{
	public class ServiceHistoryRepository : IRepository<ServiceHistory>
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<ServiceHistory> List(QueryListBase queryList)
		{
			ServiceHistoryQueryList query;
			if (queryList is ServiceHistoryQueryList)
				query = (ServiceHistoryQueryList)queryList;
			else
				throw new TypeUnloadedException();

			using (var connection = new SqlConnection(connectionString))
			{
				string join = "INNER JOIN Companies ON Companies.Id = ServicesHistory.CompanyId INNER JOIN Services ON Services.Id = ServicesHistory.ServiceId INNER JOIN Employee ON Employee.Id = ServicesHistory.EmployeeId";
				string sql = $"SELECT * FROM ServicesHistory {join} WHERE DateOfCreate >= @DateBegin AND CONVERT(date, DateOfCreate) <= @DateEnd AND ServicesHistory.Status = @Status"; 
				object sqlParams = new { DateBegin = query.DateBegin, DateEnd = query.DateEnd, Status = query.Status };
				return connection.Query<ServiceHistory, Company, Service, Employee, ServiceHistory>(
					sql, 
					(history, company, service, employee) =>
					{
						history.Company = company;
						history.Service = service;
						history.Employee = employee;

						return history;
					},
					sqlParams
					).ToList();
			}
		}
		public void Update(ServiceHistory entity)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServicesHistory SET CompanyId = @CompanyId, DateOfCreate = @DateOfCreate, DateOfFinish = @DateOfFinish, ServiceId = @ServiceId, EmployeeId = @EmployeeId WHERE Id = @Id";
				connection.Query(query, new { Id = entity.Id, CompanyId = entity.CompanyId, ServiceId = entity.ServiceId, EmployeeId = entity.EmployeeId, DateOfCreate = entity.DateOfCreate, DateOfFinish = entity.DateOfFinish, DateOfDelete = entity.DateOfDelete});
			}
		}

		public void Add(ServiceHistory entity)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO ServicesHistory (CompanyId, ServiceId, EmployeeId, DateOfCreate, DateOfFinish, DateOfDelete, Status) VALUES (@CompanyId, @ServiceId, @EmployeeId, @DateOfCreate, @DateOfFinish, @DateOfDelete, @Status)";
				connection.Query(query, new { CompanyId = entity.CompanyId, ServiceId = entity.ServiceId, EmployeeId = entity.EmployeeId, DateOfCreate = entity.DateOfCreate, DateOfFinish = entity.DateOfFinish, DateOfDelete = entity.DateOfDelete, Status = entity.Status });
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
