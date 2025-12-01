using app.Db.Uow;
using app.Db.utils;
using app.Services.Common.attrs;
using app.Services.Common.Ref;
using app.Services.Component;
using app.Services.Component.component;
using app.Services.Component.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace app.Services.ComponentService
{
	public class ComponentService : IComponentService
	{
		readonly UnitOfWork1 _uow;
		readonly ComponentDataModel _componentDataModel;
		readonly IRefHelper _refHelper;

		public ComponentDataModel ComponentDataModel { get { return _componentDataModel; } }
		public ComponentService(UnitOfWork1 uow, ComponentDataModel componentDataModel, IRefHelper refHelper)
		{
			_uow = uow;
			_componentDataModel = componentDataModel;
			_refHelper = refHelper;
		}


		public async Task<List<IComponentModel>> GetComponentsAsObjIEnum((Dictionary<string, object> pairs, List<int> ids) parameters = default)
		{
			var cTask = Task.Run(async () =>
			{
				return await _uow.CapacitorRepository.SelectAsObj(parameters);
			});
			var dTask = Task.Run(async () =>
			{
				return await _uow.DiodRepository.SelectAsObj(parameters);
			});
			var mTask = Task.Run(async () =>
			{
				return await _uow.MicrochipRepository.SelectAsObj(parameters);
			});
			var rTask = Task.Run(async () =>
			{
				return await _uow.ResistorRepository.SelectAsObj(parameters);
			});
			var tTask = Task.Run(async () =>
			{
				return await _uow.TransistorRepository.SelectAsObj(parameters);
			});
			await Task.WhenAll(cTask, dTask, mTask, rTask, tTask);
			List<IComponentModel> components = new List<IComponentModel>();
			components.AddRange(cTask.Result);
			components.AddRange(dTask.Result);
			components.AddRange(mTask.Result);
			components.AddRange(rTask.Result);
			components.AddRange(tTask.Result);
			return components;
		}
		public async Task<ComponentAllModel> GetComponentsAsObj((Dictionary<string, object> pairs, List<int> ids) parameters)
		{
			var cTask = Task.Run(async () =>
			{
				return await _uow.CapacitorRepository.SelectAsObj(parameters);
			});
			var dTask = Task.Run(async () =>
			{
				return await _uow.DiodRepository.SelectAsObj(parameters);
			});
			var mTask = Task.Run(async () =>
			{
				return await _uow.MicrochipRepository.SelectAsObj(parameters);
			});
			var rTask = Task.Run(async () =>
			{
				return await _uow.ResistorRepository.SelectAsObj(parameters);
			});
			var tTask = Task.Run(async () =>
			{
				return await _uow.TransistorRepository.SelectAsObj(parameters);
			});
			await Task.WhenAll(cTask, dTask, mTask, rTask, tTask);
			return new ComponentAllModel
			{
				capacitor = cTask.Result,
				diod = dTask.Result,
				microchip = mTask.Result,
				resistor = rTask.Result,
				transistor = tTask.Result
			};
		}

		public async Task<List<IComponentModel>> GetComponentsByEnType(string entype)
		{
			Dictionary<string, Func<Task<List<IComponentModel>>>> tasks = _componentDataModel.GetComponentDictionary();
			if (tasks.ContainsKey(entype))
			{
				return await tasks[entype]();
			}
			return null;
		}


		public Dictionary<string, List<ParamProductionModel>> GetParamStat(List<IComponentModel> components, string parameter, string entype)
		{
			Dictionary<string, Func<int>> totals = _componentDataModel.GetTotalsDictionary();

			entype = entype.ToLower();
			if (!totals.ContainsKey(entype))
			{
				return null;
			}
			int componentTypeTotal = totals[entype]();

			Dictionary<string, List<ParamProductionModel>> dict = new Dictionary<string, List<ParamProductionModel>>();
			foreach (var item in components)
			{
				if (!dict.ContainsKey(entype))
				{
					dict[entype] = new List<ParamProductionModel>();
				}

				var t = item.GetType();
				var props = t.GetProperties();
				var prop = props.Where(prop => prop.Name.ToLower() == parameter.ToLower()).First();
				var val = prop.GetValue(item);
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

		public async Task<Dictionary<string, Dictionary<string, List<Dictionary<string, object>>>>> GetPriorityComponents(List<IComponentModel> components, string entype, Dictionary<string, string> parameters)
		{
			Dictionary<string, Dictionary<string, List<Dictionary<string, object>>>> resDict = new();
			Dictionary<string, List<Dictionary<string, object>>> descent = new();
			var all = components;
			foreach (var key in parameters.Keys)
			{
				List<IComponentModel> componentsList = new();
				PropertyInfo? prop = null;
				foreach (var component in all)
				{
					var t = component.GetType();
					if (prop == null)
					{
						var props = t.GetProperties();
						prop = props.Where(p => p.Name.ToLower() == key.ToLower()).First();
					}
					var attrs = prop.GetCustomAttributes(inherit: true);
					var attr = attrs.OfType<ICompare>().FirstOrDefault();
					if (attr != null)
					{
						var res = attr.Compare(component, parameters[key]);
						if (res)
						{
							componentsList.Add(component);
						}
					}
				}

				var dictList = ObjListToDictionary(componentsList, new() {
							(t: typeof(JsonIgnoreAttribute), shoudHave: false)
						});
				descent[key] = dictList;
				all = componentsList;
			}
			resDict[entype] = descent;
			return resDict;
		}
		public List<Dictionary<string, object>> ObjListToDictionary<T>(List<T> list, List<(Type t, bool shoudHave)> exceptionsAttr = null) where T : class
		{
			List<Dictionary<string, object>> dictList = new();
			string lastType = null;
			PropertyInfo[] props = null;
			foreach (var item in list)
			{
				var t = item.GetType();
				if (lastType != t.Name)
				{
					props = _refHelper.GetProps(t, new()
					{
						(t: typeof(JsonIgnoreAttribute), shoudHave: false)
						});
					}
				var dict = _uow.ObjToDictionary(item, props);
				dictList.Add(dict);
				lastType = t.Name;
			}
			return dictList;
		}
	}
}
