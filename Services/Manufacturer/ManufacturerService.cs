using app.Db.ef;
using app.Db.Uow;
using app.Db.utils;
using app.Services.Common.Ref;
using app.Services.Component;
using app.Services.Manufacturer.production;
using System.Reflection;
using System.Threading.Tasks;

namespace app.Services.Manufacturer
{
	public class ManufacturerService : IManufacturerService
	{
		readonly UnitOfWork1 _uow;
		public ManufacturerService(UnitOfWork1 uow)
		{
			_uow = uow;
		}

		public async Task<List<Manufacturers>> GetAsObj()
		{
			return await _uow.ManufacturerRepository.SelectAsObj();
		}
		public async Task<List<Dictionary<string, object>>> GetAsDict()
		{
			return await _uow.ManufacturerRepository.SelectAsDict();
		}
		public async Task<ClassifiedManufacturersModel> GetForeignList(List<IComponentModel> components)
		{
			var manufacturers = await GetAsObj();
			var oneManufacturerDict = ConvertToOneDict(manufacturers);
			var mfProduction = GetProdAsDict(components);
			var cmm = new ClassifiedManufacturersModel() { CIS = new(), OTHER = new() };
			foreach (var name in mfProduction.Keys)
			{
				if (oneManufacturerDict.ContainsKey(name))
				{
					var manufacturerForeignness = oneManufacturerDict[name]["foreignness"];
					if (manufacturerForeignness == "native")
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
		public Dictionary<string, Dictionary<string, int>> GetProdAsDict(List<IComponentModel> components)
		{
			Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string, int>>();
			foreach (var item in components)
			{
				if (!dict.ContainsKey(item.ManufacturerName))
				{
					dict[item.ManufacturerName] = new Dictionary<string, int>();
				}
				if (!dict[item.ManufacturerName].ContainsKey(item.EnComponentType))
				{
					dict[item.ManufacturerName][item.EnComponentType] = 0;
				}
				dict[item.ManufacturerName][item.EnComponentType] += 1;
			}
			return dict;
		}

		public Dictionary<string, Dictionary<string, string>> ConvertToOneDict(List<Manufacturers> manufacturers)
		{
			Dictionary<string, Dictionary<string, string>> dict = new();
			foreach (var manufacturer in manufacturers)
			{
				dict[manufacturer.ManufacturerName] = new();
				dict[manufacturer.ManufacturerName]["country"] = manufacturer.CountryName;
				dict[manufacturer.ManufacturerName]["foreignness"] = manufacturer.ForeignnessType;
			}
			return dict;
		}

		//public Dictionary<string, object> ObjToDictionary(object instance, PropertyInfo[] props = null)
		//{
		//	var t = instance.GetType();
		//	if (props == null)
		//	{
		//		props = t.GetProperties();
		//	}
		//	Dictionary<string, object> res = new();
		//	foreach (var prop in props)
		//	{
		//		var val = prop.GetValue(instance);
		//		res[prop.Name] = val;
		//	}
		//	return res;
		//}
		

		public Dictionary<string, List<ManufacturerProductionModel>> GetStatistic(List<IComponentModel> components, Dictionary<string, Func<int>> totals)
		{
			Dictionary<string ,int> counts = new Dictionary<string ,int>();
			Dictionary<string, List<ManufacturerProductionModel>> dict = new Dictionary<string, List<ManufacturerProductionModel>>();
			foreach (var item in components)
			{
				if (!dict.ContainsKey(item.EnComponentType))
				{
					dict[item.EnComponentType] = new List<ManufacturerProductionModel>();
					counts[item.EnComponentType.ToLower()] = totals[item.EnComponentType.ToLower()]();
				}
				var list = dict[item.EnComponentType].Where(mp => mp.ManufacturerName == item.ManufacturerName).ToList();

				int componentTypeTotal = counts[item.EnComponentType.ToLower()];
				if (list.Count == 0)
				{
					ManufacturerProductionModel mp = new ManufacturerProductionModel()
					{
						ManufacturerName = item.ManufacturerName,
						Amount = 1,
						Weight = 100 / double.Parse(componentTypeTotal.ToString())
					};
					dict[item.EnComponentType].Add(mp);
				}
				else
				{
					foreach (var existedMP in dict[item.EnComponentType].Where(mp => mp.ManufacturerName == item.ManufacturerName))
					{
						existedMP.Amount += 1;
						existedMP.Weight = (100 * existedMP.Amount) / double.Parse(componentTypeTotal.ToString());
						break;
					}
				}
			}
			return dict;
		}
	}
}
