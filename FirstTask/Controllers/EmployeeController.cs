using FirstTask.Helpers;

using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;
using FirstTaskEntities.Repository;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstTask.Controllers
{
    public class EmployeeController : Controller
    {
		private readonly EmployeeRepository rep;

		public ActionResult Index(int? page)
		{
			return View();
		}
			/*
			var emloyies = rep.GetAll();
			int pagesCount = 0;
			var pageResult = PageHelper<Employee>.GetPageResult(emloyies, page, out pagesCount);

			ViewBag.PagesCount = pagesCount;
			ViewBag.Model = pageResult;

			return View();
		}

		[HttpPost]
		public string FindCurrentEmployee(string companyName, string fullName, EmloyeeStatuses? status)
		{
			var result = new List<Employee>();

			try
			{
				result = rep.GetAll();
			}
			catch(Exception ex)
			{
				return JsonHelper.SerializeErorr("Параметры для поиска не заданы!");
			}

			if(!string.IsNullOrEmpty(companyName) || !string.IsNullOrEmpty(fullName) || status != null)
			{
				if (!string.IsNullOrEmpty(companyName))
					result = result.Where(x => x.Company.Name.Equals(companyName)).ToList();
				if (!string.IsNullOrEmpty(fullName))
					result = result.Where(x => x.FullName.Equals(companyName)).ToList();
				if (status != null)
					result = result.Where(x => x.Status == status).ToList();
			}
			else
			{
				return JsonHelper.SerializeErorr("Параметры для поиска не заданы!");
			}

			if(result.Any())
			{
				dynamic array = new JArray();

				foreach(var el in result)
				{
					dynamic temp = new JObject();

					temp.FullName = el.FullName;
					temp.CompanyName = el.Company.Name;
					temp.BirthdayDate = el.BirthdayDate;
					temp.PersonalNumber = el.PersonalNumber;
					temp.Status = el.Status;
					temp.Phone = el.Phone;

					array.Add(temp);
				}

				return JsonHelper.SerializeSuccesList(array);
			}
			else
			{
				return JsonHelper.SerializeErorr("По заданным параметрам ничего не найдено!");
			}
		}

		[HttpPost]
		public string AddEmployee(string personalNumber, string fullName, DateTime birthdayDate, int? companyId, string phone, EmloyeeStatuses? status)
		{
			if (string.IsNullOrEmpty(personalNumber) || string.IsNullOrEmpty(fullName) || birthdayDate == null ||
				companyId == null || string.IsNullOrEmpty(phone) || status == null)
				return JsonHelper.SerializeErorr("Заполните все поля!");

			var newEmployee = new Employee
			{
				BirthdayDate = birthdayDate,
				PersonalNumber = personalNumber,
				FullName = fullName,
				Company = new Company { Id = Convert.ToInt32(companyId) },
				Phone = phone,
				Status = (EmloyeeStatuses)status
			};

			try
			{
				rep.Add(newEmployee);
			}
			catch(Exception)
			{
				var error = "Ошибка добавления в БД!";

				return JsonHelper.SerializeErorr(error);
			}

			return JsonHelper.SerializeSuccess("Сотрудник успешно добавлен!");
		}

		[HttpPost]
		public string UpdateEmployee(int? id, string personalNumber, string fullName, DateTime birthdayDate, int? companyId, string phone, EmloyeeStatuses? status)
		{
			if (id == null || string.IsNullOrEmpty(personalNumber) || string.IsNullOrEmpty(fullName) || birthdayDate == null ||
				   companyId == null || string.IsNullOrEmpty(phone) || status == null)
				return JsonHelper.SerializeErorr("Заполните все поля!");

			var newEmployee = new Employee
			{
				BirthdayDate = birthdayDate,
				PersonalNumber = personalNumber,
				FullName = fullName,
				Company = new Company { Id = Convert.ToInt32(companyId) },
				Phone = phone,
				Status = (EmloyeeStatuses)status
			};

			try
			{
				rep.Update(newEmployee, (int)id);
			}
			catch (Exception)
			{
				var error = "Ошибка обновления БД!";

				return JsonHelper.SerializeErorr(error);
			}

			return JsonHelper.SerializeSuccess($"Сотрудник {fullName} успешно обновлен!");
		}

		[HttpPost]
		public string ShutdownEmloyee(int? id)
		{
			if (id == null)
				return JsonHelper.SerializeErorr("Номер пользователя не был получен!");

			try
			{
				rep.Remove((int)id);
			}
			catch(Exception)
			{
				return JsonHelper.SerializeErorr("Сотрудник не был удален!");
			}

			return JsonHelper.SerializeSuccess("Сотрудник был переведен в 'Не активные'!");
		}*/

	}
}