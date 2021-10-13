using AutoMapper;

using FirstTask.Errors;
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
	public class CompanyManager
	{
		private CompanyRepository rep = new CompanyRepository();
		private IMapper mapper;

		public CompanyManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public CompanyViewModel GetCompanies(CompanyViewModel model)
		{
			var query = mapper.Map<CompanyQueryList>(model);

			var companies = rep.List(query);
			model.Items = companies;
			model.RowNumber = companies.Any() ? companies.FirstOrDefault().TotalRows : 0;

			return model;
		}
		public bool Edit(Company company)
		{
			//ActionStatus result;
			if (string.IsNullOrEmpty(company.BIN) || string.IsNullOrEmpty(company.Name) || string.IsNullOrEmpty(company.Phone) || company.Id <= 0)
				return false;//new ActionStatus(false, "Заполните все поля!");

			if(company.DateOfBegin <= DateTime.Now)
			{
				company.Status = Statuses.Active;
				company.DateOfFinish = null;
			}
			else
			{
				company.Status = Statuses.Disabled;
			}

			company.BIN = company.BIN.Replace("-", "");
			company.Phone = company.Phone.Replace("(", "").Replace(")", "").Replace("-", "");
			
			try
			{
				rep.Update(company);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Add(Company company)
		{
			if (string.IsNullOrEmpty(company.BIN) || string.IsNullOrEmpty(company.Name) || string.IsNullOrEmpty(company.Phone))
				return false;

			if (company.DateOfBegin >= DateTime.Now)
				company.Status = Statuses.Active;
			else
				company.Status = Statuses.Disabled;

			company.BIN = company.BIN.Replace("-", "");
			company.Phone = company.Phone.Replace("-", "").Replace("(", "").Replace("(", "");

			try
			{
				rep.Add(company);

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
				rep.Remove(id);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}