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
using System.Configuration;
using System.Data.SqlClient;

namespace FirstTask.Managers
{
	public class ServiceManager
	{
		private ServiceRepository serviceRepository = new ServiceRepository();
		private ServiceProvidedRepository serviceProvidedRepository = new ServiceProvidedRepository();
		private IMapper mapper;

		public ServiceManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public List<ServiceModel> List(ServiceViewQuery queryView)
		{
			queryView.Page = queryView.Page ?? 1;
			queryView.PageSize = queryView.Page ?? 20;

			var query = mapper.Map<ServiceQueryList>(queryView);
			var serviceEntities = serviceRepository.List(query);
			var serviceModels = mapper.Map<List<ServiceModel>>(serviceEntities);

			return serviceModels;
		}

		public MessageHandler Edit(ServiceModel model)
		{

			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			if ((int)model.Status < 1)
				throw new Exception("Сервер не получил статус записи!");

			var entity = mapper.Map<Service>(model);

			try
			{
				serviceRepository.Update(entity);

				return new MessageHandler(true, Resource.EditSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public MessageHandler Add(ServiceModel model)
		{
			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			var entity = mapper.Map<Service>(model);

			try
			{
				serviceRepository.Add(entity);

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
				serviceRepository.Remove(id);

				return new MessageHandler(true, Resource.ActivateSuccess);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public MessageHandler Activate(int id)
		{
			try
			{
				var entity = serviceRepository.Find(id);

				entity.Status = (int)Statuses.Active;
				entity.DateOfBegin = DateTime.Now;
				entity.DateOfFinish = null;

				serviceRepository.Update(entity);

				return new MessageHandler(true, Resource.ActivateSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}