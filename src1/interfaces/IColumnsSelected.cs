using app.Models.other.alias;

namespace app.src1.interfaces
{
	public interface IColumnsSelected
	{
		public List<AliasModel> GetChartColumns(Dictionary<string, string> alias, Type t);
		public List<AliasModel> GetAllMapedColumns(Dictionary<string, string> alias, Type t);
		public List<AliasModel> GetAllMapedCustomColumns(Dictionary<string, string> alias, Type t);
	}
}
