using app.Db.utils;
using app.Services.Component.component;
using app.Services.Component.Models;

namespace app.Services.Component
{
	public interface IComponentService
	{
		//public Task<List<IComponentModel>> GetComponentsAsObjIEnum((KeyValueObject pairs, List<int> ids) parameters = default);
		//public Task<ComponentAllModel> GetComponentsAsObj((KeyValueObject pairs, List<int> ids) parameters = default);
		//public Task<List<KeyValueObject>> GetComponentsByEnType(string entype);
		//public Task<Dictionary<string, string>> GetComponentsById(string entype, int id);
		//public Task<ComponentAllDatesModel> GetComponentsDividedByDates((KeyValueObject pairs, List<int> ids) parameters = default);



		public Task<DataSystemView> SelectAll();
		public Dictionary<string, List<ParamProductionModel>> GetParamStat(ComponentList components, string parameter);
		//public  ToDictionary();
		//public ComponentAllModel Filter(ComponentAllModel all, (KeyValueObject pairs, List<int> ids) parameters = default);
		//public ComponentAllModel FilterByParamValue(ComponentAllModel components, KeyValueObject dict);
		//public ComponentAllModel FilterByIds(ComponentAllModel components, List<int> indexes);

		//public List<KeyValueObject> ObjListToDictionary<T>(List<T> list, List<(Type t, bool shoudHave)> exceptionsAttr = null) where T : class;
		//public Dictionary<string, List<ParamProductionModel>> GetParamStat(List<KeyValueObject> components, string parameter, string entype);
		//public ComponentDataModel ComponentDataModel { get; }
		//public Task<Dictionary<string, Dictionary<string, List<KeyValueObject>>>> GetPriorityComponents(List<IComponentModel> components, string entype, Dictionary<string, string> parameters);
	}
}