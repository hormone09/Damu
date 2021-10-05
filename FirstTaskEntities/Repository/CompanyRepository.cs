using Dapper;
using FirstTaskEntities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FirstTaskEntities.Repository
{
	public class CompanyRepository
	{
		private string connectionString = ConfigurationManager.AppSettings["connection"];

		public List<Company> GetAll()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				return connection.Query<Company>("SELECT * FROM Services").ToList();
			}
		}
		public void Add(Company company)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO Companies (Name, BIN, Phone, DateOfBegin) VALUES (@Name, @BIN, @Phone, @DateOfBegin)";
				connection.Query<Company>(query, new {  Name = company.Name, BIN = company.BIN, Phone = company.Phone, DateOfBegin = company.DateOfBegin });
			}
		}

		public void Update(Company company, int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE Companies SET Name = @Name, BIN = @BIN, DateOfBegin = @Date, Phone = @Phone WHERE Id= @Id";
				connection.Query<Company>(query, new { Id = id, Name = company.Name, BIN = company.BIN, Date = company.DateOfBegin, Phone = company.Phone });
			}
		}

		public void Remove(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "DELETE FROM Companies WHERE Id = @Id";
				connection.Query<Company>(query, new { Id = id });
			}
		}
	}
}