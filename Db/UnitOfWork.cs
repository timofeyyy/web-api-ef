using app.Context;
using app.Db.Rep;
using app.Entities;
using app.Models.Ef;
using app.Models.other.alias;
using app.Models.other.component;
using app.Models.other.production;
using app.Models.other.repository;
using app.src1.attrs;
using app.src1.interfaces;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Collections;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace app.db
{
	public class UnitOfWork: IColumnsSelected
	{
		readonly CapacitorRepository _cr;
		readonly DiodRepository _dr;
		readonly MicrochipsRepository _mr;
		readonly ResistorRepository _rr;
		readonly TransistorRepository _tr;
		readonly ComponentTypeRepository _ctr;
		readonly ManufacturerRepository _mfr;
		readonly ForeignnessRepository _fr;

		//public CapacitorRepository Cr { get => _cr; }
		//public DiodRepository Dr { get => _dr; }
		//public MicrochipsRepository Mr { get => _mr; }
		//public ResistorRepository Rr { get => _rr; }
		//public TransistorRepository Tr { get => _tr; }

		readonly RepositoryDataModel _rdm;

		public UnitOfWork(IDbContextFactory<DataBase> factory)
		{
			_cr = new CapacitorRepository(factory.CreateDbContext());
			_dr = new DiodRepository(factory.CreateDbContext());
			_mr = new MicrochipsRepository(factory.CreateDbContext());
			_rr = new ResistorRepository(factory.CreateDbContext());
			_tr = new TransistorRepository(factory.CreateDbContext());
			_ctr = new ComponentTypeRepository(factory.CreateDbContext());
			_rdm = new RepositoryDataModel(this);
			_mfr = new ManufacturerRepository(factory.CreateDbContext());
			_fr = new ForeignnessRepository(factory.CreateDbContext());
		}
		
		public async Task<ComponentAllModel> SelectAll(Dictionary<string, string> dict)
		{
			var cTask = Task.Run(async () =>
			{
				return await _cr.Select(dict);
			});

			var dTask = Task.Run(async () =>
			{
				return await _dr.Select(dict);
			});

			var mTask = Task.Run(async () =>
			{
				return await _mr.Select(dict);
				//return new List<Microchips>();
			});

			var rTask = Task.Run(async () =>
			{
				return await _rr.Select(dict);
				//return new List<Resistors>();

			});

			var tTask = Task.Run(async () =>
			{
				return await _tr.Select(dict);
				//return new List<Transistors>();

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
		public int GetCount<T>(IRepositoryBase<T> repository) where T : class, IComponentModel
		{
			return repository.GetCount();
		}

		public ClassifiedManufacturersModel GetForeignManufacturerList(List<IComponentModel> components, List<Manufacturers> manufacturers)
		{
			var manufacturersDict = _mfr.GetManufacturersAsDictionary(manufacturers);
			var mfProduction =  GetManufacturersProductionAsDictionarty(components);
			var cmm = new ClassifiedManufacturersModel() { CIS = new(), OTHER = new()};
			foreach (var name in mfProduction.Keys)
			{
				if(manufacturersDict.ContainsKey(name))
				{
					var manufacturerForeignness = manufacturersDict[name]["foreignness"];
					if(manufacturerForeignness == "native")
					{
						cmm.CIS[name] = mfProduction[name];
					}
					else
					{
						cmm.OTHER[name] = mfProduction[name];
					}
				}
			}
			return cmm;
		}
		public async Task<List<Manufacturers>> GetManufacturers()
		{
			return await _mfr.Select();
		}
		public Dictionary<string, Dictionary<string, int>> GetManufacturersProductionAsDictionarty(List<IComponentModel> components)
		{
			return _mfr.GetManufacturersProductionAsDictionarty(components); ;
		}
		public async Task<List<ComponentTypes>> GetComponentTypes()
		{
			return await _ctr.Select();
		}
		public async Task<List<IComponentModel>> GetComponentPreviewByParamValue(Dictionary<string, string> dict = null)
		{
			var cTask = Task.Run(async () =>
			{
				return await _cr.SelectPreviewByParamValue(dict);
			});

			var dTask = Task.Run(async () =>
			{
				return await _dr.SelectPreviewByParamValue(dict);
			});

			var mTask = Task.Run(async () =>
			{
				return await _mr.SelectPreviewByParamValue(dict);
			});

			var rTask = Task.Run(async () =>
			{
				return await _rr.SelectPreviewByParamValue(dict);
			});

			var tTask = Task.Run(async () =>
			{
				return await _tr.SelectPreviewByParamValue(dict);
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

		//public async Task<List<IComponent>> GetComponentsPreviewByListId(string entype, List<int> ids) {
		//	Dictionary<string, Func<Task<List<IComponent>>>> tasks = new();
		//	tasks["microchip"] = async () => await _mr.SelectPreviewByListIds(ids);
		//	tasks["capacitor"] = async () => await _cr.SelectPreviewByListIds(ids);
		//	tasks["diod"] = async () => await _dr.SelectPreviewByListIds(ids);
		//	tasks["transistor"] = async () => await _tr.SelectPreviewByListIds(ids);
		//	tasks["resistor"] = async () => await _rr.SelectPreviewByListIds(ids);
		//	if (tasks.ContainsKey(entype))
		//	{
		//		return await tasks[entype]();
		//	}
		//	return null;
		//}

		public async Task<List<IComponentModel>> GetAllComponentsByEnType(string entype)
		{
			Dictionary<string, Func<Task<List<IComponentModel>>>> tasks = new();
			tasks["capacitor"] = async () => {
				object boxing = await _cr.Select();
				var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
				return unboxing;
			};
			tasks["microchip"] = async () => {
				object boxing = await _mr.Select();
				var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
				return unboxing;
			};
			tasks["diod"] = async () => {
				object boxing = await _dr.Select();
				var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
				return unboxing;
			}; ;
			tasks["transistor"] = async () => {
				object boxing = await _tr.Select();
				var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
				return unboxing;
			}; ;
			tasks["resistor"] = async () => {
				object boxing = await _tr.Select();
				var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
				return unboxing;
			}; 
			if (tasks.ContainsKey(entype))
			{
				return await tasks[entype]();
			}
			return null;
		}
		public async Task<List<AliasModel>> GetChartColumns(string entype)
		{
			var alias = await _rdm.GetAlias();
			Dictionary<string, Func<List<AliasModel>>> tasks = new();
			tasks["microchip"] = () => GetChartColumns(alias, typeof(Microchips));
			tasks["capacitor"] = () => GetChartColumns(alias, typeof(Capacitors)); ;
			tasks["diod"] = () => GetChartColumns(alias, typeof(Diods)); ;
			tasks["transistor"] = () => GetChartColumns(alias, typeof(Transistors)); ;
			tasks["resistor"] = () => GetChartColumns(alias, typeof(Resistors)); ;
			if (tasks.ContainsKey(entype)) {
				return tasks[entype]();
			}
			return null;
		}
		public async Task<List<AliasModel>> GetAllMapedColumfGetParamStatisticns(string entype)
		{
			var alias = await _rdm.GetAlias();
			Dictionary<string, Func<List<AliasModel>>> tasks = new();
			tasks["microchip"] = () => GetAllMapedColumns(alias, typeof(Microchips));
			tasks["capacitor"] = () => GetAllMapedColumns(alias, typeof(Capacitors));
			tasks["diod"] = () => GetAllMapedColumns(alias, typeof(Diods));
			tasks["transistor"] = () => GetAllMapedColumns(alias, typeof(Transistors));
			tasks["resistor"] = () => GetAllMapedColumns(alias, typeof(Resistors));
			if (tasks.ContainsKey(entype))
			{
				return tasks[entype]();
			}
			return null;
		}
		public List<AliasModel> GetAllMapedColumns(Dictionary<string, string> alias, Type t)
		{
			List<AliasModel> columns = new();
			var props = t.GetProperties();
			foreach (var prop in props)
			{
				var hasJsonIgnoreAttr = Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute));
				if (!hasJsonIgnoreAttr)
				{
					string key = null;
					string val = null;
					foreach (var item in alias.Keys)
					{
						if (item.ToLower() == prop.Name.ToLower())
						{
							val = alias[item];
							key = item;
							break;
						}
					}
					columns.Add(new() { EnVal = key, RuVal = val });
				}			
			}
			return columns;
		}
		public List<AliasModel> GetChartColumns(Dictionary<string, string> alias, Type t)
		{
			List<AliasModel> columns = new();
			var props = t.GetProperties();
			foreach (var prop in props)
			{
				var hasChartUsageAttr = Attribute.IsDefined(prop, typeof(ChartUsageAttribute));
				if (hasChartUsageAttr)
				{
					string key = null;
					string val = null;
					foreach (var item in alias.Keys)
					{
						if (item.ToLower() == prop.Name.ToLower())
						{
							val = alias[item];
							key = item;
							break;
						}
					}
					columns.Add(new() { EnVal = key, RuVal = val });
				}
			}
			return columns;
		}
		public async Task<List<IComponentModel>> ComponentListByEnType(string entype, List<int> ids)
		{
			Dictionary<string, Func<Task<List<IComponentModel>>>> dict = new();
			dict["microchip"] = async () =>
			{
				var result = await _mr.SelectByListIds(ids);
				return result.Cast<IComponentModel>().ToList(); 
			};
			dict["capacitor"] = async () =>
			{
				var result = await _cr.SelectByListIds(ids);
				return result.Cast<IComponentModel>().ToList();
			};
			dict["diod"] = async () =>
			{
				var result = await _dr.SelectByListIds(ids);
				return result.Cast<IComponentModel>().ToList();
			};
			dict["resistor"] = async () =>
			{
				var result = await _rr.SelectByListIds(ids);
				return result.Cast<IComponentModel>().ToList();
			};
			dict["transistor"] = async () =>
			{
				var result = await _tr.SelectByListIds(ids);
				return result.Cast<IComponentModel>().ToList();
			};

			if (dict.ContainsKey(entype))
			{
				return await dict[entype]();
			}
			return null;
		}
		public List<Dictionary<string, object>> ComponentsToDictionary(List<IComponentModel> components)
		{
			List<Dictionary<string, object>> keyValuePairs = new();
			foreach (var component in components)
			{
				var t = component.GetType();
				var props = t.GetProperties();
				Dictionary<string, object> dict = new();
				foreach (var prop in props)
				{
					dict[prop.Name] = prop.GetValue(component);
				}
				keyValuePairs.Add(dict);
			}
			return keyValuePairs;
		}
		public Dictionary<string, List<ManufacturerProductionModel>> GetManufacturerStatistic(List<IComponentModel> components)
		{
			Dictionary<string, int> totals = new();
			totals.Add("microchip", GetCount(_mr));
			totals.Add("resistor", GetCount(_rr));
			totals.Add("transistor", GetCount(_tr));
			totals.Add("diod", GetCount(_dr));
			totals.Add("capacitor", GetCount(_cr));

			Dictionary<string, List<ManufacturerProductionModel>> dict = new Dictionary<string, List<ManufacturerProductionModel>>();

			foreach (var item in components)
			{
				if (!dict.ContainsKey(item.Type.EnComponentType))
				{
					dict[item.Type.EnComponentType] = new List<ManufacturerProductionModel>();
				}

				var list = dict[item.Type.EnComponentType].Where(mp => mp.ManufacturerName == item.Manufacturer.ManufacturerName).ToList();
				int componentTypeTotal = totals[item.Type.EnComponentType.ToLower()];

				if (list.Count == 0)
				{
					ManufacturerProductionModel mp = new ManufacturerProductionModel()
					{
						ManufacturerName = item.Manufacturer.ManufacturerName,
						Amount = 1,
						Weight = 100 / double.Parse(componentTypeTotal.ToString())
					};
					dict[item.Type.EnComponentType].Add(mp);
				}
				else
				{
					foreach (var existedMP in dict[item.Type.EnComponentType].Where(mp => mp.ManufacturerName == item.Manufacturer.ManufacturerName))
					{
						existedMP.Amount += 1;
						existedMP.Weight = (100 * existedMP.Amount) / double.Parse(componentTypeTotal.ToString());
						break;
					}
				}
			}
			return dict;
		}
		public Dictionary<string, List<ParamProductionModel>> GetParamStatistic(List<IComponentModel> components, string parameter, string entype) {
			Dictionary<string, Func<int>> totals = new();
			totals.Add("microchip", () => GetCount(_mr));
			totals.Add("resistor", () => GetCount(_rr));
			totals.Add("transistor", () => GetCount(_tr));
			totals.Add("diod", () => GetCount(_dr));
			totals.Add("capacitor", () => GetCount(_cr));
			entype = entype.ToLower();
			if (!totals.ContainsKey(entype))
			{
				return null;
			}
			int componentTypeTotal = totals[entype]();

			Dictionary<string, List<ParamProductionModel>> dict = new Dictionary<string, List<ParamProductionModel>>();
			foreach (var item in components)
			{
				if(!dict.ContainsKey(entype))
				{
					dict[entype] = new List<ParamProductionModel>();
				}
				var t = item.GetType();
				var props = t.GetProperties();
				var prop = props.Where(prop => prop.Name.ToLower() == parameter.ToLower()).First();
				var val = prop.GetValue(item);
				var list = dict[entype].Where(mp => {
					if($"{val}" == mp.ParamValue)
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
		public bool IsEnComponentTypeExists(List<ComponentTypes> ctList, string entype) => ctList.Exists(el => el.EnComponentType.ToLower() == entype.ToLower());
		public bool IsParameterExists(List<AliasModel> amList, string parameter) => amList.Exists(el => el.EnVal.ToLower() == parameter.ToLower());

		List<AliasModel> IColumnsSelected.GetAllMapedColumns(Dictionary<string, string> alias, Type t)
		{
			return GetAllMapedColumns(alias, t);
		}

		public List<AliasModel> GetAllMapedCustomColumns(Dictionary<string, string> alias, Type t)
		{
			throw new NotImplementedException();
		}
	}
}
