using app.Services.Common.alias;
using app.Services.Common.Ref.Models;
using System.Reflection;

namespace app.Services.Common.Ref
{
	public class RefHelper : IRefHelper
	{
		readonly RefDataModel _refDataModel;

		public RefHelper(RefDataModel refDataModel) {
			_refDataModel = refDataModel;
		}

		public async Task<List<AliasModel>> GetAllMapedColumns(string entype)
		{
			Dictionary<string, List<AliasModel>> tasks = await _refDataModel.GetAllMapedColumns();
			if (tasks.ContainsKey(entype))
			{
				return tasks[entype];
			}
			return null;
		}

		public Task<List<AliasModel>> GetAllMapedCustomColumns(string entype)
		{
			throw new NotImplementedException();
		}

		public async Task<List<AliasModel>> GetChartColumns(string entype)
		{
			Dictionary<string, List<AliasModel>> tasks = await _refDataModel.GetChartColumns();
			if (tasks.ContainsKey(entype))
			{
				return tasks[entype];
			}
			return null;
		}

		//public async Task<bool> IsEnComponentTypeExists(string entype)
		//{
		//	var ctList = await _rdm.GetComponentTypes();
		//	return ctList.Exists(el => el.EnComponentType.ToLower() == entype.ToLower());
		//}
		public async Task<bool> IsParameterExists(string entype, string parameter)
		{
			var amList = await _refDataModel.GetAllMapedColumns();
			return amList[entype].Exists(el => el.EnVal.ToLower() == parameter.ToLower());
		}

		public PropertyInfo[] GetProps(Type t, List<(Type t, bool shoudHave)> exceptionsAttr = null)
		{
			return _refDataModel.RefPropsSelected.PropsByAttrs(t, exceptionsAttr);
		}

		public async Task<Dictionary<string, List<AliasModel>>> GetAllMapedColumns()
		{
			Dictionary<string, List<AliasModel>> columns = await _refDataModel.GetAllMapedColumns();
		
			return columns;
		}


		//public Task<List<AliasModel>> GetAllMapedColumns(string entype)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<List<AliasModel>> GetAllMapedCustomColumns(string entype)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<List<AliasModel>> GetChartColumns(string entype)
		//{
		//	throw new NotImplementedException();

		//	//Dictionary<string, List<AliasModel>> tasks = await _refDataModel.GetChartColumns();
		//	//if (tasks.ContainsKey(entype))
		//	//{
		//	//	return tasks[entype];
		//	//}
		//	//return null;
		//}
		//public async Task<List<AliasModel>> GetChartColumns(string entype)
		//{
		//Dictionary<string, List<AliasModel>> tasks = await _rdm.GetChartColumns();
		//	if (tasks.ContainsKey(entype))
		//	{
		//		return tasks[entype];
		//	}
		//	return null;
		//}
		//public async Task<List<AliasModel>> GetAllMapedColums(string entype)
		//{
		//	Dictionary<string, List<AliasModel>> tasks = await _rdm.GetAllMapedColumns();
		//	if (tasks.ContainsKey(entype))
		//	{
		//		return tasks[entype];
		//	}
		//	return null;
		//}

	}

	
}
