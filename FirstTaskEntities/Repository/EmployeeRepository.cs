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

namespace FirstTaskEntities.Repository
{
	public class EmployeeRepository : IRepository<Employee>
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<Employee> List(object queryList)
		{
			EmployeeQueryList query;

			if (queryList is EmployeeQueryList)
				query = (EmployeeQueryList)queryList;
			else
				throw new Exception("Некорректный тип обьекта с набором параметров SQL-запроса!");

			string where = "WHERE Status = @Status";
			string limit = string.Empty;
			string orderType;

			if (string.IsNullOrEmpty(query.SortingType))
				orderType = "Id";
			else
				orderType = query.SortingType;

			if (!string.IsNullOrEmpty(query.FullName))
				where += " AND FullName LIKE '" + query.FullName + "%'";

			if (query.CompanyId != null)
				where += " AND CompanyId = @CompanyId";

			if (query.Limit > 0)
				limit = " FETCH NEXT @Limit ROWS ONLY";

			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<Employee>($"SELECT *, COUNT(*) OVER() AS TotalRows FROM Employee {where} ORDER BY {orderType} OFFSET @Skip ROWS{limit}", query).ToList();
			}
		}

		public void Add(Employee employee)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO Employee (PersonalNumber, FullName, BirthdayDate, Phone, DateOfBegin, CompanyId, Status) " +
					"VALUES (@PersonalNumber, @FullName, @BirthdayDate, @Phone, @DateOfBegin, @CompanyId, @Status)";
				connection.Query<Employee>(query, new { Status = employee.Status, PersonalNumber = employee.PersonalNumber, FullName = employee.FullName, BirthdayDate = employee.BirthdayDate, Phone = employee.Phone, DateOfBegin = employee.DateOfBegin, CompanyId = employee.CompanyId });
			}
		}

		public void Update(Employee employee)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Employee SET DateOfFinish = @DateOfFinish, DateOfBegin = @DateOfBegin, PersonalNumber = @PersonalNumber, FullName = @FullName, " +
					"BirthdayDate = @BirthdayDate, Phone = @Phone, CompanyId = @CompanyId, Status = @Status WHERE Id = @Id";
				connection.Query<Employee>(query, new { Id = employee.Id, Phone = employee.Phone, PersonalNumber = employee.PersonalNumber, CompanyId = employee.CompanyId,
					FullName = employee.FullName, BirthdayDate = employee.BirthdayDate, Status = employee.Status, DateOfBegin = employee.DateOfBegin, DateOfFinish = employee.DateOfFinish});
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Employee SET Status = @Status, DateOfFinish = @Finish WHERE Id = @Id";
				connection.Query<Employee>(query, new { Id = id, Finish = DateTime.Now, Status = Statuses.Disabled });
			}
		}

		public Employee Find(int id)
		{
			var result = new Employee();
			using (var connection = new SqlConnection(connectionString))
			{
				var query = "SELECT * FROM Employee WHERE Id = @Id";
				result = connection.Query<Employee>(query, new { Id = id }).FirstOrDefault();
			}

			return result;
		}
	}
}
