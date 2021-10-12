using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FirstTaskEntities.Models;
using System.Configuration;
using FirstTaskEntities.Repository;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Query;

namespace DapperTest
{
	class Program
	{
		static string connectionString = ConfigurationManager.AppSettings["connection"];

		static ServicesRepository serviceRep = new ServicesRepository();
		static CompanyRepository companyRep = new CompanyRepository();
		static EmployeeRepository employeeRep = new EmployeeRepository();
		static ServiceProvidedRepository serviceProvidedRep = new ServiceProvidedRepository();
		static ServicesHistoryRepository serviceHistoryRep = new ServicesHistoryRepository();


		static void Main(string[] args)
		{

			//AddServices();
			//AddCompanies();
			AddEmployee();
		}
		static void AddCompanies()
		{
			for (int i = 0; i < 15; i++)
			{
				var obj = new Company
				{
					Phone = "87076405699",
					BIN = "123456789101",
					DateOfBegin = DateTime.Now,
					Name = $"Компания {i}",
					Status = Statuses.Active,
				};

				companyRep.Add(obj);
			}
		}

		static void AddServices()
		{
			for (int i = 0; i < 15; i++)
			{
				var obj = new Service
				{
					Code = "12345",
					DateOfBegin = DateTime.Now,
					Name = "NewName",
					Price = 5000,
					Status = Statuses.Disabled
				};

				serviceRep.Add(obj);
			}
		}
		static void AddEmployee()
		{
			int[] mas = companyRep.List(new CompanyQueryList { Skip = 0, Limit = 20 }).Select(x => x.Id).Distinct().ToArray();
			Random random = new Random();

			for (int i = 0; i < 50; i++)
			{
				var obj = new Employee
				{
					CompanyId = random.Next(6, 10),
					BirthdayDate = DateTime.Now,
					DateOfBegin = DateTime.Now,
					FullName = "Name",
					PersonalNumber = "123456789101",
					Phone = "123123",
					Status = Statuses.Active
				};

				employeeRep.Add(obj);
			}
		}
	}
}
