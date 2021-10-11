using AutoMapper;

using FirstTask.App_Start;
using FirstTask.ViewModels;
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

		public ServiceViewModel GetServices(ServiceViewModel model)
		{
			var query = mapper.Map<ServiceQueryList>(model);
			
			var services = rep.List(query);
			model.Items = services;
			model.RowNumber = services.Any() ? services.FirstOrDefault().TotalRows : 0;

			return model;
		}

		public bool Edit(Service service)
		{
			if (string.IsNullOrEmpty(service.Code) || string.IsNullOrEmpty(service.Name) || service.Price <= 0 || service.Id <= 0 )
				return false;

			try
			{
				rep.Update(service);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Add(Service service)
		{
			if (string.IsNullOrEmpty(service.Code) || string.IsNullOrEmpty(service.Name) || service.Price <= 0)
				return false;

			if (service.DateOfBegin <= DateTime.Now)
				service.Status = Statuses.Active;
			else
				service.Status = Statuses.Disabled;

			try
			{
				rep.Add(service);

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