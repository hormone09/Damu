using FirstTask.Resources;
using FirstTaskEntities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstTask.Models
{
	public class EmployeeModel
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int? Id { get; set; }

		/// <summary>
		/// 12-ти значный ИИН
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[StringLength(12, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FormatBIN")]
		public string PersonalNumber { get; set; }

		/// <summary>
		/// Полное имя 
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[StringLength(300, MinimumLength = 10, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmployeeNameLength")]
		public string FullName { get; set; }

		/// <summary>
		/// Дата рождения
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public DateTime? BirthdayDate { get; set; }

		/// <summary>
		/// Дата начала работы
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public DateTime? DateOfBegin { get; set; }

		/// <summary>
		/// Дата окончания работы - удаление
		/// </summary>
		public DateTime? DateOfFinish { get; set; }

		/// <summary>
		/// Организация, к которой относится сотрудник
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public CompanyModel Company { get; set; }

		/// <summary>
		/// Номер телефона
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public string Phone { get; set; }

		/// <summary>
		/// Статус записи в БД
		/// </summary>
		public Statuses Status { get; set; }
		public int TotalRows { get; set; }
	}
}