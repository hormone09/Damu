using FirstTask.ViewModels;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;
using FirstTaskEntities.Query;
using FirstTaskEntities.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace FirstTask.Managers
{
	public class ServiceManager
	{
		private int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
		private ServicesRepository rep = new ServicesRepository();

		public ServiceViewModel GetPageResult(ServiceViewModel model)
		{
			int skip = 0;

			if (model.Page == null)
				model.Page = 1;
			else
				skip = (int)model.Page * pageSize;

			var query = new ServiceListQuery
			{
				Price = model.Price,
				ServiceName = model.ServiceName,
				Status = model.Status,
				Date1 = model.Date1,
				Date2 = model.Date2,
				Skip = skip,
				Limit = pageSize
			};

			model.RowNumber = 40;
			model.Limit = pageSize;
			model.Items = rep.List(query);

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


		public void Test()
		{

		}
	}
}