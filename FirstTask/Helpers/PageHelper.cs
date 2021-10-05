using FirstTaskEntities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Helpers
{
	public static class PageHelper<T>
	{
		private const int pageSize = 20;

		public static List<T> GetPageResult(List<T> full, int? page, out int pagesCount)
		{
			if (page == null)
				page = 1;

			int skipCount = (Convert.ToInt32(page) - 1) * pageSize;
			var pageResult = full.Skip(skipCount).Take(pageSize).ToList();
			var servicesCount = full.Count();
			pagesCount = servicesCount / pageSize + 1;

			return pageResult;
		}
	}
}