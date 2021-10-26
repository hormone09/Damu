using AutoMapper;
using FirstTask.Models;
using FirstTask.ViewQueris;
using FirstTaskEntities.Query;
using FirstTaskEntities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using FirstTask.Enums;
using FirstTask.Handlers;
using FirstTaskEntities.Models;
using FirstTaskEntities.Enums;

namespace FirstTask.Managers
{
	public class ServicesHistoryManager
	{
		private ServicesHistoryRepository historyRep = new ServicesHistoryRepository();
		private CompanyRepository companyRep = new CompanyRepository();
		private EmployeeRepository emloyeeRep = new EmployeeRepository();
		private ServicesRepository servicesRep = new ServicesRepository();
		private MessagesStrings strings = new MessagesStrings();
		private IMapper mapper;

		public ServicesHistoryManager(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public List<ServicesHistoryModel> List(ServicesHistoryViewQuery queryView)
		{
			queryView.Status = FirstTaskEntities.Enums.Statuses.Active;
			var query = mapper.Map<ServiceHistoryQueryList>(queryView);
			var entities = historyRep.List(query);
			var models = new List<ServicesHistoryModel>();

			foreach (var el in entities)
			{
				var companyEntity = companyRep.Find(el.CompanyId);
				var employeeEntity = emloyeeRep.Find(el.EmployeeId);
				var serviceEntity = servicesRep.Find(el.ServiceId);

				var model = new ServicesHistoryModel();
				model.Id = el.Id;
				model.Company = mapper.Map<CompanyModel>(companyEntity);
				model.Service = mapper.Map<ServiceModel>(serviceEntity);
				model.Employee = mapper.Map<EmployeeModel>(employeeEntity);
				model.DateOfCreate = el.DateOfCreate;
				model.DateOfFinish = el.DateOfCreate.AddMinutes(15.00);
				// TODO: Change on JS Hedler
				model.Title = $"Наименование услуги: {model.Service.Name} \n Стоимость: {model.Service.Price} \n Выполнил сотрудник компании: {model.Company.Name} \n Имя: {model.Employee.FullName}";

				models.Add(model);
			}

			return models;
		}

		public MessageHandler Add(ServicesHistoryModel model)
		{
			if (model.Company == null || model.Service == null || model.Employee == null || model.DateOfCreate == null)
				return new MessageHandler(false, strings.FormError);

			var entity = new ServicesHistory
			{
				DateOfCreate = (DateTime)model.DateOfCreate,
				CompanyId = model.Company.Id,
				EmployeeId = model.Employee.Id,
				ServiceId = model.Service.Id,
				Status = Statuses.Active
			};

			try
			{
				historyRep.Add(entity);

				return new MessageHandler(true, strings.AddSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}

		public MessageHandler Update(ServicesHistoryModel model)
		{
			if (model.Company == null || model.Service == null || model.Employee == null || model.DateOfCreate == null)
				return new MessageHandler(false, strings.FormError);

			var entity = new ServicesHistory
			{
				Id = model.Id,
				DateOfCreate = (DateTime)model.DateOfCreate,
				CompanyId = model.Company.Id,
				EmployeeId = model.Employee.Id,
				ServiceId = model.Service.Id,
				Status = Statuses.Active
			};

			try
			{
				historyRep.Update(entity);

				return new MessageHandler(true, strings.EditSuccess);
			}
			catch (Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}

		public MessageHandler Remove(int id)
		{
			try
			{
				historyRep.Remove(id);

				return new MessageHandler(true, strings.DeleteSuccess);
			}
			catch(Exception)
			{
				return new MessageHandler(false, strings.DatabaseError);
			}
		}

		public StiMvcActionResult GetReport(ReportViewQuery query)
		{
			if (query.Date == null)
				query.Date = DateTime.Now;

			string filterEmpty = "'Все'";
			string hasFilter = "Задана";
			string companyName = filterEmpty, serviceName = filterEmpty, employeeName = filterEmpty;

			if (query.ServiceId != null)
				serviceName = hasFilter;
			if (query.EmployeeId != null)
				employeeName = hasFilter;
			if (query.CompanyId != null)
				companyName = hasFilter;

			var report = new StiReport();
			report.Load(query.Path);

			report["Date"] = query.Date;
			report["CompanyId"] = query.CompanyId;
			report["EmployeeId"] = query.EmployeeId;
			report["ServiceId"] = query.ServiceId;
			report.Dictionary.Variables["ServiceFilter"].Value = serviceName;
			report.Dictionary.Variables["CompanyFilter"].Value = companyName;
			report.Dictionary.Variables["EmployeeFilter"].Value = employeeName;
			report.Dictionary.Variables["Date"].Value = ((DateTime)query.Date).ToLongDateString();

			switch (query.Type)
			{
				case ReportTypes.EXEL:
					return StiMvcReportResponse.ResponseAsExcel2007(report);
				case ReportTypes.WORD:
					return StiMvcReportResponse.ResponseAsWord2007(report);
				case ReportTypes.PDF:
					return StiMvcReportResponse.ResponseAsPdf(report);
				case ReportTypes.XML:
					return StiMvcReportResponse.ResponseAsXml(report);
				case ReportTypes.PNG:
					return StiMvcReportResponse.ResponseAsPng(report);
				default:
					return StiMvcReportResponse.ResponseAsPdf(report);
			}
		}
	}
}