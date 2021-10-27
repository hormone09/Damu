using AutoMapper;

using FirstTask.App_Start;
using FirstTask.Handlers;
using FirstTask.Models;
using FirstTask.ViewQueris;

using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;
using FirstTaskEntities.Query;
using FirstTaskEntities.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace FirstTask.Managers
{
	public class ServiceManager
	{
		private MessagesStrings strings = new MessagesStrings();

		private ServicesRepository serviceRepository = new ServicesRepository();
		private ServiceProvidedRepository serviceProvidedRepository = new ServiceProvidedRepository();
		private IMapper mapper;

		public ServiceManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public List<ServiceModel> List(ServiceViewQuery viewQuery)
		{
			var query = mapper.Map<ServiceQueryList>(viewQuery);
			var serviceEntities = serviceRepository.List(query);
			var serviceModels = mapper.Map<List<ServiceModel>>(serviceEntities);

			return serviceModels;
		}

		public MessageHandler Edit(ServiceModel model)
		{

			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, strings.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			var entity = mapper.Map<Service>(model);

			try
			{
				serviceRepository.Update(entity);

				return new MessageHandler(true, strings.EditSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public MessageHandler Add(ServiceModel model)
		{
			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, strings.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			var entity = mapper.Map<Service>(model);

			try
			{
				serviceRepository.Add(entity);

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
				serviceRepository.Remove(id);

				var serviceProvidedEntities = serviceProvidedRepository.List(new ServiceProvidedQueryList { ServiceId = id, Status = Statuses.Active, Skip = 0, Limit = 100000000 });
				foreach (var el in serviceProvidedEntities)
					serviceProvidedRepository.Remove(el.Id);

				return new MessageHandler(true, strings.DeleteSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public MessageHandler Activate(int id)
		{
			try
			{
				serviceRepository.Activate(id);

				return new MessageHandler(true, strings.ActivateSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}