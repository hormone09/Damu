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
	public class EmployeeRepository : IRepository<Employee>
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<Employee> List(string query, object param)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<Employee>("SELECT * FROM Services").ToList();
			}
		}

		public void Add(Employee employee)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO Employee (PersonalNumber, FullName, BirthdayDate, Phone, DateOfBegin, CompanyID, Status) " +
					"VALUES (@PersonalNumber, @FullName, @BirthdayDate, @Phone, @DateOfBegin, @CompanyID, @Status)";
				connection.Query(query, new { Status = employee.Status, PersonalNumber = employee.PersonalNumber, FullName = employee.FullName, BirthdayDate = employee.BirthdayDate, Phone = employee.Phone, DateOfBegin = DateTime.Now, CompanyId = employee.CompanyId });
			}
		}

		// TODO: Перенести ID в объект
		public void Update(Employee employee)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Employee SET PersonalNumber = @PersonalNumber, FullName = @FullName, BirthdayDate = @BirthdayDate, Phone = @Phone Status = @Status WHERE Id = @Id";
				connection.Query<Employee>(query, new { Id = employee.Id, PersonalNumber = employee.PersonalNumber, FullName = employee.FullName, BirthdayDate = employee.BirthdayDate, Phone = employee.Phone, Status = employee.Status});
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Employee SET Status = @Status, DateOfFinish = @Finish WHERE Id = @Id";
				connection.Query<Employee>(query, new { Id = id, Finish = DateTime.Now, Statuses.Disabled });
			}
		}
	}
}
