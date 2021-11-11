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
	public class ServiceProvidedManager
	{
		private ServiceProvidedRepository providedRep;
		private CompanyRepository companyRep;
		private ServiceRepository servicesRep;
		private IMapper mapper;

		public ServiceProvidedManager(IMapper mapper, ServiceProvidedRepository providedRep, CompanyRepository companyRep, ServiceRepository servicesRep)
		{
			this.providedRep = providedRep;
			this.companyRep = companyRep;
			this.servicesRep = servicesRep;
			this.mapper = mapper;
		}
		
		public List<ServiceProvidedModel> List(ServiceProvidedViewQuery queryView)
		{
			queryView.Page = queryView.Page ?? 1;
			queryView.PageSize = queryView.PageSize ?? 20;

			if (queryView.SortingType != null && !SortingTypeHandler.Ckeck(typeof(ServiceProvidedModel), queryView.SortingType))
				throw new Exception(Resource.ExceptionSortingType);

			if ((int)queryView.Status < 0 && (int)queryView.Status > 2)
				throw new Exception(Resource.ExceptionStatus);

			var query = mapper.Map<ServiceProvidedQueryList>(queryView);
			var entities = providedRep.List(query);
			var models = mapper.Map<List<ServiceProvidedModel>>(entities);

			return models;
		}

		public MessageHandler Edit(ServiceProvidedModel model)
		{
			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			if ((int)model.Status < 1)
				throw new Exception(Resource.ExceptionStatus);

			var entity = mapper.Map<ServiceProvided>(model);

			try
			{
				providedRep.Update(entity);

				return new MessageHandler(true, Resource.EditSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}

		public MessageHandler Add(ServiceProvidedModel model)
		{
			// Сопоставить с организацией
			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;

			var entity = mapper.Map<ServiceProvided>(model);

			try
			{
				providedRep.Add(entity);

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
				providedRep.Remove(id);

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
				var entity = providedRep.Find(id);

				entity.Status = (int)Statuses.Active;
				entity.DateOfBegin = DateTime.Now;
				entity.DateOfFinish = null;

				providedRep.Update(entity);

				return new MessageHandler(true, Resource.ActivateSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}
	}
}