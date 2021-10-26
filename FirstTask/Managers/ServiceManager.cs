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
			if (string.IsNullOrEmpty(model.Code) || string.IsNullOrEmpty(model.Name) || model.Price <= 0 || model.Id <= 0)
				return new MessageHandler(false, strings.FormError);

			string pattern = "[A-z]{1}[0-9]{2}.[0-9]{3}.[0-9]{3}$";
			if (!Regex.IsMatch(model.Code, pattern))
				return new MessageHandler(false, strings.FormatError);

			if (model.DateOfBegin <= DateTime.Now)
			{
				model.Status = Statuses.Active;
				model.DateOfFinish = null;
			}
			else
			{
				model.Status = Statuses.Disabled;
			}

			var entity = mapper.Map<Service>(model);

			try
			{
				serviceRepository.Update(entity);

				return new MessageHandler(true, strings.EditSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}

		public MessageHandler Add(ServiceModel model)
		{
			if (string.IsNullOrEmpty(model.Code) || string.IsNullOrEmpty(model.Name) || model.Price <= 0)
				return new MessageHandler(false, strings.FormError);

			string pattern = "[A-z]{1}[0-9]{2}.[0-9]{3}.[0-9]{3}$";
			if (!Regex.IsMatch(model.Code, pattern))
				return new MessageHandler(false, strings.FormError);

			if (model.DateOfBegin <= DateTime.Now)
				model.Status = Statuses.Active;
			else
				model.Status = Statuses.Disabled;

			var entity = mapper.Map<Service>(model);

			try
			{
				serviceRepository.Add(entity);

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
				serviceRepository.Remove(id);

				var serviceProvidedEntities = serviceProvidedRepository.List(new ServiceProvidedQueryList { ServiceId = id, Status = Statuses.Active, Skip = 0, Limit = 100000000 });
				foreach (var el in serviceProvidedEntities)
					serviceProvidedRepository.Remove(el.Id);

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
				serviceRepository.Activate(id);

				return new MessageHandler(true, strings.ActivateSuccess);
			}
			catch(Exception)
			{
				return new MessageHandler(false, strings.ActivateFailed);
			}
		}
	}
}