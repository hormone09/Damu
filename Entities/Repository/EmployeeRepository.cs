using Dapper;
using Entities.Enums;
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
	public class EmployeeRepository : IRepository<Employee>
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<Employee> List(QueryListBase queryList)
		{
			EmployeeQueryList query;

			if (queryList is EmployeeQueryList)
				query = (EmployeeQueryList)queryList;
			else
				throw new TypeUnloadedException();

			string where = string.Empty;
			string limit = string.Empty;
			string orderType;

			if (query.Status == Statuses.Active || query.Status == Statuses.Disabled)
				where = "WHERE Employee.Status = @Status";
			else if (query.Status == 0)
				where = "WHERE Employee.Status IN(1,2)";

			if (string.IsNullOrEmpty(query.SortingType))
				orderType = "Employee.Id";
			else
			{
				orderType = $"Employee.{query.SortingType}";
			}	

			if (!string.IsNullOrEmpty(query.FullName))
				where += " AND Employee.FullName LIKE '%" + query.FullName + "%'";

			if (query.CompanyId != null)
				where += " AND Employee.CompanyId = @CompanyId";

			if (query.Limit > 0)
				limit = " FETCH NEXT @Limit ROWS ONLY";

			using (var connection = new SqlConnection(connectionString))
			{
				string join = "INNER JOIN Companies ON Companies.Id = Employee.CompanyId";
				string sql = $"SELECT *, COUNT(*) OVER() AS TotalRows FROM Employee {join} {where} ORDER BY {orderType} OFFSET @Skip ROWS{limit} ";

				return connection.Query<Employee, Company, Employee>(
					sql,
					(employee, company) =>
					{
						employee.TotalRows = company.TotalRows;
						employee.Company = company;
						return employee;
					},
					query
					).ToList();
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
					FullName = employee.FullName, BirthdayDate = employee.BirthdayDate, DateOfBegin = employee.DateOfBegin, DateOfFinish = employee.DateOfFinish, Status = employee.Status});
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
