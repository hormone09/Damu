using AutoMapper;
using ServiceRegister.Handlers;
using ServiceRegister.Models;
using ServiceRegister.Resources;
using ServiceRegister.ViewQueris;
using Entities.Enums;
using Entities.Models;
using Entities.Query;
using Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceRegister.Managers
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
			queryView.Page = queryView.Page ?? 1;
			queryView.PageSize = queryView.PageSize ?? 20;

			if (queryView.SortingType != null && !SortingTypeHandler.Ckeck(typeof(EmployeeModel), queryView.SortingType))
				throw new Exception(Resource.ExceptionSortingType);

			if ((int)queryView.Status < 0 && (int)queryView.Status > 2)
				throw new Exception(Resource.ExceptionStatus);

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

			if ((int)model.Status < 1)
				throw new Exception(Resource.ExceptionStatus);

			var entity = mapper.Map<Employee>(model);
			entity.CompanyId = (int)model.Company.Id;

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
			entity.CompanyId = (int)model.Company.Id;

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