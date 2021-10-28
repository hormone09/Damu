﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;

namespace FirstTask.Handlers
{
	public class MessagesStrings
	{
		private ResourceManager rm;
		private CultureInfo culture;

		public MessagesStrings()
		{
			rm = new ResourceManager("FirstTask.Resources.ManagerMessages", Assembly.GetExecutingAssembly());
			culture = Thread.CurrentThread.CurrentCulture;
		}

		public string AddSuccess { get { return rm.GetString("AddSuccess", culture); } }
		public string ActivateSuccess { get { return rm.GetString("ActivateSuccess", culture); } }
		public string ActivateFailed { get { return rm.GetString("ActivateFailed", culture); } }
		public string AgeError { get { return rm.GetString("AgeError", culture); } }
		public string DatabaseError { get { return rm.GetString("DatabaseError", culture); } }
		public string DeleteSuccess { get { return rm.GetString("DeleteSuccess", culture); } }
		public string DateOfBeginNonCorrect { get { return rm.GetString("DateOfBeginNonCorrect", culture); } }
		public string EditSuccess { get { return rm.GetString("EditSuccess", culture); } }
		public string FormError { get { return rm.GetString("FormError", culture); } }
		public string FormatError { get { return rm.GetString("FormatError"); } }
	}
}