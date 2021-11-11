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

namespace ServiceRegister.Managers
{
	public class CompanyManager
	{
		private CompanyRepository companyRep;
		private IMapper mapper;

		public CompanyManager(IMapper mapper, CompanyRepository companyRep)
		{
			this.companyRep = companyRep;
			this.mapper = mapper;
		}

		public List<CompanyModel> List(CompanyViewQuery queryView)
		{
			queryView.Page = queryView.Page ?? 1;
			queryView.PageSize = queryView.PageSize ?? 20;

			if(queryView.SortingType != null && !SortingTypeHandler.Ckeck(typeof(CompanyModel), queryView.SortingType))
				throw new Exception(Resource.ExceptionSortingType);

			if ((int)queryView.Status < 0 && (int)queryView.Status > 2)
				throw new Exception(Resource.ExceptionStatus);

			var query = mapper.Map<CompanyQueryList>(queryView);
			var entities = companyRep.List(query);
			var models = mapper.Map<List<CompanyModel>>(entities);

			return models;
		}

		public MessageHandler Edit(CompanyModel model)
		{
			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			if ((int)model.Status < 1)
				throw new Exception(Resource.ExceptionStatus);

			var entity = mapper.Map<Company>(model);
			
			try
			{
				companyRep.Update(entity);

				return new MessageHandler(true, Resource.EditSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}

		public MessageHandler Add(CompanyModel model)
		{

			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			var entity = mapper.Map<Company>(model);

			try
			{
				companyRep.Add(entity);

				return new MessageHandler(true, Resource.AddSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}

		public MessageHandler Delete(int id)
		{
			try
			{
				companyRep.Remove(id);

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
				var entity = companyRep.Find(id);

				entity.DateOfBegin = DateTime.Now;
				entity.DateOfFinish = null;
				entity.Status = (int)Statuses.Active;

				companyRep.Update(entity);

				return new MessageHandler(true, Resource.ActivateSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}
	}
}