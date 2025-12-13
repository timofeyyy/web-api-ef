using app.Db.Uow;
using app.Db.utils;
using app.Services.Common.attrs;
using app.Services.Common.Ref;
using app.Services.Component;
using app.Services.Component.component;
using app.Services.Component.Models;
using NuGet.Packaging.Signing;
using System.Reflection;
using System.Text.Json.Serialization;


namespace app.Services.ComponentService
{
	public class ComponentService : IComponentService
	{
		readonly UnitOfWork1 _uow;
		public ComponentService(UnitOfWork1 uow, ComponentDataModel componentDataModel)
		{
			_uow = uow;
		}

		public async Task<DataSystemView> SelectAll()
		{
			var cTask = Task.Run(async () =>
			{
				return await _uow.CapacitorRepository.SelectAll();
			});
			var dTask = Task.Run(async () =>
			{
				return await _uow.DiodRepository.SelectAll();
			});
			var mTask = Task.Run(async () =>
			{
				return await _uow.MicrochipRepository.SelectAll();
			});
			var rTask = Task.Run(async () =>
			{
				return await _uow.ResistorRepository.SelectAll();
			});
			var tTask = Task.Run(async () =>
			{
				return await _uow.TransistorRepository.SelectAll();
			});
			await Task.WhenAll(cTask, dTask, mTask, rTask, tTask);
			return new ComponentAllModel()
			{
				Capacitor = cTask.Result,
				Diod = dTask.Result,
				Microchip = mTask.Result,
				Resistor = rTask.Result,
				Transistor = tTask.Result
			}.ToDictionary();
		}



		public Dictionary<string, List<ParamProductionModel>> GetParamStat(ComponentList components, string parameter)
		{
			int componentTypeTotal = components.Count;	
			Dictionary<string, List<ParamProductionModel>> dict = new Dictionary<string, List<ParamProductionModel>>();
			if (components.Count == 0) {
				return dict;
			}
			dict[(string)components[0]["EnComponentType"]] = new List<ParamProductionModel>();
			
			foreach (var item in components)
			{
				var props = item.Keys;
				var prop = props.Where(prop => prop.ToLower() == parameter.ToLower()).First();
				var entype = (string)item["EnComponentType"];
				var val = item[prop];

				if ($"{val}" == "-1,7976931348623157E+308" || $"{val}" == "")
				{
					continue;
				}
				var list = dict[entype].Where(mp =>
				{
					if ($"{val}" == mp.ParamValue)
					{
						mp.Amount += 1;
						mp.Weight = (100 * mp.Amount) / double.Parse(componentTypeTotal.ToString());
					}
					return $"{val}" == mp.ParamValue;
				}).ToList();

				if (list.Count == 0)
				{
					ParamProductionModel mp = new ParamProductionModel()
					{
						ParamName = parameter,
						ParamValue = $"{val}",
						Amount = 1,
						Weight = 100 / double.Parse(componentTypeTotal.ToString())
					};
					dict[entype].Add(mp);
				}
			}
			return dict;
		}

		//public async Task<Dictionary<string, Dictionary<string, List<KeyValueObject>>>> GetPriorityComponents(List<KeyValueObject> components, string entype, Dictionary<string, string> parameters)
		//{
		//	Dictionary<string, Dictionary<string, List<KeyValueObject>>> resDict = new();
		//	Dictionary<string, List<KeyValueObject>> descent = new();
		//	var all = components;
		//	foreach (var key in parameters.Keys)
		//	{
		//		List<KeyValueObject> componentsList = new();
		//		//var t = components.GetType();
		//		//var props = t.GetProperties();

		//		//foreach (var prop in props)
		//		//{
		//		//var all = (List<KeyValueObject>)prop.GetValue(components, null);
		//		foreach (var item in all)
		//		{
		//			var attrs = prop.GetCustomAttributes(inherit: true);
		//			var attr = attrs.OfType<ICompare>().FirstOrDefault();
		//			if (attr != null)
		//			{
		//				var res = attr.Compare(item, parameters[key]);
		//				if (res)
		//				{
		//					componentsList.Add(item);
		//				}
		//			}
		//		}
		//		//}

		//		//var dictList = ObjListToDictionary(componentsList);
		//		//var dictList = ObjListToDictionary(componentsList, new() {
		//		//			(t: typeof(JsonIgnoreAttribute), shoudHave: false)
		//		//		});
		//		descent[key] = dictList;
		//		all = componentsList;
		//	}
		//	resDict[entype] = descent;
		//	return resDict;
		//}
		//public List<KeyValueObject> ObjListToDictionary(List<KeyValueObject> list)
		//{
		//	List<KeyValueObject> dictList = new();
		//	string lastType = null;
		//	PropertyInfo[] props = null;
		//	foreach (var item in list)
		//	{
		//		var t = item.GetType();
		//		if (lastType != t.Name)
		//		{
		//			props = _refHelper.GetProps(t, new()
		//			{
		//				(t: typeof(JsonIgnoreAttribute), shoudHave: false)
		//				});
		//		}
		//		var dict = _uow.ObjToDictionary(item, props);
		//		dictList.Add(dict);
		//		lastType = t.Name;
		//	}
		//	return dictList;
		//}

		//public ComponentAllModel FilterByParamValue(ComponentAllModel components, KeyValueObject dict)
		//{
		//	//var t = components.GetType();
		//	//var props = t.GetProperties();
		//	//foreach (var prop in props)
		//	//{
		//	//	List<KeyValueObject> all = (List<KeyValueObject>)prop.GetValue(components, null);	
		//	//	foreach (var key in dict.Keys)
		//	//	{
		//	//		if (dict[key] != null)
		//	//		{
		//	//			all = all.Where(item => item[key] == dict[key]).ToList();
		//	//		}
		//	//	}
		//	//}
		//	return components;
		//}
		//public ComponentAllModel FilterByIds(ComponentAllModel components, List<int> indexes) {
		//	var t = components.GetType();
		//	var props = t.GetProperties();
		//	foreach (var prop in props)
		//	{
		//		ComponentList all = (ComponentList)prop.GetValue(components, null);
		//		all.Where(c => indexes.Contains((int)c["ID"])).ToList();
		//	}
		//	return components;
		//} 

		//public async Task<ComponentAllDatesModel> GetComponentsDividedByDates()
		//{
		//	var components = await GetComponentsAsObj(parameters);
		//	var t = components.GetType();
		//	var props = t.GetProperties();
		//	foreach (var prop in props)
		//	{
		//		var list = prop.GetValue(components, null);
		//		//Console.WriteLine(list.GetType().Name);
		//	}
		//	return new();
		//}
	}
}
