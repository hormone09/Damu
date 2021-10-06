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

			AddServices();
			//AddEmployee();
		}

		static void AddServices()
		{
			for (int i = 0; i < 50; i++)
			{
				var obj = new Service
				{
					Code = "12345",
					DateOfBegin = DateTime.Now,
					Name = "NewName",
					Price = 5000,
					Status = Statuses.Active
				};

				serviceRep.Add(obj);
			}
		}
		static void AddEmployee()
		{
			int[] mas = companyRep.List("SELECT * FROM Services", new { }).Select(x => x.Id).Distinct().ToArray();
			Random random = new Random();

			for (int i = 0; i < 5; i++)
			{
				var obj = new Employee
				{
					CompanyId = random.Next(mas[0], mas[mas.Length - 1]),
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
