using Entities.Enums;
using System;

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
		public ServiceModel Service { get; set; }

		/// <summary>
		/// Организация, выполнившая услугу
		/// </summary>
		public CompanyModel Company { get; set; }

		/// <summary>
		/// Сотрудник, выполнивший услугу
		/// </summary>
		public EmployeeModel Employee { get; set; }

		/// <summary>
		/// Дата создания записи - дата начала оказания услуг
		/// </summary>
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