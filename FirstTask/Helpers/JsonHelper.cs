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
		public static string SerializeSuccesList(JArray array)
		{
			dynamic obj = new JObject();

			obj.IsSuccess = true;
			obj.Error = "none";
			obj.Items = array;

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