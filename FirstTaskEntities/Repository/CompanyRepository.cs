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
	public class CompanyRepository : IRepository<Company>
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<Company> List(object queryList)
		{
			CompanyQueryList query;

			if (queryList is CompanyQueryList)
				query = (CompanyQueryList)queryList;
			else
				throw new Exception("Некорректный тип обьекта с набором параметров SQL-запроса!");

			string where = string.Empty;
			string limit = string.Empty;
			string orderType;

			if (query.Status == Statuses.Active || query.Status == Statuses.Disabled)
				where = "WHERE Status = @Status";
			else if (query.Status != Statuses.Active && query.Status != Statuses.Disabled && query.Status == 0)
				where = "WHERE Status = 1 OR Status = 2";
			else
				throw new Exception("Некорректный номер статуса записи!");


			if (string.IsNullOrEmpty(query.SortingType))
				orderType = "Id";
			else
				orderType = query.SortingType;

			if (!string.IsNullOrEmpty(query.CompanyName))
				where += " AND Name LIKE '%" + query.CompanyName + "%'";

			if(query.Limit > 0)
				limit = " FETCH NEXT @Limit ROWS ONLY";

			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<Company>($"SELECT *, COUNT(*) OVER() AS TotalRows FROM Companies {where} ORDER BY {orderType} OFFSET @Skip ROWS{limit}", query).ToList();
			}
		}

		public void Add(Company company)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO Companies (Name, BIN, Phone, DateOfBegin, Status) VALUES (@Name, @BIN, @Phone, @DateOfBegin, @Status)";
				connection.Query<Company>(query, new { Name = company.Name, DateOfBegin = company.DateOfBegin, Status = company.Status, BIN = company.BIN, Phone = company.Phone });
			}
		}

		public void Update(Company company)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Companies SET Name = @Name, BIN = @BIN, DateOfFinish = @DateOfFinish, DateOfBegin = @Date, Phone = @Phone, Status = @Status WHERE Id= @Id";
				connection.Query<Company>(query, new { Id = company.Id, Name = company.Name, Date = company.DateOfBegin, 
					DateOfFinish = company.DateOfFinish, BIN = company.BIN, Phone = company.Phone, Status = company.Status });
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var query = "UPDATE Companies SET Status = @Status, DateOfFinish = @DateOfFinish WHERE Id = @Id";
				connection.Query<Company>(query, new { Id = id, Status = Statuses.Disabled, DateOfFinish = DateTime.Now });
			}
		}

		public Company Find(int id)
		{
			var result = new Company();
			using (var connection = new SqlConnection(connectionString))
			{
				var query = "SELECT * FROM Companies WHERE Id = @Id";
				result = connection.Query<Company>(query, new { Id = id }).FirstOrDefault();
			}
			return result;
		}
	}
}