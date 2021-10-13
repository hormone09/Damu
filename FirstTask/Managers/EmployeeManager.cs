using AutoMapper;
using FirstTask.Models;
using FirstTask.ViewModels;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;
using FirstTaskEntities.Query;
using FirstTaskEntities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Managers
{
	public class EmployeeManager
	{
		private CompanyRepository companyRep = new CompanyRepository();
		private EmployeeRepository emloyeeRep = new EmployeeRepository();
		private IMapper mapper;

		public EmployeeManager(IMapper mapper)
		{
			this.mapper = mapper;
		}
		public EmployeeViewModel GetEmployeies(EmployeeViewModel model)
		{
			var query = mapper.Map<EmployeeQueryList>(model);
			var employeeEntities = emloyeeRep.List(query);
			var emloyeeModels = mapper.Map<List<EmployeeModel>>(employeeEntities);

			foreach (var el in emloyeeModels)
			{
				var companyId = employeeEntities.First(x => x.Id == el.Id).CompanyId;
				el.Company = companyRep.Find(companyId);
			}

			model.Items = emloyeeModels;
			model.RowNumber = employeeEntities.Any() ? employeeEntities.FirstOrDefault().TotalRows : 0;

			return model;
		}

		
		public bool Edit(Employee employee)
		{
			if (string.IsNullOrEmpty(employee.PersonalNumber) || string.IsNullOrEmpty(employee.FullName) || string.IsNullOrEmpty(employee.Phone)
				|| employee.DateOfBegin == null || employee.BirthdayDate == null)
				return false;

			if (employee.DateOfBegin <= DateTime.Now)
			{
				employee.Status = Statuses.Active;
				employee.DateOfFinish = null;
			}
			else
			{
				employee.Status = Statuses.Disabled;
			}

			employee.Phone = employee.Phone.Replace("-", "").Replace("(", "").Replace(")", "");
			employee.PersonalNumber = employee.PersonalNumber.Replace("-", "").Replace(" ", "");

			try
			{
				emloyeeRep.Update(employee);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Add(Employee employee)
		{
			if (string.IsNullOrEmpty(employee.FullName) || string.IsNullOrEmpty(employee.PersonalNumber) || string.IsNullOrEmpty(employee.Phone)
				|| employee.BirthdayDate == null || employee.DateOfBegin == null)
				return false;

			if (employee.DateOfBegin <= DateTime.Now)
				employee.Status = Statuses.Active;
			else
				employee.Status = Statuses.Disabled;

			employee.Phone = employee.Phone.Replace("-", "").Replace("(", "").Replace(")", "");
			employee.PersonalNumber = employee.PersonalNumber.Replace("-", "").Replace(" ", "");

			try
			{
				emloyeeRep.Add(employee);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Delete(int id)
		{
			try
			{
				emloyeeRep.Remove(id);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}