using Dapper;
using Entities.Enums;
using Entities.Models;
using Entities.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Entities.Repository
{
	public class ServiceProvidedRepository
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<ServiceProvided> List(QueryListBase queryList)
		{
			ServiceProvidedQueryList query;

			if (queryList is ServiceProvidedQueryList)
				query = (ServiceProvidedQueryList)queryList;
			else
				throw new TypeUnloadedException();

			string where = "WHERE ServiceProvided.Status = @Status";
			string orderType;
			string limit = string.Empty;

			if (string.IsNullOrEmpty(query.SortingType))
				orderType = "ServiceProvided.Id";
			else
			{
				orderType = $"ServiceProvided.{query.SortingType}";
			}

			if (query.CompanyId != null)
				where += " AND ServiceProvided.CompanyId = @CompanyId";

			if (query.ServiceId != null)
				where += " AND ServiceProvided.ServiceId = @ServiceId";

			if (query.Limit > 0)
				limit = " FETCH NEXT @Limit ROWS ONLY";

			using (var connection = new SqlConnection(connectionString))
			{
				string join = "INNER JOIN Companies ON Companies.Id = ServiceProvided.CompanyId INNER JOIN Services ON Services.Id = ServiceProvided.ServiceId";
				string sql = $"SELECT *, COUNT(ServiceProvided.Id) OVER() AS TotalRows FROM ServiceProvided {join} {where} ORDER BY {orderType} OFFSET @Skip ROWS{limit}";

				var result =  connection.Query<ServiceProvided, Company, Service, ServiceProvided>(
					sql,
					(provided, company, service) =>
					{
						provided.Service = service;
						provided.Company = company;
						provided.TotalRows = service.TotalRows;

						return provided;
					},
					query
					).ToList();

				return result;
			}
		}

		public void Add(ServiceProvided entity)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO ServiceProvided (ServiceId, CompanyId, DateOfBegin, DateOfFinish, Status, ServicePrice) VALUES (@ServiceId, @CompanyId, @DateOfBegin, null, @Status, @ServicePrice)";
				connection.Query(query, new { ServiceId = entity.ServiceId, CompanyId = entity.CompanyId, DateOfBegin = entity.DateOfBegin, Status = entity.Status, ServicePrice = entity.ServicePrice});
			}
		}

		public void Update(ServiceProvided entity)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServiceProvided SET ServiceId = @ServiceId, CompanyId = @CompanyId, DateOfBegin = @DateOfBegin, DateOfFinish = @DateOfFinish, ServicePrice = @ServicePrice, Status = @Status WHERE Id= @Id";
				connection.Query<ServiceProvided>(query, new { Id = entity.Id, ServiceId = entity.ServiceId, CompanyId = entity.CompanyId, DateOfBegin = entity.DateOfBegin, DateOfFinish = entity.DateOfFinish, ServicePrice = entity.ServicePrice, Status = entity.Status });
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServiceProvided SET Status = @Status, DateOfFinish = @Finish WHERE Id = @Id";
				connection.Query<ServiceProvided>(query, new { Id = id, Status = Statuses.Disabled, Finish = DateTime.Now});
			}
		}

		public ServiceProvided Find(int id)
		{
			var result = new ServiceProvided();
			using (var connection = new SqlConnection(connectionString))
			{
				var query = "SELECT * FROM ServiceProvided WHERE Id = @Id";
				result = connection.Query<ServiceProvided>(query, new { Id = id }).FirstOrDefault();
			}

			return result;
		}
	}
}
