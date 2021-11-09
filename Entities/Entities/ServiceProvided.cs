using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public class ServiceProvided
	{
		// Добавить таблицу в БД и репозиторий
		public int Id { get; set; }

		/// <summary>
		/// Ссылка на оказываемую услугу
		/// </summary>
		public int ServiceId { get; set; }

		/// <summary>
		/// Ссылка на оказывающую данную услугу компанию
		/// </summary>
		public int CompanyId { get; set; }

		/// <summary>
		/// Итоговая стоимость оказанной услуги
		/// </summary>
		public decimal ServicePrice { get; set; }

		/// <summary>
		/// Дата начала оказания услуги компанией
		/// </summary>
		public DateTime DateOfBegin { get; set; }

		/// <summary>
		/// Дата окончания оказания услуги компанией
		/// </summary>
		public DateTime? DateOfFinish { get; set; }

		/// <summary>
		/// Статус активности услуги
		/// </summary>
		public int Status { get; set; }

		[NotMapped]
		public int TotalRows { get; set; }
	}
}
