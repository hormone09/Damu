using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public class Employee
	{
		public int Id { get; set; }

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
		public DateTime? DateOfFinish { get; set; }

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
		public int Status { get; set; }

		[NotMapped]
		public int TotalRows { get; set; }

		[NotMapped]
		public Company Company { get; set; }
	}
}