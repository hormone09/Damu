using AutoMapper;

using FirstTask.App_Start;
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

namespace FirstTask.Managers
{
	public class ServiceManager
	{
		private ServicesRepository rep = new ServicesRepository();
		private IMapper mapper;

		public ServiceManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public List<ServiceModel> List(ServiceViewQuery viewQuery)
		{
			var query = mapper.Map<ServiceQueryList>(viewQuery);
			var serviceEntities = rep.List(query);
			var serviceModels = mapper.Map<List<ServiceModel>>(serviceEntities);

			return serviceModels;
		}

		public bool Edit(ServiceModel model)
		{
			if (string.IsNullOrEmpty(model.Code) || string.IsNullOrEmpty(model.Name) || model.Price <= 0 || model.Id <= 0 )
				return false;

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
				rep.Update(entity);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Add(ServiceModel model)
		{
			if (string.IsNullOrEmpty(model.Code) || string.IsNullOrEmpty(model.Name) || model.Price <= 0)
				return false;

			if (model.DateOfBegin <= DateTime.Now)
				model.Status = Statuses.Active;
			else
				model.Status = Statuses.Disabled;

			var entity = mapper.Map<Service>(model);

			try
			{
				rep.Add(entity);

				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		public bool Delete(int id)
		{
			try
			{
				rep.Remove(id);

				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}
	}
}