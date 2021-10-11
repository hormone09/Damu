using AutoMapper;

using FirstTask.ViewModels;

using FirstTaskEntities.Models;
using FirstTaskEntities.Query;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
		}
	}
}