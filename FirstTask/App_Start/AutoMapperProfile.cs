using AutoMapper;
using FirstTask.Models;
using FirstTask.ViewQueris;

using FirstTaskEntities.Models;
using FirstTaskEntities.Query;

namespace FirstTask.App_Start
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
				.ForMember("ServicePrice", opt => opt.Ignore());
			CreateMap<ServiceProvidedModel, ServiceProvided>();
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

			CreateMap<ServicesHistoryViewQuery, ServiceHistoryQueryList>();
		}
	}
}