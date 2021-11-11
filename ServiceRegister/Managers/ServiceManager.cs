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
using System.Configuration;
using System.Data.SqlClient;

namespace ServiceRegister.Managers
{
	public class ServiceManager
	{
		private ServiceRepository serviceRep;
		private IMapper mapper;

		public ServiceManager(IMapper mapper, ServiceRepository serviceRep)
		{
			this.serviceRep = serviceRep;
			this.mapper = mapper;
		}

		public List<ServiceModel> List(ServiceViewQuery queryView)
		{
			queryView.Page = queryView.Page ?? 1;
			queryView.PageSize = queryView.PageSize ?? 20;

			if (queryView.SortingType != null && !SortingTypeHandler.Ckeck(typeof(ServiceModel), queryView.SortingType))
				throw new Exception(Resource.ExceptionSortingType);

			if ((int)queryView.Status < 0 && (int)queryView.Status > 2)
				throw new Exception(Resource.ExceptionStatus);

			var query = mapper.Map<ServiceQueryList>(queryView);
			var serviceEntities = serviceRep.List(query);
			var serviceModels = mapper.Map<List<ServiceModel>>(serviceEntities);

			return serviceModels;
		}

		public MessageHandler Edit(ServiceModel model)
		{
			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			if ((int)model.Status < 1)
				throw new Exception(Resource.ExceptionStatus);

			var entity = mapper.Map<Service>(model);

			try
			{
				serviceRep.Update(entity);

				return new MessageHandler(true, Resource.EditSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
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
				serviceRep.Add(entity);

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
				serviceRep.Remove(id);

				return new MessageHandler(true, Resource.ActivateSuccess);
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
				var entity = serviceRep.Find(id);

				entity.Status = (int)Statuses.Active;
				entity.DateOfBegin = DateTime.Now;
				entity.DateOfFinish = null;

				serviceRep.Update(entity);

				return new MessageHandler(true, Resource.ActivateSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}
	}
}