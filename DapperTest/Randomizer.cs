using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTest
{
	public static class Randomizer
	{
		private static Random random = new Random();

		private static string[] firstNames = {
			"Владимир", "Антон", "Иван", "Бекзат", "Айдос", "Турсун", "Даниил", "Андрей", "Виталий", "Майкл", "Сакен", "Берик", "Серик", "Вячеслав", "Айдар", "Руслан", "Максим", "Юрий"
		};

		private static string[] secondNames = { 
			"Петров", "Иванов", "Мухаметжанов", "Сапарбаев", "Коробов", "Власов", "Лошак", "Дудь", "Путин", "Айтбай", "Мороз", "Ступаченко", "Вдовиченков", "Малахов"
		};

		private static string[] companyTypes = { "TOO", "ИП", "ОАО", "ООО" };

		private static string[] companyNames = { 
			"МеталПром", "Нефть и Газ", "Газ и Нефть", "Масло", "Тасол", "Расол", "Сулпак", "Технодом", "Apple", "XIOMI", "Poco", "Яндекс", "Google", "Space X", "Tesla", "The Facebook" 
		};

		private static string[] serviceNames = { 
			"Инъекция шприцем в мышцу", "Инъекция шприцем в вену", "Обработка первично заживающих ран", "Обработка первично заживающей большой раны (без наложения швов)",
			"Сустав кисти, коленный сустав или сустав стопы", "Промывание мочевого пузыря при установленном постоянном катетере", "Установка в/в капельницы и наблюдение за пациентом (без цены медикаментов)",
			"Визит медсестры/помощника врача (включено: перевязка, в/м, в/в (без цены медикаментов))", "Обработка трофических язв", "Определение уровня сахара в крови"
		};

		private static char[] alfavit = { 'z', 'f', 'x', 'y', 'h', 'g', 'm', 'n', 'p', 'o' };

		public static int CompanisCount { get { return companyNames.Length; } }
		public static int ServicesCount { get { return serviceNames.Length; } }


		public static string GetFullName()
		{
			string firstName = firstNames[random.Next(0, (firstNames.Length - 1))];
			string secondName = secondNames[random.Next(0, (secondNames.Length - 1))];

			return $"{firstName}  {secondName}";
		}

		public static string GetCompanyName(int number)
		{
			string companyType = companyTypes[random.Next(0, (companyTypes.Length - 1))];
			string companyName = companyNames[number];

			return $"{companyType}  {companyName}";
		}

		public static string GetPersonalNumber()
		{
			return $"{random.Next(111111, 999999)}{random.Next(111111, 999999)}";
		}

		public static string GetServiceName(int number)
		{
			return serviceNames[number];
		}

		public static string GetServiceCode()
		{
			char symbol = alfavit[random.Next(0, (alfavit.Length - 1))];
			string result = Convert.ToString(symbol);

			for (int i = 0; i < 8; i++)
			{
				result += random.Next(0, 9);

				if (i == 1 || i == 4)
					result += ".";
			}

			return result;
		}

		public static decimal GetPrice()
		{
			return random.Next(250, 99999);
		}

		public static string GetPhone()
		{
			string result = "8707";

			for (int i = 0; i < 7; i++)
				result += random.Next(0, 9);

			return result;
		}

		public static DateTime GetDate()
		{
			string day = Convert.ToString(random.Next(10, 28));
			string month = Convert.ToString(random.Next(10, 12));
			string year = Convert.ToString(random.Next(2015, 2021));
			string stringResult = $"{day}.{month}.{year}";

			DateTime result = DateTime.Parse(stringResult);

			return result;
		}
	}
}
