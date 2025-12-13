using System.Globalization;

namespace app.Services.Common.attrs
{
	public class IsEqualFilterAttribute : Attribute, ICompare
	{
		public bool Compare(object value, object comparedValue)
		{
			try
			{
				//var type = value.GetType();
				//Console.WriteLine(type.Name);
				//var type = instance.GetType();
				//var prop = type.GetProperty(_column);
				//value = prop.GetValue(instance);
				//value = instance[_column.Replace(_column[0], Char.ToLower(_column[0]))];
				if (value == null || value is String && (string.IsNullOrEmpty((string)value) || (string)value == "null"))
				{
					//Console.WriteLine($"1 {string.IsNullOrEmpty((string)comparedValue)} {(string)comparedValue == "null"}");
					return string.IsNullOrEmpty((string)comparedValue) || (string)comparedValue == "null";
				}
				if ((string)comparedValue == "null" || value is String && (string.IsNullOrEmpty((string)value) || (string)value == "null"))
				{
					//Console.WriteLine($"2");
					return value is String && (string.IsNullOrEmpty((string)value) || (string)value == "null") || value == null;
				}
				if (value is String)
				{
					//Console.WriteLine($"3 {$"{(string)value}".ToLower() == $"{(string)comparedValue}".ToLower()} {$"{(string)value}".ToLower()} {$"{(string)comparedValue}".ToLower()}");
					return $"{(string)value}".ToLower() == $"{(string)comparedValue}".ToLower();
				}
				double.TryParse($"{value}", NumberStyles.Float, CultureInfo.InvariantCulture, out double first);
				double.TryParse($"{comparedValue}".Replace(".", ","), NumberStyles.Float, CultureInfo.InvariantCulture, out double second);
				return (double)first == (double)second;
			}
			catch (Exception ex)
			{
				//Console.WriteLine(ex.Message);
				return false;
			}
		}
	}
}
