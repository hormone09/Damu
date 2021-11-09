using Entities.Enums;

namespace ServiceRegister.ViewQueris
{
	public class ViewQueryBase
	{
		/// <summary>
		/// Номер страницы клиента
		/// </summary>
		public int? Page { get; set; }

		/// <summary>
		/// Размер страницы клиента
		/// </summary>
		public int? PageSize { get; set; }

		/// <summary>
		/// Количество записей, отвечающих заданным фильтрам
		/// </summary>
		public int TotalRows { get; set; }

		/// <summary>
		/// Статус искомых записей в БД
		/// </summary>
		public Statuses Status { get; set; }

		/// <summary>
		/// Тип сортировки конечного списка
		/// </summary>
		public string SortingType { get; set; }
	}
}