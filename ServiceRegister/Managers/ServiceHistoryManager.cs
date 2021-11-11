using AutoMapper;
using ServiceRegister.Enums;
using ServiceRegister.Handlers;
using ServiceRegister.Models;
using ServiceRegister.Resources;
using ServiceRegister.ViewQueris;
using Entities.Enums;
using Entities.Models;
using Entities.Query;
using Entities.Repository;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace ServiceRegister.Managers
{
	public class ServiceHistoryManager
	{
		private ServiceHistoryRepository historyRep;
		private CompanyRepository companyRep;
		private EmployeeRepository emloyeeRep;
		private ServiceRepository servicesRep;
		private IMapper mapper;

		public ServiceHistoryManager(IMapper mapper, ServiceHistoryRepository historyRep, CompanyRepository companyRep, EmployeeRepository employeeRep, ServiceRepository servicesRep)
		{
			this.historyRep = historyRep;
			this.companyRep = companyRep;
			this.emloyeeRep = employeeRep;
			this.servicesRep = servicesRep;
			this.mapper = mapper;
		}

		public List<ServiceHistoryModel> List(ServicesHistoryViewQuery queryView)
		{
			queryView.Page = queryView.Page ?? 1;
			queryView.PageSize = queryView.PageSize ?? 20;

			queryView.Status = Statuses.Active;
			queryView.DateBegin = queryView.DateBegin.Date;
			queryView.DateEnd = queryView.DateEnd.Date;
			var query = mapper.Map<ServiceHistoryQueryList>(queryView);
			var entities = historyRep.List(query);
			var models = mapper.Map<List<ServiceHistoryModel>>(entities);

			return models;
		}

		public MessageHandler Add(ServiceHistoryModel model)
		{
			if (!Validator.TryValidateObject(model, new System.ComponentModel.DataAnnotations.ValidationContext(model), new List<ValidationResult>()))
				return new MessageHandler(false, Resource.ValidationMessageFormatError);

			model.DateOfFinish = ((DateTime)model.DateOfCreate).AddMinutes(15.00);
			var entity = mapper.Map<ServiceHistory>(model);

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
			if (!Validator.TryValidateObject(model, new System.ComponentModel.DataAnnotations.ValidationContext(model), new List<ValidationResult>()))
				return new MessageHandler(false, Resource.ValidationMessageFormatError);

			model.DateOfFinish = ((DateTime)model.DateOfCreate).AddMinutes(15.00);
			var entity = mapper.Map<ServiceHistory>(model);

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
			string companyName = filterEmpty, serviceName = filterEmpty, employeeName = filterEmpty;

			if (query.Service != null && query.Service.Id > 0)
				serviceName = query.Service.Name;
			else
				query.Service = new ServiceModel();

			if (query.Employee != null && query.Employee.Id > 0)
				employeeName = query.Employee.FullName;
			else
				query.Employee = new EmployeeModel();

			if (query.Company != null && query.Company.Id > 0)
				companyName = query.Company.Name;
			else
				query.Company = new CompanyModel();

			var report = new StiReport();
			report.Load(query.Path);

			report["DateBegin"] = query.DateBegin;
			report["DateEnd"] = query.DateEnd;
			report["CompanyId"] = query.Company.Id;
			report["EmployeeId"] = query.Employee.Id;
			report["ServiceId"] = query.Service.Id;
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