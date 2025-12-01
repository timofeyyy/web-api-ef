using System.Globalization;

namespace app.Services.Common.attrs
{
	public class IsEqualFilterAttribute : Attribute, ICompare
	{
		readonly string _column;
		public IsEqualFilterAttribute(string column)
		{
			_column = column;
		}
		public bool Compare(object instance, object comparedValue)
		{
			object value = null;
			try
			{
				var type = instance.GetType();
				var prop = type.GetProperty(_column);
				value = prop.GetValue(instance);
				if (value == null || value is String && (string.IsNullOrEmpty((string)value) || (string)value == "null"))
				{
					return string.IsNullOrEmpty((string)comparedValue) || (string)comparedValue == "null";
				}
				if ((string)comparedValue == "null" || value is String && (string.IsNullOrEmpty((string)value) || (string)value == "null"))
				{
					return value is String && (string.IsNullOrEmpty((string)value) || (string)value == "null") || value == null;
				}

				if (value is String)
				{
					return $"{(string)value}".ToLower() == $"{(string)comparedValue}".ToLower();
				}
				double.TryParse($"{value}", NumberStyles.Float, CultureInfo.InvariantCulture, out double first);
				double.TryParse($"{comparedValue}".Replace(".", ","), NumberStyles.Float, CultureInfo.InvariantCulture, out double second);
				return (double)first == (double)second;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
