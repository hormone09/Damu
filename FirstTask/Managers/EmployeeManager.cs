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
				el.Company = companyRep.List(new CompanyQueryList { Id = el.Company.Id, Skip = 0, Limit = 1 } ).FirstOrDefault();

			model.Items = emloyeeModels;
			model.RowNumber = employeeEntities.Any() ? employeeEntities.FirstOrDefault().TotalRows : 0;

			return model;
		}

		/*
		public bool Edit(Employee employee)
		{
			if (string.IsNullOrEmpty(employee.Code) || string.IsNullOrEmpty(employee.Name) || employee.Price <= 0 || employee.Id <= 0)
				return false;

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
			if (string.IsNullOrEmpty(employee.Code) || string.IsNullOrEmpty(employee.Name) || employee.Price <= 0)
				return false;

			if (employee.DateOfBegin <= DateTime.Now)
				employee.Status = Statuses.Active;
			else
				employee.Status = Statuses.Disabled;

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
		}*/
	}
}