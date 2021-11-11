using Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceRegister.Models
{
	public class ServiceHistoryModel
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Оказанная услуга
		/// </summary>
		[Required]
		public ServiceModel Service { get; set; }

		/// <summary>
		/// Организация, выполнившая услугу
		/// </summary>
		[Required]
		public CompanyModel Company { get; set; }

		/// <summary>
		/// Сотрудник, выполнивший услугу
		/// </summary>
		[Required]
		public EmployeeModel Employee { get; set; }

		/// <summary>
		/// Дата создания записи - дата начала оказания услуг
		/// </summary>
		/// 
		[Required]
		public DateTime? DateOfCreate { get; set; }

		/// <summary>
		/// Дата окончания выполнения услуги
		/// </summary>
		public DateTime? DateOfFinish { get; set; }

		/// <summary>
		/// Дата удаления записи из истории оказания услуг
		/// </summary>
		public DateTime? DateOfDelete { get; set; }

		/// <summary>
		/// Статус записи в БД
		/// </summary>
		public Statuses Status { get; set; }
	}
}