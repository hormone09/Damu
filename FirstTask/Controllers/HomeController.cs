﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstTask.Helpers;
using FirstTaskEntities.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FirstTask.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}