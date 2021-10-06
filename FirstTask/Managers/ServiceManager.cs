using FirstTaskEntities.Enums;
using FirstTaskEntities.Interfaces;
using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FirstTask.Managers
{
	public class ServiceManager
	{
		private IRepository<Service> rep;
		private int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

		public ServiceManager(IRepository<Service> rep)
		{
			this.rep = rep;
		}

		public List<Service> GetPageResult(int? page, out int pageCount)
		{
			pageCount = 1;

			List<Service> result;
			int indent;

			if (page == null)
			{
				indent = 0;
				page = 1;
			}
			else
				indent = (int)page * pageSize;

			string query = "SELECT * FROM Services WHERE Status = @Status ORDER BY Id OFFSET @Indent ROWS FETCH NEXT @PageSize ROWS ONLY";
			object param = new { Status = Statuses.Active, Indent = indent, PageSize = pageSize};
			result = rep.List(query, param);

			return result;
		}

		public bool Delete(int id)
		{
			try
			{
				rep.Remove(id);

				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}
	}
}