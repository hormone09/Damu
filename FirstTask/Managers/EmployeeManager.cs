﻿using AutoMapper;
using FirstTask.Models;
using FirstTask.ViewQueris;

using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;
using FirstTaskEntities.Query;
using FirstTaskEntities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
		public List<EmployeeModel> List(EmployeeViewQuery queryView)
		{
			var query = mapper.Map<EmployeeQueryList>(queryView);
			var entities = emloyeeRep.List(query);
			var models = mapper.Map<List<EmployeeModel>>(entities);

			foreach (var el in models)
			{
				var companyId = entities.First(x => x.Id == el.Id).CompanyId;
				var companyEntity = companyRep.Find(companyId);
				el.Company = mapper.Map<CompanyModel>(companyEntity);
			}

			return models;
		}

		
		public bool Edit(EmployeeModel model)
		{
			if (string.IsNullOrEmpty(model.PersonalNumber) || string.IsNullOrEmpty(model.FullName) || string.IsNullOrEmpty(model.Phone)
				|| model.DateOfBegin == null || model.BirthdayDate == null)
				return false;

			int minEmployeeAge = 18;

			if ((DateTime.Now - model.BirthdayDate).TotalHours < (minEmployeeAge * 365 * 24))
				return false;

			if (model.DateOfBegin <= DateTime.Now)
			{
				model.Status = Statuses.Active;
				model.DateOfFinish = null;
			}
			else
			{
				model.Status = Statuses.Disabled;
			}
			/*//0 - (000) - 000 - 00 - 00 || 000 - 000 - 000 - 000
			string phonePattern = "[0-9]{1}-([0-9]{3})-[0-9]{3}-[0-9]{2}-[0-9]{2}", personalNumberPattern = "[0-9]{3}-[0-9]{3}-[0-9]{3}-[0-9]{3}";
			if (!Regex.IsMatch(model.Phone, phonePattern) || !Regex.IsMatch(model.PersonalNumber, personalNumberPattern))
				return false;*/

			model.Phone = model.Phone.Replace("-", "").Replace("(", "").Replace(")", "");
			model.PersonalNumber = model.PersonalNumber.Replace("-", "").Replace(" ", "");
			var entity = mapper.Map<Employee>(model);
			entity.CompanyId = model.Company.Id;

			try
			{
				emloyeeRep.Update(entity);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Add(EmployeeModel model)
		{
			if (string.IsNullOrEmpty(model.FullName) || string.IsNullOrEmpty(model.PersonalNumber) || string.IsNullOrEmpty(model.Phone)
				|| model.BirthdayDate == null || model.DateOfBegin == null)
				return false;

			int minEmployeeAge = 18;

			if ((DateTime.Now - model.BirthdayDate).TotalHours < (minEmployeeAge * 365 * 24))
				return false;


			if (model.DateOfBegin <= DateTime.Now)
				model.Status = Statuses.Active;
			else
				model.Status = Statuses.Disabled;

			model.Phone = model.Phone.Replace("-", "").Replace("(", "").Replace(")", "");
			model.PersonalNumber = model.PersonalNumber.Replace("-", "").Replace(" ", "");
			var entity = mapper.Map<Employee>(model);
			entity.CompanyId = model.Company.Id;

			try
			{
				emloyeeRep.Add(entity);

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