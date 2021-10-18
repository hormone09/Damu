using Dapper;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Interfaces;
using FirstTaskEntities.Models;
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

		public void Add(ServicesHistory entity)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO ServicesHistory (CompanyId, ServiceId, EmployeeId, DateOfCreate, DateOfDelete, Status) VALUES (@Name, @Price, @Code, @DateOfBegin, null, @Status)";
				connection.Query(query, new { CompanyId = entity.CompanyId, ServiceId = entity.ServiceId, EmployeeId = entity.EmployeeId, DateOfCreate = entity.DateOfCreate, DateOfDelete = entity.DateOfDelete, Status = entity.Status });
			}
		}

		public List<ServicesHistory> List(object queryList)
		{
			throw new NotImplementedException();
		}
		public void Update(ServicesHistory instanse)
		{
			throw new NotImplementedException();
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
