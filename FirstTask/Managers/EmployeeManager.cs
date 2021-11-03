using AutoMapper;
using FirstTask.Handlers;
using FirstTask.Models;
using FirstTask.Resources;
using FirstTask.ViewQueris;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;
using FirstTaskEntities.Query;
using FirstTaskEntities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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
			if (queryView.Page == null)
			{
				queryView.Page = 1;
				queryView.PageSize = 20;
			}

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
				return new MessageHandler(false, Resource.AgeError);

			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			var entity = mapper.Map<Employee>(model);
			entity.CompanyId = model.Company.Id;

			try
			{
				emloyeeRep.Update(entity);

				return new MessageHandler(true, Resource.EditSuccess);
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
				return new MessageHandler(false, Resource.AgeError);

			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			var entity = mapper.Map<Employee>(model);
			entity.CompanyId = model.Company.Id;

			try
			{
				emloyeeRep.Add(entity);

				return new MessageHandler(true, Resource.AddSuccess);
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

				return new MessageHandler(true, Resource.DeleteSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}

		public MessageHandler Activate(int id)
		{
			try
			{
				var entity = emloyeeRep.Find(id);

				entity.Status = (int)Statuses.Active;
				entity.DateOfBegin = DateTime.Now;
				entity.DateOfFinish = null;

				emloyeeRep.Update(entity);

				return new MessageHandler(true, Resource.ActivateSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}