using app.Services.Common.alias;
using app.Services.Common.attrs;
using System.Reflection;
using System.Text.Json.Serialization;

namespace app.Services.Common.Ref
{
	public class RefPropsSelected : RefBase, IColumnsSelected
	{
		public List<AliasModel> GetAllMapedColumns(Dictionary<string, string> alias, Type t)
		{
			var props = PropsByAttrs(t, new List<(Type t, bool shoudHave)>() { (typeof(JsonIgnoreAttribute), false) });
			List<AliasModel> columns = GetColumns(alias, props);
			return columns;
		}

		public List<AliasModel> GetAllMapedCustomColumns(Dictionary<string, string> alias, Type t)
		{
			var props = PropsByAttrs(t, new List<(Type t, bool shoudHave)>() { (typeof(CustomMappedColumnAttribute), true) });
			List<AliasModel> columns = GetColumns(alias, props);
			return columns;
		}

		public List<AliasModel> GetChartColumns(Dictionary<string, string> alias, Type t)
		{
			var props = PropsByAttrs(t, new List<(Type t, bool shoudHave)>() { (typeof(ChartUsageAttribute), true) });
			List<AliasModel> columns = GetColumns(alias, props);
			return columns;
		}

		public List<AliasModel> GetColumns(Dictionary<string, string> alias, PropertyInfo[] props)
		{
			List<AliasModel> columns = new();
			foreach (var prop in props)
			{
				foreach (var key in alias.Keys)
				{
					if (key.ToLower() == prop.Name.ToLower())
					{
						columns.Add(new() { EnVal = key, RuVal = alias[key] });
						break;
					}
				}
			}
			return columns;
		}
	}
}
