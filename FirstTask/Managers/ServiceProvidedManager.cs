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
using System.Web;

namespace FirstTask.Managers
{
	public class ServiceProvidedManager
	{
		private MessagesStrings strings = new MessagesStrings();

		private ServiceProvidedRepository providedRepository = new ServiceProvidedRepository();
		private CompanyRepository companyRepository = new CompanyRepository();
		private ServicesRepository servicesRepository = new ServicesRepository();
		private IMapper mapper;

		public ServiceProvidedManager(IMapper mapper)
		{
			this.mapper = mapper;
		}
		
		public List<ServiceProvidedModel> List(ServiceProvidedViewQuery viewQuery)
		{
			var query = mapper.Map<ServiceProvidedQueryList>(viewQuery);
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
			if (model.Company == null || model.Service == null || model.DateOfBegin == null || model.ServicePrice == 0)
				return new MessageHandler(false, strings.FormError);

			if (model.DateOfBegin <= DateTime.Now)
			{
				model.Status = Statuses.Active;
				model.DateOfFinish = null;
			}
			else
			{
				model.Status = Statuses.Disabled;
			}

			var entity = mapper.Map<ServiceProvided>(model);
			entity.CompanyId = model.Company.Id;
			entity.ServiceId = model.Service.Id;

			var currentService = servicesRepository.Find(entity.ServiceId);
			currentService.Price = model.ServicePrice;


			try
			{
				providedRepository.Update(entity);
				servicesRepository.Update(currentService);

				return new MessageHandler(true, strings.EditSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}

		public MessageHandler Add(ServiceProvidedModel model)
		{
			if (model.Company == null || model.Service == null || model.DateOfBegin == null)
				return new MessageHandler(false, strings.FormError);

			if (model.DateOfBegin <= DateTime.Now)
				model.Status = Statuses.Active;
			else
				model.Status = Statuses.Disabled;

			var entity = mapper.Map<ServiceProvided>(model);

			var currentService = servicesRepository.Find(entity.ServiceId);
			currentService.Price = model.ServicePrice;

			try
			{
				providedRepository.Add(entity);
				servicesRepository.Update(currentService);

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
				providedRepository.Remove(id);

				return new MessageHandler(true, strings.DeleteSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}
	}
}