using FirstTask.Resources;
using FirstTaskEntities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstTask.Models
{
	public class CompanyModel
	{	
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// Название
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[StringLength(300, MinimumLength = 6, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "CompnayNameLength")]
		public string Name { get; set; }

		/// <summary>
		/// 12-ти значный БИН
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		[StringLength(12, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FormatBIN")]
		public string BIN { get; set; }

		/// <summary>
		/// Номер телефона
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public string Phone { get; set; }

		/// <summary>
		/// Дата начала сотрудничества
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IsRequired")]
		public DateTime? DateOfBegin { get; set; }

		/// <summary>
		/// Дата окончания сотрудничества - удаления
		/// </summary>
		public DateTime? DateOfFinish { get; set; }
		
		/// <summary>
		/// Статус в БД
		/// </summary>
		public Statuses Status { get; set; }

		public int TotalRows { get; set; }
	}
}