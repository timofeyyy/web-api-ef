using app.Context;
using app.db;
using app.Entities;
using app.Models.Ef;
using app.src1.interfaces;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Security.Principal;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


		public Task<List<Manufacturers>> Select(Dictionary<string, string> dict = null)
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

		public Dictionary<string, Dictionary<string, int>> GetManufacturersProductionAsDictionarty(List<IComponentModel> components)
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
		public Dictionary<string, Dictionary<string, string>> GetManufacturersAsDictionary(List<Manufacturers> manufacturers)
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
