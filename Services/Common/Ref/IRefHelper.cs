using app.Services.Common.alias;
using System.Reflection;

namespace app.Services.Common.Ref
{
	public interface IRefHelper
	{
		public Task<List<AliasModel>> GetChartColumns(string entype);
		public Task<List<AliasModel>> GetAllMapedColumns(string entype);
		public Task<Dictionary<string, List<AliasModel>>> GetAllMapedColumns();
		public Task<List<AliasModel>> GetAllMapedCustomColumns(string entype);
		public Task<bool> IsParameterExists(string entype, string parameter);
		public PropertyInfo[] GetProps(Type t, List<(Type t, bool shoudHave)> exceptionsAttr = null);
	}
}
