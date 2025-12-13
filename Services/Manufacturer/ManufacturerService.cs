using app.Db.ef;
using app.Db.Uow;
using app.Services.Manufacturer.production;

namespace app.Services.Manufacturer
{
	public class ManufacturerService : IManufacturerService
	{
		readonly UnitOfWork1 _uow;
		public ManufacturerService(UnitOfWork1 uow)
		{
			_uow = uow;
		}
		public async Task<List<Manufacturers>> SelectAll()
		{
			return await _uow.ManufacturerRepository.SelectAll();
		}
		public ClassifiedManufacturersModel GetForeignList(DataSystemView components, List<Manufacturers> manufacturers)
		{
			var mfProduction = GetProdAsDict(components);
			var cmm = new ClassifiedManufacturersModel() { CIS = new(), OTHER = new() };

			foreach (var manufacturer in manufacturers)
			{
				if(manufacturer.ForeignnessType == null || !mfProduction.ContainsKey(manufacturer.ManufacturerName))
				{
					continue;
				}

				if (manufacturer.ForeignnessType == "native")
				{
					cmm.CIS[manufacturer.ManufacturerName] = mfProduction[manufacturer.ManufacturerName];
				}
				else
				{
					cmm.OTHER[manufacturer.ManufacturerName] = mfProduction[manufacturer.ManufacturerName];
				}
			}
			return cmm;
		}
		public Dictionary<string, Dictionary<string, int>> GetProdAsDict(DataSystemView components)
		{
			Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string, int>>();
			foreach (var section in components)
			{
				ComponentList all = section.Value.Components;
				foreach (var item in all)
				{
					string manufacturer = (string)item["ManufacturerName"];
					string enType = (string)item["EnComponentType"];
					if (manufacturer == null || enType == null)
					{
						continue;
					}
					if (!dict.ContainsKey(manufacturer))
					{
						dict[manufacturer] = new Dictionary<string, int>();
					}
					if (!dict[manufacturer].ContainsKey(enType))
					{
						dict[manufacturer][enType] = 0;
					}
					dict[manufacturer][enType] += 1;
				}
			}
			return dict;
		}
		public Dictionary<string, List<ManufacturerProductionModel>> GetStatistic(DataSystemView components)
		{
			Dictionary<string ,int> counts = new Dictionary<string ,int>();
			Dictionary<string, List<ManufacturerProductionModel>> dict = new Dictionary<string, List<ManufacturerProductionModel>>();
			foreach (var section in components)
			{
				dict[section.Key] = new List<ManufacturerProductionModel>();
				;
				foreach (var component in section.Value.Components)
				{
					var list = dict[section.Key].Where(mp => mp.ManufacturerName == (string)component["ManufacturerName"]).ToList();
					if (list.Count == 0)
					{
						ManufacturerProductionModel mp = new ManufacturerProductionModel()
						{
							ManufacturerName = (string)component["ManufacturerName"],
							Amount = 1,
							Weight = 100 / double.Parse(section.Value.Components.Count.ToString())
						};
						dict[section.Key].Add(mp);
					}
					else
					{
						foreach (var existedMP in dict[section.Key].Where(mp => mp.ManufacturerName == (string)component["ManufacturerName"]))
						{
							existedMP.Amount += 1;
							existedMP.Weight = (100 * existedMP.Amount) / double.Parse(section.Value.Components.Count.ToString());
							break;
						}
					}
				}
			}
			return dict;
		}
	}
}
