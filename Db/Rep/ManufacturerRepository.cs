using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Db.Rep
{
	public class ManufacturerRepository: IRepositoryBase<Manufacturers>
	{
		DataBase db;
		public ManufacturerRepository(DataBase context) {
			db = context;
		}

		public int GetCount()
		{
			return db.Manufacturers.Count();
		}

		public Task<List<Manufacturers>> SelectAsObj((Dictionary<string, object> pairs, List<int> ids) parameters = default)
		{
			var query = db.Manufacturers.AsQueryable();
			
			var items = query
							.Select(m => new Manufacturers(m)
							{
								CountryName = m.Country.CountryName,
								ForeignnessType = m.Country.Foreignness.ForeignName,
							});
			return items.ToListAsync();
		}

		public async Task<List<Dictionary<string, object>>> SelectAsDict((Dictionary<string, object> pairs, List<int> ids) parameters = default)
		{
			var query = db.Manufacturers.AsQueryable();
			var result = new List<Dictionary<string, object>>();
			var props = typeof(Manufacturers).GetProperties()
				.Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)))
				.ToArray();

			await foreach (var c in query.AsAsyncEnumerable())
			{
				var m = new Manufacturers(c)
				{
					CountryName = c.Country.CountryName,
					ForeignnessType = c.Country.Foreignness.ForeignName,
				};
				var dictItem = new Dictionary<string, object>();
				foreach (var prop in props)
				{
					dictItem[prop.Name] = prop.GetValue(m);
				}

				result.Add(dictItem);
			}

			return result;
		}





		public Dictionary<string, Dictionary<string, int>> GetMfsProdAsDict(List<IComponentModel> components)
		{
			Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string, int>>();
			foreach (var item in components)
			{
				if (!dict.ContainsKey(item.Manufacturer.ManufacturerName))
				{
					dict[item.Manufacturer.ManufacturerName] = new Dictionary<string, int>();
				}
				if (!dict[item.Manufacturer.ManufacturerName].ContainsKey(item.Type.EnComponentType))
				{
					dict[item.Manufacturer.ManufacturerName][item.Type.EnComponentType] = 0;
				}
				dict[item.Manufacturer.ManufacturerName][item.Type.EnComponentType] += 1;
			}
			return dict;
		}
		public Dictionary<string, Dictionary<string, string>> GetMfsAsDict(List<Manufacturers> manufacturers)
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
	}
}
