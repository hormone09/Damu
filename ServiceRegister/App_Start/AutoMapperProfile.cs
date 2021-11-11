using AutoMapper;
using ServiceRegister.Models;
using ServiceRegister.ViewQueris;

using Entities.Models;
using Entities.Query;

namespace ServiceRegister.App_Start
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Service, ServiceModel>();
			CreateMap<ServiceModel, Service>();
			CreateMap<ServiceViewQuery, ServiceQueryList>()
				.ForMember("Skip", opt => opt.MapFrom(x => (x.Page - 1) * x.PageSize))
				.ForMember("Limit", opt => opt.MapFrom(x => x.PageSize));

			CreateMap<ServiceProvided, ServiceProvidedModel>()
				.ForMember(x => x.Company, opt => opt.MapFrom(x => x.Company))
				.ForMember(x => x.Service, opt => opt.MapFrom(x => x.Service));
			CreateMap<ServiceProvidedModel, ServiceProvided>()
				.ForMember(x => x.Company, opt => opt.MapFrom(x => x.Company))
				.ForMember(x => x.Service, opt => opt.MapFrom(x => x.Service));
			CreateMap<ServiceProvidedViewQuery, ServiceProvidedQueryList>()
				.ForMember("Skip", opt => opt.MapFrom(x => (x.Page - 1) * x.PageSize))
				.ForMember("Limit", opt => opt.MapFrom(x => x.PageSize));

			CreateMap<Company, CompanyModel>();
			CreateMap<CompanyModel, Company>();
			CreateMap<CompanyViewQuery, CompanyQueryList>()
				.ForMember("Skip", opt => opt.MapFrom(x => (x.Page - 1) * x.PageSize))
				.ForMember("Limit", opt => opt.MapFrom(x => x.PageSize));

			CreateMap<Employee, EmployeeModel>();
			CreateMap<EmployeeModel, Employee>();
			CreateMap<EmployeeViewQuery, EmployeeQueryList>()
				.ForMember("Skip", opt => opt.MapFrom(x => (x.Page - 1) * x.PageSize))
				.ForMember("Limit", opt => opt.MapFrom(x => x.PageSize));

			CreateMap<ServiceHistory, ServiceHistoryModel>();
			CreateMap<ServiceHistoryModel, ServiceHistory>()
				.ForMember(x => x.Status, y => y.MapFrom(src => 1));

			CreateMap<ServicesHistoryViewQuery, ServiceHistoryQueryList>();
		}
	}
}