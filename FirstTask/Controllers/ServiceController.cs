using FirstTaskEntities.Models;
using FirstTaskEntities.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstTaskEntities.Models;
using FirstTask.Helpers;

namespace FirstTask.Controllers
{
    public class ServiceController : Controller
    {
		private readonly ServicesRepository serviceRep = new ServicesRepository();

		/*public ServiceController(ServicesRepository serviceRep)
		{
			this.serviceRep = serviceRep;
		}*/

		public ActionResult Index(int? page)
		{
			var services = serviceRep.GetAll();
			int pagesCount = 0;
			var pageResult = PageHelper<Service>.GetPageResult(services, page, out pagesCount);

			ViewBag.PagesCount = pagesCount;
			ViewBag.Model = pageResult;

			return View();
		}

		[HttpPost]
		public string AddService(string name, string code, decimal price, DateTime dateOfBegin)
		{
			bool IsSuccess = true;
			string error = "none";

			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code) || price < 0 || dateOfBegin.Hour < DateTime.Now.Hour)
			{
				IsSuccess = false;
				error = "Данные введены неверно!";
			}

			var newService = new Service
			{
				Name = name,
				Code = code,
				Price = price,
				DateOfBegin = dateOfBegin
			};

			try
			{
				serviceRep.Add(newService);
			}
			catch (Exception ex)
			{
				IsSuccess = false;
				error = $"Ошибка добавления в БД: {ex.Message}";
			}

			dynamic obj = new JObject();
			obj.Success = IsSuccess;
			obj.Error = error;
			var json = JsonConvert.SerializeObject(obj);

			return json;
		}

		[HttpPost]
		public string UpdateService(int id, string name, string code, decimal price, DateTime dateOfBegin)
		{
			bool IsSuccess = true;
			string error = "none";

			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code) || price < 0 || dateOfBegin.Hour < DateTime.Now.Hour)
			{
				IsSuccess = false;
				error = "Данные введены неверно!";
			}
			else if(id < 0)
				throw new Exception("Noncorrect Id!");

			var update = new Service
			{
				Name = name,
				Code = code,
				Price = price,
				DateOfBegin = dateOfBegin
			};

			try
			{
				serviceRep.Update(update, id);
			}
			catch (Exception ex)
			{
				IsSuccess = false;
				error = $"Ошибка добавления в БД: {ex.Message}";
			}

			dynamic obj = new JObject();
			obj.Success = IsSuccess;
			obj.Error = error;
			var json = JsonConvert.SerializeObject(obj);

			return json;
		}

		[HttpPost]
		public string FinishService(int id)
		{
			bool IsSuccess = true;
			string error = "none";

			if (id < 0)
				throw new Exception("Noncorrect Id!");

			try
			{
				serviceRep.Remove(id);
			}
			catch(Exception ex)
			{
				error = $"Ошибка работы с БД: {ex.Message}";
			}

			dynamic obj = new JObject();
			obj.Success = IsSuccess;
			obj.Error = error;
			var json = JsonConvert.SerializeObject(obj);

			return json;
		}
	}
}