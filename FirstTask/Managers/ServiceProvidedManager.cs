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
	public class ServiceProvidedManager
	{
		private ServiceProvidedRepository providedRepository = new ServiceProvidedRepository();
		private CompanyRepository companyRepository = new CompanyRepository();
		private ServiceRepository servicesRepository = new ServiceRepository();
		private IMapper mapper;

		public ServiceProvidedManager(IMapper mapper)
		{
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
			var entities = providedRepository.List(query);
			var models = mapper.Map<List<ServiceProvidedModel>>(entities);

			foreach(var el in models)
			{
				var entity = entities.First(x => x.Id == el.Id);

				var companyEntity = companyRepository.Find(entity.CompanyId);
				var serviceEntity = servicesRepository.Find(entity.ServiceId);

				el.Company = mapper.Map<CompanyModel>(companyEntity);
				el.Service = mapper.Map<ServiceModel>(serviceEntity);
			}

			return models;
		}

		public MessageHandler Edit(ServiceProvidedModel model)
		{
			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			if ((int)model.Status < 1)
				throw new Exception(Resource.ExceptionStatus);

			var entity = mapper.Map<ServiceProvided>(model);
			entity.CompanyId = (int)model.Company.Id;
			entity.ServiceId = (int)model.Service.Id;

			try
			{
				providedRepository.Update(entity);

				return new MessageHandler(true, Resource.EditSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public MessageHandler Add(ServiceProvidedModel model)
		{
			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;

			var entity = mapper.Map<ServiceProvided>(model);

			try
			{
				providedRepository.Add(entity);

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
				providedRepository.Remove(id);

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
				var entity = providedRepository.Find(id);

				entity.Status = (int)Statuses.Active;
				entity.DateOfBegin = DateTime.Now;
				entity.DateOfFinish = null;

				providedRepository.Update(entity);

				return new MessageHandler(true, Resource.ActivateSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}