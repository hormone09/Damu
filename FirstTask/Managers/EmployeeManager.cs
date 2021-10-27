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
			int minEmployeeAge = 18;

			if ((DateTime.Now - (DateTime)model.BirthdayDate).TotalHours < (minEmployeeAge * 365 * 24))
				return new MessageHandler(false, strings.AgeError);

			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, strings.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			model.Phone = model.Phone.Replace("-", "").Replace("(", "").Replace(")", "");
			model.PersonalNumber = model.PersonalNumber.Replace("-", "").Replace(" ", "");
			var entity = mapper.Map<Employee>(model);
			entity.CompanyId = model.Company.Id;

			try
			{
				emloyeeRep.Update(entity);

				return new MessageHandler(true, strings.EditSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public MessageHandler Add(EmployeeModel model)
		{
			int minEmployeeAge = 18;

			if ((DateTime.Now - (DateTime)model.BirthdayDate).TotalHours < (minEmployeeAge * 365 * 24))
				return new MessageHandler(false, strings.AgeError);

			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, strings.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			model.Phone = model.Phone.Replace("-", "").Replace("(", "").Replace(")", "");
			model.PersonalNumber = model.PersonalNumber.Replace("-", "").Replace(" ", "");
			var entity = mapper.Map<Employee>(model);
			entity.CompanyId = model.Company.Id;

			try
			{
				emloyeeRep.Add(entity);

				return new MessageHandler(true, strings.AddSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
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

		public MessageHandler Activate(int id)
		{
			try
			{
				emloyeeRep.Activate(id);

				return new MessageHandler(true, strings.ActivateSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}