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
using System.Threading;

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
			Thread.Sleep(2000);
			Console.WriteLine("Сервисы загружены!");
			AddCompanies();
			Thread.Sleep(2000);
			Console.WriteLine("Компании загружены!");
			AddEmployee();
			Thread.Sleep(2000);
			Console.WriteLine("Сотрудники загружены!");
			AddServicesProvided();
			Thread.Sleep(2000);
			Console.WriteLine("Оказываемые услуги загружены!");
			AddHistory();
			Console.WriteLine("История загружена!");

			Console.ReadKey();
		}
		static void AddCompanies()
		{
			for (int i = 0; i < 150; i++)
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
			for (int i = 0; i < 150; i++)
			{
				var obj = new Service
				{
					Code = "x11.111.111",
					DateOfBegin = DateTime.Parse("12.10.2021"),
					Name = $"Service number {i}",
					Price = 500 + (i*24),
					Status = Statuses.Active
				};

				serviceRep.Add(obj);
			}
		}

		static void AddServicesProvided()
		{
			int[] companiesMas = companyRep.List(new CompanyQueryList { Skip = 0, Limit = 10000000, Status = Statuses.Active }).Select(x => x.Id).ToArray();
			int[] servicesMas = serviceRep.List(new ServiceQueryList { Skip = 0, Limit = 10000000, Status = Statuses.Active }).Select(x => x.Id).ToArray();
			Random random = new Random();

			for (int i = 0; i < 150; i++)
			{
				var companyId = companiesMas[random.Next(0, companiesMas.Length - 1)];
				var serviceId = servicesMas[random.Next(0, servicesMas.Length - 1)];
				var obj = new ServiceProvided
				{
					CompanyId = companyId,
					ServiceId = serviceId,
					DateOfBegin = DateTime.Parse("15.10.2021"),
					Status = Statuses.Active,
					ServicePrice = 500 + (i * 57)
				};

				serviceProvidedRep.Add(obj);
			}
		}

		static void AddEmployee()
		{
			int[] companiesMas = companyRep.List(new CompanyQueryList { Skip = 0, Limit = 10000000, Status = Statuses.Active }).Select(x => x.Id).ToArray();
			Random random = new Random();

			for (int i = 0; i < 250; i++)
			{
				var companyId = companiesMas[random.Next(0, companiesMas.Length - 1)];
				var obj = new Employee
				{
					CompanyId = companyId,
					BirthdayDate = DateTime.Now,
					DateOfBegin = DateTime.Now,
					FullName = $"Employee Number {i}",
					PersonalNumber = "123456789101",
					Phone = "87012730270",
					Status = Statuses.Active
				};

				employeeRep.Add(obj);
			}
		}

		static void AddHistory()
		{
			int[] companiesMas = companyRep.List(new CompanyQueryList { Skip = 0, Limit = 1000000, Status = Statuses.Active }).Select(x => x.Id).ToArray();
			int[] servicesMas = serviceRep.List(new ServiceQueryList { Skip = 0, Limit = 100000, Status = Statuses.Active }).Select(x => x.Id).ToArray();
			int[] employeeMas = employeeRep.List(new EmployeeQueryList { Skip = 0, Limit = 100000, Status = Statuses.Active }).Select(x => x.Id).ToArray();
			Random random = new Random();

			for (int i = 0; i < 1; i++)
			{
				var companyId = companiesMas[random.Next(0, companiesMas.Length - 1)];
				var serviceId = servicesMas[random.Next(0, servicesMas.Length - 1)];
				var employeeId = employeeMas[random.Next(0, employeeMas.Length - 1)];
				var obj = new ServicesHistory
				{
					EmployeeId = employeeId,
					CompanyId = companyId,
					ServiceId = serviceId,
					DateOfCreate = DateTime.Now,
					DateOfDelete = null,
					Status = Statuses.Active
				};

				serviceHistoryRep.Add(obj);
			}
		}
	}
}
