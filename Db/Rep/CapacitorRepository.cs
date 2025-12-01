using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf.Filters;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Security.Principal;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace app.Db.Rep
{
	public class CapacitorRepository : ComponentBase<Capacitors>, IRepositoryBase<Capacitors>
	{
		DataBase db;
		public CapacitorRepository(DataBase context) {
			db = context;
		}

		public Task<List<Capacitors>> SelectAsObj((Dictionary<string, object> pairs, List<int> ids) parameters = default)
		{
			var query = db.Capacitors.AsQueryable();
			if (parameters.pairs != null)
			{
				query = FilterByParamValue(parameters.pairs, query);
			}
			if (parameters.ids != null)
			{
				query = FilterByIds(parameters.ids, query);
			}
			var items = query
							.Select(c => new Capacitors(c)
							{
								RuComponentKind = c.Kind.RuComponentKind,
								EnComponentKind = c.Kind.EnComponentKind,
								RuComponentType = c.Type.RuComponentType,
								EnComponentType = c.Type.EnComponentType,
								ManufacturerName = c.Manufacturer.ManufacturerName,
							});
			return items.ToListAsync();
		}

		public async Task<List<Dictionary<string, object>>> SelectAsDict((Dictionary<string, object> pairs, List<int> ids) parameters = default)
		{
			var query = db.Capacitors.AsQueryable();
			if (parameters.pairs != null)
			{
				query = FilterByParamValue(parameters.pairs, query);
			}
			if (parameters.ids != null)
			{
				query = FilterByIds(parameters.ids, query);
			}
			var result = new List<Dictionary<string, object>>();
			var props = typeof(Capacitors).GetProperties()
				.Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)))
				.ToArray();

			await foreach (var c in query.AsAsyncEnumerable())
			{
				var cap = new Capacitors(c)
				{
					RuComponentKind = c.Kind.RuComponentKind,
					EnComponentKind = c.Kind.EnComponentKind,
					RuComponentType = c.Type.RuComponentType,
					EnComponentType = c.Type.EnComponentType,
					ManufacturerName = c.Manufacturer.ManufacturerName,
				};

				var dictItem = new Dictionary<string, object>();
				foreach (var prop in props)
				{
					dictItem[prop.Name] = prop.GetValue(cap);
				}

				result.Add(dictItem);
			}

			return result;
		}
		public int GetCount()
		{
			return db.Capacitors.Count();
		}
		



		
		//public override Task<List<IComponentModel>> SelectPreview(Dictionary<string, string> dict)
		//{
		//	var query = db.Capacitors.AsQueryable();
		//	query = FilterByParamValue(dict, query);
		//	var items = SelectPreview(query);
		//	return items.ToListAsync();
		//}
		//public override Task<List<IComponentModel>> SelectPreview(List<int> ids)
		//{
		//	var query = db.Capacitors.AsQueryable();
		//	query = FilterByIds(ids, query);
		//	var items = SelectPreview(query);
		//	return items.ToListAsync();
		//}


		public List<Dictionary<string, object>> ToDictionary(List<Capacitors> components)
		{
			List<Dictionary<string, object>> keyValuePairs = new List<Dictionary<string, object>>();
			var dictionary = new Dictionary<string, object>();

			foreach (var component in components)
			{
				keyValuePairs.Add(ObjToDictionary(component));
			}
			return keyValuePairs;
		}

		public Dictionary<string, object> ObjToDictionary(Capacitors obj)
		{

			var dictionary = new Dictionary<string, object>();
			PropertyInfo[] properties = obj.GetType().GetProperties(); 
			
			foreach (PropertyInfo property in properties)
			{
				dictionary.Add(property.Name, property.GetValue(obj));
			}

			return dictionary;
		}

		


		//public override Task<List<Capacitors>> SelectByListIds(List<int> ids)
		//{
		//	var query = db.Capacitors.AsQueryable();
		//	query = FilterByIds(ids, query);
		//	var items = query.Select(c => new Capacitors(c)
		//	{
		//		RuComponentKind = c.Kind.RuComponentKind,
		//		EnComponentKind = c.Kind.RuComponentKind,
		//		RuComponentType = c.Type.RuComponentType,
		//		EnComponentType = c.Type.EnComponentType,
		//		ManufacturerName = c.Manufacturer.ManufacturerName,
		//	});

		//	return items.ToListAsync();
		//}
	}
}
