using AutoMapper;
using FirstTask.Enums;
using FirstTask.Handlers;
using FirstTask.Models;
using FirstTask.Resources;
using FirstTask.ViewQueris;
using FirstTaskEntities.Enums;
using FirstTaskEntities.Models;
using FirstTaskEntities.Query;
using FirstTaskEntities.Repository;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;
using System.Collections.Generic;

namespace FirstTask.Managers
{
	public class ServiceHistoryManager
	{
		private ServiceHistoryRepository historyRep = new ServiceHistoryRepository();
		private CompanyRepository companyRep = new CompanyRepository();
		private EmployeeRepository emloyeeRep = new EmployeeRepository();
		private ServiceRepository servicesRep = new ServiceRepository();
		private IMapper mapper;

		public ServiceHistoryManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public List<ServiceHistoryModel> List(ServicesHistoryViewQuery queryView)
		{
			if (queryView.Page == null)
			{
				queryView.Page = 1;
				queryView.PageSize = 20;
			}

			queryView.Status = Statuses.Active;
			queryView.DateBegin = queryView.DateBegin.Date;
			queryView.DateEnd = queryView.DateEnd.Date;
			var query = mapper.Map<ServiceHistoryQueryList>(queryView);
			var entities = historyRep.List(query); 
			var models = new List<ServiceHistoryModel>();

			foreach (var el in entities)
			{
				var companyEntity = companyRep.Find(el.CompanyId);
				var employeeEntity = emloyeeRep.Find(el.EmployeeId);
				var serviceEntity = servicesRep.Find(el.ServiceId);

				var model = new ServiceHistoryModel();
				model.Id = el.Id;
				model.Company = mapper.Map<CompanyModel>(companyEntity);
				model.Service = mapper.Map<ServiceModel>(serviceEntity);
				model.Employee = mapper.Map<EmployeeModel>(employeeEntity);
				model.DateOfCreate = el.DateOfCreate;
				model.DateOfFinish = el.DateOfCreate.AddMinutes(15.00);

				model.Title = $"Наименование услуги: {model.Service.Name} \n Стоимость: {model.Service.Price} \n Выполнил сотрудник компании: {model.Company.Name} \n Имя: {model.Employee.FullName}";

				models.Add(model);
			}

			return models;
		}

		public MessageHandler Add(ServiceHistoryModel model)
		{
			if (model.Company.Id == null || model.Service.Id == null || model.Employee.Id == null || model.DateOfCreate == null)
				return new MessageHandler(false, Resource.FormError);

			var entity = new ServiceHistory
			{
				DateOfCreate = (DateTime)model.DateOfCreate,
				CompanyId = (int)model.Company.Id,
				EmployeeId = (int)model.Employee.Id,
				ServiceId = (int)model.Service.Id,
				Status = (int)Statuses.Active
			};

			try
			{
				historyRep.Add(entity);

				return new MessageHandler(true, Resource.AddSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}

		public MessageHandler Update(ServiceHistoryModel model)
		{
			if (model.Company.Id == null || model.Service.Id == null || model.Employee.Id == null || model.DateOfCreate == null)
				return new MessageHandler(false, Resource.FormError);

			var entity = new ServiceHistory
			{
				Id = model.Id,
				DateOfCreate = (DateTime)model.DateOfCreate,
				CompanyId = (int)model.Company.Id,
				EmployeeId = (int)model.Employee.Id,
				ServiceId = (int)model.Service.Id,
				Status = (int)Statuses.Active
			};

			try
			{
				historyRep.Update(entity);

				return new MessageHandler(true, Resource.EditSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}

		public MessageHandler Remove(int id)
		{
			try
			{
				historyRep.Remove(id);

				return new MessageHandler(true, Resource.DeleteSuccess);
			}
			catch(Exception)
			{
				return new MessageHandler(false, Resource.DatabaseError);
			}
		}

		public StiMvcActionResult GetReport(ReportViewQuery query)
		{
			if (query.DateBegin == null)
				query.DateBegin = DateTime.Now;
			if (query.DateEnd == null)
				query.DateEnd = query.DateBegin;

			string filterEmpty = "'Все'";
			string hasFilter = "Задана";
			string companyName = filterEmpty, serviceName = filterEmpty, employeeName = filterEmpty;

			if (query.ServiceId != null && query.ServiceId > 0)
				serviceName = hasFilter;
			if (query.EmployeeId != null && query.EmployeeId > 0)
				employeeName = hasFilter;
			if (query.CompanyId != null && query.CompanyId > 0)
				companyName = hasFilter;

			var report = new StiReport();
			report.Load(query.Path);

			report["DateBegin"] = query.DateBegin;
			report["DateEnd"] = query.DateEnd;
			report["CompanyId"] = query.CompanyId;
			report["EmployeeId"] = query.EmployeeId;
			report["ServiceId"] = query.ServiceId;
			report.Dictionary.Variables["ServiceFilter"].Value = serviceName;
			report.Dictionary.Variables["CompanyFilter"].Value = companyName;
			report.Dictionary.Variables["EmployeeFilter"].Value = employeeName;
			report.Dictionary.Variables["DateBegin"].Value = ((DateTime)query.DateBegin).ToLongDateString();
			report.Dictionary.Variables["DateEnd"].Value = ((DateTime)query.DateEnd).ToLongDateString();

			switch (query.Type)
			{
				case ReportTypesEnum.EXEL:
					return StiMvcReportResponse.ResponseAsExcel2007(report);
				case ReportTypesEnum.WORD:
					return StiMvcReportResponse.ResponseAsWord2007(report);
				case ReportTypesEnum.PDF:
					return StiMvcReportResponse.ResponseAsPdf(report);
				case ReportTypesEnum.XML:
					return StiMvcReportResponse.ResponseAsXml(report);
				case ReportTypesEnum.PNG:
					return StiMvcReportResponse.ResponseAsPng(report);
				default:
					return StiMvcReportResponse.ResponseAsPdf(report);
			}
		}
	}
}