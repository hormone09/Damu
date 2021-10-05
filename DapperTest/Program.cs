using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FirstTaskEntities.Models;
using System.Configuration;

namespace DapperTest
{
	class Program
	{
		static string connectionString = ConfigurationManager.AppSettings["connection"];
		//static string connectionString = "Server=DESKTOP-8LKEMKN; Database=FirstTask; Trusted_Connection=True;";

		static void Main(string[] args)
		{
			Company company = new Company
			{
				BIN = "123456789101",
				DateOfBegin = DateTime.Now,
				Name = "new company",
				Phone = "8-707-640-56-99"
			};

			for (int i = 1; i < 5; i++)
				Add(company); 
			 //Remove();

		}

		public static void Add(Company company)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO Companies (Name, BIN, Phone, DateOfBegin) VALUES (@Name, @BIN, @Phone, @DateOfBegin)";
				connection.Query<Company>(query, new { Name = company.Name, BIN = company.BIN, Phone = company.Phone, DateOfBegin = company.DateOfBegin });
			}
		}

		public static void Remove()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				string query = "DELETE FROM Services WHERE Id > 0;";
				connection.Query<Service>(query);
			}
		}
	}
}
