using AutoMapper;

using FirstTask.Handlers;
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
		private MessagesStrings strings = new MessagesStrings();

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

		
		public MessageHandler Edit(EmployeeModel model)
		{
			if (string.IsNullOrEmpty(model.PersonalNumber) || string.IsNullOrEmpty(model.FullName) || string.IsNullOrEmpty(model.Phone)
				|| model.DateOfBegin == null || model.BirthdayDate == null || model.Company.Id == 0)
				return new MessageHandler(false, strings.FormError);

			int minEmployeeAge = 18;

			if ((DateTime.Now - model.BirthdayDate).TotalHours < (minEmployeeAge * 365 * 24))
				return new MessageHandler(false, strings.AgeError);

			if (model.DateOfBegin <= DateTime.Now)
			{
				model.Status = Statuses.Active;
				model.DateOfFinish = null;
			}
			else
			{
				model.Status = Statuses.Disabled;
			}

			string personalNumberPattern = @"[0-9]{3}-[0-9]{3}-[0-9]{3}-\d{3}$";
			string phonePattern = @"[0-9]{1}-[(]?[0-9]{3}[)]?-[0-9]{3}-[0-9]{2}-[0-9]{2}$";
			if (!Regex.IsMatch(model.PersonalNumber, personalNumberPattern) || !Regex.IsMatch(model.Phone, phonePattern))
				return new MessageHandler(false, strings.FormatError);

			model.Phone = model.Phone.Replace("-", "").Replace("(", "").Replace(")", "");
			model.PersonalNumber = model.PersonalNumber.Replace("-", "").Replace(" ", "");
			var entity = mapper.Map<Employee>(model);
			entity.CompanyId = model.Company.Id;

			try
			{
				emloyeeRep.Update(entity);

				return new MessageHandler(true, strings.EditSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}

		public MessageHandler Add(EmployeeModel model)
		{
			if (string.IsNullOrEmpty(model.FullName) || string.IsNullOrEmpty(model.PersonalNumber) || string.IsNullOrEmpty(model.Phone)
				|| model.BirthdayDate == null || model.DateOfBegin == null)
				return new MessageHandler(false, strings.FormError);

			int minEmployeeAge = 18;

			if ((DateTime.Now - model.BirthdayDate).TotalHours < (minEmployeeAge * 365 * 24))
				return new MessageHandler(false, strings.AgeError);


			if (model.DateOfBegin <= DateTime.Now)
				model.Status = Statuses.Active;
			else
				model.Status = Statuses.Disabled;

			string binPattern = @"[0-9]{3}-[0-9]{3}-[0-9]{3}-\d{3}$";
			string phonePattern = @"[0-9]{1}-[(]?[0-9]{3}[)]?-[0-9]{3}-[0-9]{2}-[0-9]{2}$";
			if (!Regex.IsMatch(model.PersonalNumber, binPattern) || !Regex.IsMatch(model.Phone, phonePattern))
				return new MessageHandler(false, strings.FormatError);

			model.Phone = model.Phone.Replace("-", "").Replace("(", "").Replace(")", "");
			model.PersonalNumber = model.PersonalNumber.Replace("-", "").Replace(" ", "");
			var entity = mapper.Map<Employee>(model);
			entity.CompanyId = model.Company.Id;

			try
			{
				emloyeeRep.Add(entity);

				return new MessageHandler(true, strings.AddSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}

		public MessageHandler Delete(int id)
		{
			try
			{
				emloyeeRep.Remove(id);

				return new MessageHandler(true, strings.DeleteSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}
	}
}