using Dapper;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Interfaces;
using FirstTaskEntities.Models;
using FirstTaskEntities.Query;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Repository
{
	public class ServiceProvidedRepository
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<ServiceProvided> List(object queryList)
		{
			ServiceProvidedQueryList query;

			if (queryList is ServiceProvidedQueryList)
				query = (ServiceProvidedQueryList)queryList;
			else
				throw new Exception("Некорректный тип обьекта с набором параметров SQL-запроса!");

			string where = "WHERE Status = @Status";
			string orderType;

			if (string.IsNullOrEmpty(query.SortingType))
				orderType = "Id";
			else
				orderType = query.SortingType;

			if (query.CompanyId != null)
				where += " AND CompanyId = @CompanyId";

			if (query.ServiceId != null)
				where += " AND ServiceId = @ServiceId";

			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<ServiceProvided>($"SELECT *, COUNT(*) OVER() AS TotalRows FROM ServiceProvided {where} ORDER BY {orderType} OFFSET @Skip ROWS FETCH NEXT @Limit ROWS ONLY", query).ToList();
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
				string query = "UPDATE ServiceProvided SET ServiceId = @ServiceId, CompanyId = @CompanyId, DateOfBegin = @DateOfBegin, DateOfFinish = @DateOfFinish, Status = @Status, ServicePrice = @ServicePrice WHERE Id= @Id";
				connection.Query<ServiceProvided>(query, new { Id = entity.Id, ServiceId = entity.ServiceId, CompanyId = entity.CompanyId, DateOfBegin = entity.DateOfBegin, DateOfFinish = entity.DateOfFinish, Status = entity.Status, ServicePrice = entity.ServicePrice });
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

		public void Activate(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServiceProvided SET Status = @Status, DateOfBegin = @DateOfBegin, DateOfFinish = null WHERE Id = @Id";
				connection.Query<ServiceProvided>(query, new { Id = id, Status = Statuses.Active, DateOfBegin = DateTime.Now });
			}
		}
	}
}
