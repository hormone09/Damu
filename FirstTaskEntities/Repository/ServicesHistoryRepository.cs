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
	public class ServicesHistoryRepository : IRepository<ServicesHistory>
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<ServicesHistory> List(object queryList)
		{
			ServiceHistoryQueryList query;
			if (queryList is ServiceHistoryQueryList)
				query = (ServiceHistoryQueryList)queryList;
			else
				throw new Exception("Некорректный тип обьекта с набором параметров SQL-запроса!");

			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<ServicesHistory>("SELECT * FROM ServicesHistory WHERE MONTH(DateOfCreate) = MONTH(@DatePeriod) AND Status = @Status", new { DatePeriod = query.DatePeriod, Status = query.Status }).ToList();
			}
		}
		public void Update(ServicesHistory entity)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServicesHistory SET CompanyId = @CompanyId, ServiceId = @ServiceId, EmployeeId = @EmployeeId WHERE Id = @Id";
				connection.Query(query, new { Id = entity.Id, CompanyId = entity.CompanyId, ServiceId = entity.ServiceId, EmployeeId = entity.EmployeeId, DateOfCreate = entity.DateOfCreate, DateOfDelete = entity.DateOfDelete, Status = entity.Status });
			}
		}

		public void Add(ServicesHistory entity)
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
				string query = "UPDATE ServicesHistory SET Status = @Status, DateOfFinish = @DateOfFinish WHERE Id = @Id";
				connection.Query<Service>(query, new { Id = id, Status = Statuses.Disabled, DateOfFinish = DateTime.Now });
			}
		}
	}
}
