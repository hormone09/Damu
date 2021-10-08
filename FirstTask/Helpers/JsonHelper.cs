using FirstTask.ViewModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Helpers
{
	public static class JsonHelper
	{
		public static string SerializeSeriviceViewModel(ServiceViewModel model)
		{
			dynamic obj = new JObject();

			obj.IsSuccess = true;
			obj.Error = "none";
			obj.RowNumber = model.RowNumber;
			obj.PageSize = model.Limit;
			obj.Page = model.Page;
			obj.Items = new JArray();

			foreach(var el in model.Items)
			{
				dynamic temp = new JObject();
				temp.ServiceName = model.ServiceName;
				temp.Price = model.Price;
				temp.Status = model.Status;
				temp.Date1 = model.Date1;
				temp.Date2 = model.Date2;

				obj.Items.Add(temp);
			}

			return JsonConvert.SerializeObject(obj);
		}

		public static string SerializeErorr(string error)
		{
			dynamic obj = new JObject();

			obj.IsSuccess = false;
			obj.Error = error;

			return JsonConvert.SerializeObject(obj);
		}

		public static string SerializeSuccess(string message)
		{
			dynamic obj = new JObject();

			obj.IsSuccess = true;
			obj.Message = message;

			return JsonConvert.SerializeObject(obj);
		}
	}
}