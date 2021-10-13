using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTask.Errors
{
	public class ActionStatus
	{
		public bool IsSucces { get; set; }
		public string Error { get; set; }
		public string Message { get; set; }

		public ActionStatus() { }
		public ActionStatus(bool isSuccess, string str)
		{
			IsSucces = isSuccess;

			if (IsSucces)
				Message = str;
			else
				Error = str;
		}
	}
}