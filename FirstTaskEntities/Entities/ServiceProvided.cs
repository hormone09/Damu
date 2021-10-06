using FirstTaskEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Models
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
		/// Дата начала оказания услуги компанией
		/// </summary>
		public DateTime DateOfBegin { get; set; }

		/// <summary>
		/// Дата окончания оказания услуги компанией
		/// </summary>
		public DateTime DateOfFinish { get; set; }

		/// <summary>
		/// Статус активности услуги
		/// </summary>
		public Statuses Status { get; set; }
	}
}
