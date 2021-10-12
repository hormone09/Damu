using AutoMapper;

using FirstTask.Models;
using FirstTask.ViewModels;

using FirstTaskEntities.Models;
using FirstTaskEntities.Query;

namespace FirstTask.App_Start
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<ServiceViewModel, ServiceQueryList>()
				.ForMember("Skip", opt => opt.MapFrom(x => (x.Page - 1) * x.PageSize))
				.ForMember("Limit", opt => opt.MapFrom(x => x.PageSize));

			CreateMap<CompanyViewModel, CompanyQueryList>()
				.ForMember("Skip", opt => opt.MapFrom(x => (x.Page - 1) * x.PageSize))
				.ForMember("Limit", opt => opt.MapFrom(x => x.PageSize));

			CreateMap<EmployeeViewModel, EmployeeQueryList>()
				.ForMember("Skip", opt => opt.MapFrom(x => (x.Page - 1) * x.PageSize))
				.ForMember("Limit", opt => opt.MapFrom(x => x.PageSize));

			CreateMap<EmployeeModel, Employee>()
				.ForMember(obj => obj.CompanyId, opt => opt.MapFrom(x => x.Company.Id));
			CreateMap<Employee, EmployeeModel>()
				.ForMember(obj => obj.Company, opt => opt.MapFrom(x => new Company { Id = x.CompanyId }));
		}
	}
}