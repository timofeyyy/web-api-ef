using app.Db.ef;

namespace app.Services.ComponentType
{
	public interface IComponentTypeService
	{
		public Task<List<ComponentTypes>> GetNamesAsObj();
		public Task<bool> IsEnComponentTypeExists(string entype);
		public Task<ComponentTypes> GetByEn(string entype);
		public Task<ComponentTypes> GetByRu(string rutype);
	}
}


//public async Task<bool> IsEnComponentTypeExists(string entype)
//{
//	var ctList = await _rdm.GetComponentTypes();
//	return ctList.Exists(el => el.EnComponentType.ToLower() == entype.ToLower());
//}
//public async Task<bool> IsParameterExists(string entype, string parameter)
//{
//	var amList = await _rdm.GetAllMapedColumns();
//	return amList[entype].Exists(el => el.EnVal.ToLower() == parameter.ToLower());
//}