using AutoMapper;
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
using System.Data.SqlClient;

namespace FirstTask.Managers
{
	public class ServiceManager
	{
		private MessagesStrings strings = new MessagesStrings();

		private ServiceRepository serviceRepository = new ServiceRepository();
		private ServiceProvidedRepository serviceProvidedRepository = new ServiceProvidedRepository();
		private IMapper mapper;

		public ServiceManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public List<ServiceModel> List(ServiceViewQuery queryView)
		{
			if (queryView.Page == null)
			{
				queryView.Page = 1;
				queryView.PageSize = 20;
			}

			var query = mapper.Map<ServiceQueryList>(queryView);
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
			string connectionString = ConfigurationManager.AppSettings["connection"];

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				SqlTransaction transaction = connection.BeginTransaction();
				SqlCommand command = connection.CreateCommand();

				command.Transaction = transaction;

				try
				{
					command.CommandText = "UPDATE ServiceProvided SET Status = @Status WHERE ServiceId = @ServiceId";
					command.Parameters.Add(new SqlParameter { Value = Statuses.Disabled, ParameterName = "Status" });
					command.Parameters.Add(new SqlParameter { Value = id, ParameterName = "ServiceId" });
					command.ExecuteNonQuery();

					command.CommandText = "UPDATE Services SET Status = @Status WHERE Id = @ServiceId";
					command.ExecuteNonQuery();

					transaction.Commit();

					return new MessageHandler(true, strings.DeleteSuccess);
				}
				catch (Exception ex)
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
				var entity = serviceRepository.Find(id);

				entity.Status = (int)Statuses.Active;
				entity.DateOfBegin = DateTime.Now;
				entity.DateOfFinish = null;

				serviceRepository.Update(entity);

				return new MessageHandler(true, strings.ActivateSuccess);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}