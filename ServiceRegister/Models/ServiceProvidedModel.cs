using ServiceRegister.Resources;
using Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceRegister.Models
{
	public class ServiceProvidedModel
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Оказываемая услуга
		/// </summary>
		public ServiceModel Service{ get; set; }

		/// <summary>
		/// Организация, оказывающая услугу
		/// </summary>
		public CompanyModel Company { get; set; }

		/// <summary>
		/// Отдельная стоимость проведения услуги, для конкретного случая
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public decimal? ServicePrice { get; set; }

		/// <summary>
		/// Дата начала оказания услуги
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public DateTime? DateOfBegin { get; set; }

		/// <summary>
		/// Дата окончания оказания услуги
		/// </summary>
		public DateTime? DateOfFinish { get; set; }

		/// <summary>
		/// Статус записи в БД
		/// </summary>
		public Statuses Status { get; set; }
		public int TotalRows { get; set; }
	}
}