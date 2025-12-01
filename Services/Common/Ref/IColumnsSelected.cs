using app.Services.Common.alias;

namespace app.Services.Common.Ref
{
	public interface IColumnsSelected
	{
		public List<AliasModel> GetChartColumns(Dictionary<string, string> alias, Type t);
		public List<AliasModel> GetAllMapedColumns(Dictionary<string, string> alias, Type t);
		public List<AliasModel> GetAllMapedCustomColumns(Dictionary<string, string> alias, Type t);
	}
}
