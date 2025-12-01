using Microsoft.AspNetCore.Mvc;

namespace app.Services.Common.attrs
{
	public class IsMoreFilterAttribute : Attribute, ICompare
	{
		readonly string _column;
		public IsMoreFilterAttribute(string column) {
			_column = column;
		}
		public bool Compare(object instance, object comparedValue)
		{
			try
			{
				var type = instance.GetType();
				var prop = type.GetProperty(_column);
				var value = prop.GetValue(instance);
				if (value == null)
				{
					return (string)comparedValue == "null" || comparedValue == null;
				}
				double first;
				double second;
				double.TryParse($"{value}", out first);
				double.TryParse($"{comparedValue}".Replace(".", ","), out second);
				return (double)first == (double)second;
				//return (double)first >= (double)second;
			}
			catch { 
				return false;
			}
		}
	}
}
