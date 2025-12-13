using app.Db.ef;
using app.Services.Common.alias;
using System.Text.Json;

namespace app.Services.Common.Ref.Models
{
	public class RefDataModel
	{
		readonly RefPropsSelected _refPropsSelected;
		public RefPropsSelected RefPropsSelected { get { return _refPropsSelected; } }
		public RefDataModel(RefPropsSelected refPropsSelected) {
			_refPropsSelected = refPropsSelected;
		}

		Dictionary<string, List<AliasModel>> _chartColumns;
		public async Task<Dictionary<string, List<AliasModel>>> GetChartColumns()
		{
			if (_chartColumns == null)
			{
				_chartColumns = await GetAliasDictionary(_refPropsSelected.GetChartColumns);
			}
			Console.WriteLine($"_chartColumns {_chartColumns.Count}");
			return _chartColumns;
		}

		Dictionary<string, List<AliasModel>> _allMapedColumns;
		public async Task<Dictionary<string, List<AliasModel>>> GetAllMapedColumns()
		{
			if (_allMapedColumns == null)
			{
				_allMapedColumns = await GetAliasDictionary(_refPropsSelected.GetAllMapedColumns);
			}
			return _allMapedColumns;
		}

		private async Task<Dictionary<string, List<AliasModel>>> GetAliasDictionary(Func<Dictionary<string, string>, Type, List<AliasModel>> func)
		{
			var alias = await GetAlias();
			Dictionary<string, List<AliasModel>> res = new();
			res["Microchip"] = func(alias, typeof(Microchips));
			res["Capacitor"] = func(alias, typeof(Capacitors));
			res["Diod"] = func(alias, typeof(Diods));
			res["Transistor"] = func(alias, typeof(Transistors));
			res["Resistor"] = func(alias, typeof(Resistors));
			return res;
		}

		public async Task<Dictionary<string, string>> GetAlias()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "alias.json");
			if (!File.Exists(path))
				return null;

			string json = await File.ReadAllTextAsync(path);
			var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
			return dict;
		}
	}
}
