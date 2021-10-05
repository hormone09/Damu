using Dapper;

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
	public class EmployeeRepository
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<Employee> GetAll()
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
				string query = "INSERT INTO Services (PersonalNumber, FullName, BirthdayDate, Phone) " +
					"VALUES (@PersonalNumber, @FullName, @BirthdayDate, @Phone)";
				connection.Query(query, new { PersonalNumber = employee.PersonalNumber, FullName = employee.FullName, BirthdayDate = employee.BirthdayDate, Phone = employee.Phone });
			}
		}

		public void Update(Employee employee, int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Services SET PersonalNumber = @PersonalNumber, FullName = @FullName, BirthdayDate = @BirthdayDate, Phone = @Phone WHERE Id = @Id";
				connection.Query<Employee>(query, new { Id = id, PersonalNumber = employee.PersonalNumber, FullName = employee.FullName, BirthdayDate = employee.BirthdayDate, Phone = employee.Phone });
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "DELETE FROM Services WHERE Id = @Id";
				connection.Query<Employee>(query, new { Id = id });
			}
		}
	}
}
