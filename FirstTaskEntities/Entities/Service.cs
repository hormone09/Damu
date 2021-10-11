using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstTaskEntities.Models
{
	public class Service
	{
		public int Id { get; set; }

		/// <summary>
		/// Название услуги
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Идентификатор услуги
		/// </summary>
		// TODO: MASK
		public string Code { get; set; }

		/// <summary>
		/// Дата появления 
		/// </summary>
		public DateTime DateOfBegin { get; set; }

		/// <summary>
		/// Дата окончания действия 
		/// </summary>
		public DateTime DateOfFinish { get; set; }
		
		/// <summary>
		/// Стоимость оказания услуги
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// Статус активности 
		/// </summary>
		public Statuses Status { get; set; }

		[NotMapped]
		public int TotalRows { get; set; }
	}
}