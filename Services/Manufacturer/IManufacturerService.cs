using app.Db.ef;
using app.Db.utils;
using app.Services.Component.component;
using app.Services.Manufacturer.production;

namespace app.Services.Manufacturer
{
	public interface IManufacturerService
	{
		public Task<List<Manufacturers>> SelectAll();
		//public Task<List<KeyValueObject>> GetAsDict();
		//public Dictionary<string, Dictionary<string, string>> ConvertToOneDict(List<KeyValueObject> manufacturers);
		//public Task<ClassifiedManufacturersModel> GetForeignList(ComponentAllModel components);
		public ClassifiedManufacturersModel GetForeignList(DataSystemView components, List<Manufacturers> manufacturers);

		public Dictionary<string, Dictionary<string, int>> GetProdAsDict(DataSystemView components);
		public Dictionary<string, List<ManufacturerProductionModel>> GetStatistic(DataSystemView components);
		//public Dictionary<string, List<ManufacturerProductionModel>> GetStatistic(List<IComponentModel> components, Dictionary<string, Func<int>> totals);
	}
}
