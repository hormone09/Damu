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

		static ServiceRepository serviceRep = new ServiceRepository();
		static CompanyRepository companyRep = new CompanyRepository();
		static EmployeeRepository employeeRep = new EmployeeRepository();
		static ServiceProvidedRepository serviceProvidedRep = new ServiceProvidedRepository();
		static ServiceHistoryRepository serviceHistoryRep = new ServiceHistoryRepository();


		static void Main(string[] args)
		{
			while(true)
			{
				Console.WriteLine("Желаете ли вы удалить все существуещее записи?\n Да/Нет");

				var userResponse = Console.ReadLine();

				if(userResponse.Equals("Да"))
				{
					bool IsSuccessRemoved = RemoveData();

					if(IsSuccessRemoved)
						FillDataBase();

					break;
				}
				else if(userResponse.Equals("Нет"))
				{
					FillDataBase();

					break;
				}
				else
				{
					Console.WriteLine("Введите корректный ответ!");
				}
			}

			Console.ReadKey();
		}

		static bool RemoveData()
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				SqlTransaction transaction = connection.BeginTransaction();

				SqlCommand command = connection.CreateCommand();
				command.Transaction = transaction;

				try
				{
					command.CommandText = "DELETE FROM ServicesHistory";
					command.ExecuteNonQuery();

					command.CommandText = "DELETE FROM ServiceProvided";
					command.ExecuteNonQuery();

					command.CommandText = "DELETE FROM Employee";
					command.ExecuteNonQuery();

					command.CommandText = "DELETE FROM Companies";
					command.ExecuteNonQuery();

					command.CommandText = "DELETE FROM Services";
					command.ExecuteNonQuery();

					transaction.Commit();

					Console.WriteLine("Данные удалены из БД!");

					return true;
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					Console.WriteLine($"Не удалось удалить данные из БД! \n Ошибка: {ex.Message}");

					return false;
				}
			}
		}

		static void FillDataBase()
		{
			Console.WriteLine("Начало загрузки данных!");

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
		}

		static void AddCompanies()
		{
			for (int i = 0; i < Randomizer.CompanisCount; i++)
			{
				var obj = new Company
				{
					Phone = Randomizer.GetPhone(),
					BIN = Randomizer.GetPersonalNumber(),
					DateOfBegin = Randomizer.GetDate(),
					Name = Randomizer.GetCompanyName(i),
					Status = (int)Statuses.Active,
				};

				companyRep.Add(obj);
			}
		}

		static void AddServices()
		{
			for (int i = 0; i < Randomizer.ServicesCount; i++)
			{
				var obj = new Service
				{
					Code = Randomizer.GetServiceCode(),
					DateOfBegin = Randomizer.GetDate(),
					Name = Randomizer.GetServiceName(i),
					Price = Randomizer.GetPrice(),
					Status = (int)Statuses.Active
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
					DateOfBegin = Randomizer.GetDate(),
					Status = (int)Statuses.Active,
					ServicePrice = Randomizer.GetPrice()
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
					BirthdayDate = DateTime.Parse("11.11.1980"),
					DateOfBegin = Randomizer.GetDate(),
					FullName = Randomizer.GetFullName(),
					PersonalNumber = Randomizer.GetPersonalNumber(),
					Phone = Randomizer.GetPhone(),
					Status = (int)Statuses.Active
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

			for (int i = 0; i < 2; i++)
			{
				var companyId = companiesMas[random.Next(0, companiesMas.Length - 1)];
				var serviceId = servicesMas[random.Next(0, servicesMas.Length - 1)];
				var employeeId = employeeMas[random.Next(0, employeeMas.Length - 1)];
				var obj = new ServiceHistory
				{
					EmployeeId = employeeId,
					CompanyId = companyId,
					ServiceId = serviceId,
					DateOfCreate = DateTime.Now,
					DateOfDelete = null,
					Status = (int)Statuses.Active
				};

				serviceHistoryRep.Add(obj);
			}
		}
	}
}
