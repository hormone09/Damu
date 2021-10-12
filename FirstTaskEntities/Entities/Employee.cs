using FirstTaskEntities.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstTaskEntities.Models
{
	public class Employee
	{
		public int Id { get; }

		/// <summary>
		/// ИИН сотрудника
		/// </summary>
		public string PersonalNumber { get; set; }

		/// <summary>
		/// Полное имя
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// Дата рождения
		/// </summary>
		public DateTime BirthdayDate { get; set; }

		/// <summary>
		/// Дата начала работы
		/// </summary>
		public DateTime DateOfBegin { get; set; }

		/// <summary>
		/// Дата удаления сотрудника
		/// </summary>
		public DateTime DateOfFinish { get; set; }

		/// <summary>
		/// Ссылка для определения пренадлежности к компании
		/// </summary>
		public int CompanyId { get; set; }

		/// <summary>
		/// Номер телефона
		/// </summary>
		// TODO: MASK
		public string Phone { get; set; }

		/// <summary>
		/// Статус активности
		/// </summary>
		public Statuses Status { get; set; }

		[NotMapped]
		public int TotalRows { get; set; }
	}
}