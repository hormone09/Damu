using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseEditor
{
	public static class StatusEditor
	{
		//Home
		//private static string connectionString = "Data Source=DESKTOP-5MU0QK1; Database=FirstTask; Trusted_Connection=True;";
		//Work
		private static string connectionString = "Data Source=DESKTOP-8LKEMKN; Database=FirstTask; Trusted_Connection=True;";
		private static TimeSpan timeForChange = TimeSpan.Parse("00:00");
		private static DateTime today;

		public static void Start()
		{
			Console.WriteLine($"Время: {DateTime.Now.TimeOfDay} Начало обновления: {timeForChange}");
			while (true)
			{
				if(DateTime.Now.TimeOfDay.Hours == timeForChange.Hours && DateTime.Now.TimeOfDay.Minutes == timeForChange.Minutes)
				{
					today = DateTime.Now.Date;

					Console.WriteLine("Начало обновления данных...");

					EditServicesStatuses();
					EditCompanyStatuses();
					EditEmployeeStatuses();

					Console.WriteLine("Обновление данных прошло успешно!");

					break;
				}
			}
		}

		private static void  EditServicesStatuses()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				var param = new SqlParameter("@Begin", today);
				var command = connection.CreateCommand();
				command.Parameters.Add(param);

				command.CommandText = "UPDATE Services SET Status = 1 WHERE DateOfBegin <= @Begin AND DateOfFinish is null";

				command.ExecuteNonQuery();

				connection.Close();
			}
		}

		private static void EditCompanyStatuses()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				var param = new SqlParameter("Begin", today);
				var command = connection.CreateCommand();
				command.Parameters.Add(param);

				command.CommandText = "UPDATE Companies SET Status = 1 WHERE DateOfBegin <= @Begin AND DateOfFinish is null";
				command.ExecuteNonQuery();

				connection.Close();
			}
		}

		private static void EditEmployeeStatuses()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				var param = new SqlParameter("Begin", today);
				var command = connection.CreateCommand();
				command.Parameters.Add(param);

				command.CommandText = "UPDATE Employee SET Status = 1 WHERE DateOfBegin <= @Begin AND DateOfFinish is null";
				command.ExecuteNonQuery();

				connection.Close();
			}
		}
	}
}
