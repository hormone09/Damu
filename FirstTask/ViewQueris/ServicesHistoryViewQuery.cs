using System;

namespace FirstTask.ViewQueris
{
	public class ServicesHistoryViewQuery : ViewQueryBase
	{
		/// <summary>
		/// Верхняя временная граница для выгрузки записей
		/// </summary>
		public DateTime DateBegin { get; set; }

		/// <summary>
		/// Нижняя временная граница для выгрузки записей
		/// </summary>
		public DateTime DateEnd { get; set; }
	}
}