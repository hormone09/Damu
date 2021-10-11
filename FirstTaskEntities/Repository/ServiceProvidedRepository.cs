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
	public class ServiceProvidedRepository
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<ServiceProvided> List(string query, object param)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<ServiceProvided>("SELECT * FROM ServicesProvided").ToList();
			}
		}

		public void Add(ServiceProvided service)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO ServicesProvided (ServiceId, CompanyId, DateOfBegin, DateOfFinish, Status) VALUES (@ServiceId, @CompanyId, @DateOfBegin, null, @Status)";
				connection.Query(query, new { ServiceId = service.ServiceId, CompanyId = service.CompanyId, DateOfBegin = service.DateOfBegin, Status = service.Status });
			}
		}

		public void Update(ServiceProvided service)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServicesProvided SET ServiceId = @ServiceId, CompanyId = @CompanyId, DateOfBegin = @DateOfBegin, Status = @Status WHERE Id= @Id";
				connection.Query<ServiceProvided>(query, new { Id = service.Id, ServiceId = service.ServiceId, CompanyId = service.CompanyId, DateOfBegin = service.DateOfBegin, Status = service.Status });
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE ServicesProvided SET Status = @Status, DateOfFinish = @Finish";
				connection.Query<ServiceProvided>(query, new { Id = id, Status = Statuses.Disabled, Finish = DateTime.Now});
			}
		}
		public int GetCount(string query, object param)
		{
			return 1;
		}
	}
}
