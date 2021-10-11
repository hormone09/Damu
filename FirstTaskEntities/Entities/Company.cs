using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstTaskEntities.Models
{
	public class Company
	{
		public int Id { get; set; }

		/// <summary>
		/// Название компании
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// БИН
		/// </summary>
		[MaxLength(12)]
		[MinLength(12)]
		public string BIN { get; set; }

		/// <summary>
		/// Номер телефона
		/// </summary>
		// TODO: MASK
		public string Phone { get; set; }

		/// <summary>
		/// Дата начала сотрудничества
		/// </summary>
		public DateTime DateOfBegin { get; set; }
		/// <summary>
		/// Дата удаления
		/// </summary>
		public DateTime DateOfFinish { get; set; }

		public Statuses Status { get; set; }

		[NotMapped]
		public int TotalRows { get; set; }
	}
}