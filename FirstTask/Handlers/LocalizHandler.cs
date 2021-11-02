using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace FirstTask.Handlers
{
	public class LocalizHandler
	{
		private readonly ResourceManager rm;
		private readonly CultureInfo culture;
		public LocalizHandler()
		{
			rm = new ResourceManager("FirstTask.Resources.View", Assembly.GetExecutingAssembly());
			culture = Thread.CurrentThread.CurrentCulture;
		}

		public string GetValue(string key)
		{
			return rm.GetString(key, culture);
		}
	}
}