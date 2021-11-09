using FirstTask.Resources;
using FirstTaskEntities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstTask.Models
{
	public class ServiceModel
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int? Id { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[StringLength(300, MinimumLength = 6, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmployeeNameLength")]
		public string Name { get; set; }

		/// <summary>
		/// Персональный код
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[RegularExpression("[A-z]{1}[0-9]{2}.[0-9]{3}.[0-9]{3}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FormatServiceCode")]
		public string Code { get; set; }

		/// <summary>
		/// Дата начала действия услуги
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public DateTime? DateOfBegin { get; set; }

		/// <summary>
		/// Дата окончания действия - удаление
		/// </summary>
		public DateTime? DateOfFinish { get; set; }

		/// <summary>
		/// Стоимость услуги
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public decimal Price { get; set; }

		/// <summary>
		/// Статус записи в БД
		/// </summary>
		public Statuses Status { get; set; }
		public int TotalRows { get; set; }
	}
}