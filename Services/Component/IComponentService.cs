using app.Db.utils;
using app.Services.Component.component;
using app.Services.Component.Models;

namespace app.Services.Component
{
	public interface IComponentService
	{
		public Task<List<IComponentModel>> GetComponentsAsObjIEnum((Dictionary<string, object> pairs, List<int> ids) parameters = default);
		public Task<ComponentAllModel> GetComponentsAsObj((Dictionary<string, object> pairs, List<int> ids) parameters = default);
		public Task<List<IComponentModel>> GetComponentsByEnType(string entype);
		//public Task<Dictionary<string, string>> GetComponentsById(string entype, int id);
		public List<Dictionary<string, object>> ObjListToDictionary<T>(List<T> list, List<(Type t, bool shoudHave)> exceptionsAttr = null) where T : class;
		public Dictionary<string, List<ParamProductionModel>> GetParamStat(List<IComponentModel> components, string parameter, string entype);
		public ComponentDataModel ComponentDataModel { get; }
		public Task<Dictionary<string, Dictionary<string, List<Dictionary<string, object>>>>> GetPriorityComponents(List<IComponentModel> components, string entype, Dictionary<string, string> parameters);
	}
}