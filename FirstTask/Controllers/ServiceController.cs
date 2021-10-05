using FirstTaskEntities.Models;
using FirstTaskEntities.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FirstTask.Helpers;
using FirstTaskEntities.Enums;

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
		public string FindCurrentServices(string name, ServiceStatuses? status)
		{
			string error = "none";
			var result = new List<Service>();

			try
			{
				result = serviceRep.GetAll().Where(x => x.Status == ServiceStatuses.Active).ToList();
			}
			catch(Exception ex)
			{
				error = $"Ошибка работы с БД: {ex.Message}";
			}

			if(string.IsNullOrEmpty(name) && status == null)
			{
				error = "Параметры для поиска не заданы!";
			}
			else
			{
				if (name != null)
					result = result.Where(x => x.Name == name).ToList();
				if (status != null)
					result = result.Where(x => x.Status == status).ToList();
			}

			if(result.Any())
			{
				dynamic array = new JArray();

				foreach(Service el in result)
				{
					dynamic temp = new JObject();
					temp.Name = el.Name;
					temp.Code = el.Code;
					temp.Price = el.Price;
					temp.Status = el.Status;

					array.Add(temp);
				}

				return JsonHelper.SerializeSuccesList(array);
			}
			else
			{
				error = "По заданным параметрам ничего не найдено!";

				return JsonHelper.SerializeErorr(error);
			}
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
		public string RemoveService(int id)
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