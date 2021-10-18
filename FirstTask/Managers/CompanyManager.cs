using AutoMapper;
using FirstTask.Models;
using FirstTask.ViewQueris;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;
using FirstTaskEntities.Query;
using FirstTaskEntities.Repository;
using System;
using System.Collections.Generic;
using FirstTask.Handlers;
using System.Text.RegularExpressions;

namespace FirstTask.Managers
{
	public class CompanyManager
	{
		private MessagesStrings strings = new MessagesStrings();

		private CompanyRepository companyRep = new CompanyRepository();
		private ServiceProvidedRepository serviceProvidedRepository = new ServiceProvidedRepository();

		private IMapper mapper;

		public CompanyManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public List<CompanyModel> List(CompanyViewQuery queryView)
		{
			var query = mapper.Map<CompanyQueryList>(queryView);
			var entities = companyRep.List(query);
			var models = mapper.Map<List<CompanyModel>>(entities);

			return models;
		}
		public MessageHandler Edit(CompanyModel model)
		{
			if (string.IsNullOrEmpty(model.BIN) || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Phone) || model.Id <= 0)
				return new MessageHandler(false, strings.FormError);

			if(model.DateOfBegin <= DateTime.Now)
			{
				model.Status = Statuses.Active;
				model.DateOfFinish = null;
			}
			else
			{
				model.Status = Statuses.Disabled;
			}

			string binPattern = @"[0-9]{3}-[0-9]{3}-[0-9]{3}-\d{3}$";
			string phonePattern = @"[0-9]{1}-[(]?[0-9]{3}[)]?-[0-9]{3}-[0-9]{2}-[0-9]{2}$";
			if(!Regex.IsMatch(model.BIN, binPattern) || !Regex.IsMatch(model.Phone, phonePattern))
				return new MessageHandler(false, strings.FormatError);

			model.BIN = model.BIN.Replace("-", "");
			model.Phone = model.Phone.Replace("(", "").Replace(")", "").Replace("-", "");
			var entity = mapper.Map<Company>(model);
			
			try
			{
				companyRep.Update(entity);

				return new MessageHandler(true, strings.EditSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}

		public MessageHandler Add(CompanyModel model)
		{
			if (string.IsNullOrEmpty(model.BIN) || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Phone))
				return new MessageHandler(false, strings.FormError);

			if (model.DateOfBegin >= DateTime.Now)
				model.Status = Statuses.Active;
			else
				model.Status = Statuses.Disabled;

			string binPattern = @"[0-9]{3}-[0-9]{3}-[0-9]{3}-\d{3}$";
			string phonePattern = @"[0-9]{1}-[(]?[0-9]{3}[)]?-[0-9]{3}-[0-9]{2}-[0-9]{2}$";
			if (!Regex.IsMatch(model.BIN, binPattern) || !Regex.IsMatch(model.Phone, phonePattern))
				return new MessageHandler(false, strings.FormatError);

			model.BIN = model.BIN.Replace("-", "");
			model.Phone = model.Phone.Replace("-", "").Replace("(", "").Replace("(", "");
			var entity = mapper.Map<Company>(model);

			try
			{
				companyRep.Add(entity);

				return new MessageHandler(true, strings.EditSuccess);
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
				companyRep.Remove(id);
				var serviceProvidedEntities = serviceProvidedRepository.List(new ServiceProvidedQueryList { CompanyId = id, Status = Statuses.Active, Skip = 0, Limit = 100000000});
				foreach (var el in serviceProvidedEntities)
					serviceProvidedRepository.Remove(el.Id);

				return new MessageHandler(true, strings.DeleteSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}
	}
}