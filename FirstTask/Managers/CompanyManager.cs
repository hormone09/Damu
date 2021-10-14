using AutoMapper;

using FirstTask.Errors;
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
	public class CompanyManager
	{
		private CompanyRepository rep = new CompanyRepository();
		private IMapper mapper;

		public CompanyManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public List<CompanyModel> List(CompanyViewQuery queryView)
		{
			var query = mapper.Map<CompanyQueryList>(queryView);
			var entities = rep.List(query);
			var models = mapper.Map<List<CompanyModel>>(entities);

			return models;
		}
		public bool Edit(CompanyModel model)
		{
			if (string.IsNullOrEmpty(model.BIN) || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Phone) || model.Id <= 0)
				return false;

			if(model.DateOfBegin <= DateTime.Now)
			{
				model.Status = Statuses.Active;
				model.DateOfFinish = null;
			}
			else
			{
				model.Status = Statuses.Disabled;
			}

			model.BIN = model.BIN.Replace("-", "");
			model.Phone = model.Phone.Replace("(", "").Replace(")", "").Replace("-", "");
			var entity = mapper.Map<Company>(model);
			
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

		public bool Add(CompanyModel model)
		{
			if (string.IsNullOrEmpty(model.BIN) || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Phone))
				return false;

			if (model.DateOfBegin >= DateTime.Now)
				model.Status = Statuses.Active;
			else
				model.Status = Statuses.Disabled;

			model.BIN = model.BIN.Replace("-", "");
			model.Phone = model.Phone.Replace("-", "").Replace("(", "").Replace("(", "");
			var entity = mapper.Map<Company>(model);

			try
			{
				rep.Add(entity);

				return true;
			}
			catch (Exception)
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
			catch (Exception)
			{
				return false;
			}
		}
	}
}