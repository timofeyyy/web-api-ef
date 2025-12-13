using Microsoft.AspNetCore.Mvc;

namespace app.Services.Common.attrs
{
	public class IsLessFilterAttribute : Attribute, ICompare
	{
		public bool Compare(object value, object comparedValue)
		{
			try
			{
				//var type = instance.GetType();
				//var prop = type.GetProperty(_column);
				//var value = prop.GetValue(instance);
				//var value = instance[_column.Replace(_column[0], Char.ToLower(_column[0]))];

				if (value == null)
				{
					return (string)comparedValue == "null" || comparedValue == null;
				}
				double first;
				double second;
				double.TryParse($"{value}", out first);
				double.TryParse($"{comparedValue}".Replace(".", ","), out second);
				return (double)first == (double)second;
			}
			catch
			{
				return false;
			}
		}
	}
}
