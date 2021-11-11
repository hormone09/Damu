using System.Reflection;
using System;

namespace ServiceRegister.Handlers
{
	public static class SortingTypeHandler
	{
		public static bool Ckeck(Type type, string sortignType)
		{
			PropertyInfo[] properties = type.GetProperties();

			bool IsHasClientSortingType = false;

			foreach (PropertyInfo property in properties)
				if (property.Name.Equals(sortignType) || sortignType.Equals(property.Name + " DESC"))
					IsHasClientSortingType = true;

			return IsHasClientSortingType;
		}
	}
}