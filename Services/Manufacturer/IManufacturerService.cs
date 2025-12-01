using app.Db.ef;
using app.Db.utils;
using app.Services.Manufacturer.production;

namespace app.Services.Manufacturer
{
	public interface IManufacturerService
	{
		public Task<List<Manufacturers>> GetAsObj();
		public Task<List<Dictionary<string, object>>> GetAsDict();
		public Dictionary<string, Dictionary<string, string>> ConvertToOneDict(List<Manufacturers> manufacturers);
		public Task<ClassifiedManufacturersModel> GetForeignList(List<IComponentModel> components);
		public Dictionary<string, Dictionary<string, int>> GetProdAsDict(List<IComponentModel> components);
		public Dictionary<string, List<ManufacturerProductionModel>> GetStatistic(List<IComponentModel> components, Dictionary<string, Func<int>> totals);
	}
}
