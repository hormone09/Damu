using FirstTaskEntities.Enums;

namespace FirstTaskEntities.Query
{
	public abstract class QueryListBase
	{
		/// <summary>
		/// Статус записи в БД
		/// </summary>
		public Statuses Status { get; set; }

		/// <summary>
		/// Ограничение количества выгружаемых записей
		/// </summary>
		public int Limit { get; set; }

		/// <summary>
		/// Количество пропускаемых записей при выгрузке
		/// </summary>
		public int Skip { get; set; }

		/// <summary>
		/// Количество записей в конечном списке
		/// </summary>
		public int RowNumber { get; set; }

		/// <summary>
		/// Тип сортировка конечного списка
		/// </summary>
		public string SortingType { get; set; }
	}
}
