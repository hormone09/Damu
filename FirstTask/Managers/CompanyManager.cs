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
	public class CompanyManager
	{
		private CompanyRepository companyRep = new CompanyRepository();
		private ServiceProvidedRepository serviceProvidedRepository = new ServiceProvidedRepository();

		private IMapper mapper;

		public CompanyManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public List<CompanyModel> List(CompanyViewQuery queryView)
		{
			if(queryView.Page == null)
			{
				queryView.Page = 1;
				queryView.PageSize = 20;
			}

			var query = mapper.Map<CompanyQueryList>(queryView);
			var entities = companyRep.List(query);
			var models = mapper.Map<List<CompanyModel>>(entities);

			return models;
		}
		public MessageHandler Edit(CompanyModel model)
		{
			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			var entity = mapper.Map<Company>(model);
			
			try
			{
				companyRep.Update(entity);

				return new MessageHandler(true, Resource.EditSuccess);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public MessageHandler Add(CompanyModel model)
		{

			if (model.DateOfBegin > DateTime.Now)
				return new MessageHandler(false, Resource.DateOfBeginNonCorrect);

			model.Status = Statuses.Active;
			model.DateOfFinish = null;
			var entity = mapper.Map<Company>(model);

			try
			{
				companyRep.Add(entity);

				return new MessageHandler(true, Resource.AddSuccess);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public MessageHandler Delete(int id)
		{
			string connectionString = ConfigurationManager.AppSettings["connection"];

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				SqlTransaction transaction = connection.BeginTransaction();

				SqlCommand command = connection.CreateCommand();
				command.Transaction = transaction;

				try
				{
					command.CommandText = "UPDATE ServiceProvided SET Status = @Status WHERE CompanyId = @CompanyId";
					command.Parameters.Add(new SqlParameter { Value = id, ParameterName = "CompanyId" });
					command.Parameters.Add(new SqlParameter { Value = Statuses.Disabled, ParameterName = "Status" });
					command.ExecuteNonQuery();

					command.CommandText = "UPDATE Companies SET Status = @Status WHERE Id = @CompanyId";
					command.ExecuteNonQuery();

					transaction.Commit(); 
					
					return new MessageHandler(true, Resource.DeleteSuccess);
				}
				catch(Exception ex)
				{
					transaction.Rollback();
					throw ex;
				}
			}
		}

		public MessageHandler Activate(int id)
		{
			try
			{
				var entity = companyRep.Find(id);

				entity.DateOfBegin = DateTime.Now;
				entity.DateOfFinish = null;
				entity.Status = (int)Statuses.Active;

				companyRep.Update(entity);

				return new MessageHandler(true, Resource.ActivateSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}