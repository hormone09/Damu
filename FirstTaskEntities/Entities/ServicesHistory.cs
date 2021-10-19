using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Models
{
	public class ServicesHistory
	{
		public int Id { get; set; }

		/// <summary>
		/// Услуга, которая была оказанна
		/// </summary>
		public int ServiceId { get; set; }

		/// <summary>
		/// Компания, оказавшая услугу
		/// </summary>
		public int CompanyId { get; set; }

		/// <summary>
		/// Сотрудник, оказавший услугу
		/// </summary>
		public int EmployeeId { get; set; }

		/// <summary>
		/// Дата оказания услуги
		/// </summary>
		public DateTime DateOfCreate { get; set; }

		/// <summary>
		/// Дата удаления записи
		/// </summary>
		public DateTime? DateOfDelete { get; set; }
		/// <summary>
		/// Статус записи в БД
		/// </summary>
		public Statuses Status { get; set; }
	}
}
